using System;
using System.Collections.Generic;
using ResultMonad;

namespace ElementsLib.Elements.Config.Articles
{
    using ConfigCodeEnum = Module.Codes.ArticleCodeCz;
    using ConfigCode = UInt16;
    using ConfigBase = Module.Interfaces.Matrixus.IArticleConfigFeatures;
    using ConfigRoleEnum = Module.Codes.ArticleRoleCz;
    using ConfigRole = UInt16;

    using TAmountDec = Decimal;

    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using TargetErrs = String;
    using SourcePack = ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ValidsPack = ResultMonad.Result<bool, string>;
    using SourceItem = Sources.TaxBaseWithholdSource;

    using Sources;
    using Concepts;
    using Legalist.Constants;
    using Module.Items;
    using Module.Libs;
    using Module.Interfaces.Elements;
    using Module.Interfaces.Legalist;
    using Module.Interfaces.Matrixus;
    using Utils;
    using Results;
    using Module.Codes;
    using Matrixus.Config;
    using Module.Items.Utils;

    public class TaxBaseWithholdArticle : GeneralArticle, ICloneable
    {
        protected delegate IEnumerable<ResultPack> EvaluateConceptDelegate(ConfigBase evalConfig, Period evalPeriod, IPeriodProfile evalProfile, Result<EvaluateSource, string> prepValues);

        public static string ARTICLE_DESCRIPTION_ERROR_FORMAT = "TaxBaseWithholdArticle(ARTICLE_TAX_BASE_WITHHOLD, 1019): {0}";

        public TaxBaseWithholdArticle() : base((ConfigRole)ConfigRoleEnum.ARTICLE_TAX_BASE_WITHHOLD)
        {
            SourceValues = new TaxBaseWithholdSource();

            InternalEvaluate = TaxBaseWithholdConcept.EvaluateConcept;
        }

        public TaxBaseWithholdArticle(ISourceValues values) : this()
        {
            TaxBaseWithholdSource sourceValues = values as TaxBaseWithholdSource;

            SourceValues = CloneUtils<TaxBaseWithholdSource>.CloneOrNull(sourceValues);
        }

        protected EvaluateConceptDelegate InternalEvaluate { get; set; }

        protected override IEnumerable<ResultPack> EvaluateArticleResults(TargetItem evalTarget, ConfigBase evalConfig, ISourceValues evalValues, Period evalPeriod, IPeriodProfile evalProfile, IEnumerable<ResultPair> evalResults)
        {
            if (InternalEvaluate == null)
            {
                return EvaluateUtils.DecoratedError(ARTICLE_DESCRIPTION_ERROR_FORMAT, EXCEPTION_RESULT_NONE_TEXT);
            }
            var sourceBuilder = new EvaluateSource.SourceBuilder(evalValues);
            var resultBuilder = new EvaluateSource.ResultBuilder(evalTarget, evalResults);

            var bundleValues = PrepareConceptValues<EvaluateSource>(sourceBuilder, resultBuilder);
            if (bundleValues.IsFailure)
            {
                return EvaluateUtils.DecoratedError(ARTICLE_DESCRIPTION_ERROR_FORMAT, bundleValues.Error);
            }
            return InternalEvaluate(evalConfig, evalPeriod, evalProfile, bundleValues);
        }

        public TaxBaseWithholdSource SourceValues { get; set; }

        public override void ImportSourceValues(ISourceValues values)
        {
            SourceValues = SetSourceValues<TaxBaseWithholdSource>(values);
        }

        public override ISourceValues ExportSourceValues()
        {
            return SourceValues as ISourceValues;
        }

        public override string ArticleDecorateMessage(string message)
        {
            return string.Format(ARTICLE_DESCRIPTION_ERROR_FORMAT, message);
        }

        public override object Clone()
        {
            TaxBaseWithholdArticle cloneArticle = (TaxBaseWithholdArticle)this.MemberwiseClone();

            cloneArticle.InternalConfig = CloneUtils<IArticleConfigFeatures>.CloneOrNull(this.InternalConfig);
            cloneArticle.InternalRole = this.InternalRole;
            cloneArticle.InternalEvaluate = this.InternalEvaluate;

            return cloneArticle;
        }

        public class EvaluateSource
        {
            public EvaluateSource()
            {
                IncomeWithhold = TAmountDec.Zero;
            }

            // PROPERTIES DEF
            public TAmountDec IncomeWithhold { get; set; }
            // PROPERTIES DEF
            public class SourceBuilder : EvalValuesSourceBuilder<EvaluateSource>
            {
                public SourceBuilder(ISourceValues evalValues) : base(evalValues)
                {
                }

                public override EvaluateSource GetNewValues(EvaluateSource initValues)
                {
#if GET_SOURCE_VALUE
                    SourceItem conceptValues = InternalValues as SourceItem;
                    if (conceptValues == null)
                    {
                        return ReturnFailure(initValues);
                    }
                    return new EvaluateSource
                    {
                        // PROPERTIES SET
                        // PROPERTIES SET
                    };
#else
                    return initValues;
#endif
                }
            }
            public class ResultBuilder : EvalValuesResultBuilder<EvaluateSource>
            {
                public ResultBuilder(TargetItem evalTarget, IEnumerable<ResultPair> evalResults) : base(evalTarget, evalResults)
                {
                }

                private Result<MoneyAmountSum, string> GetTaxableIncome(IEnumerable<ResultPair> results, TargetItem target)
                {
                    ConfigCode incomeTaxingCode = (ConfigCode)ArticleCodeCz.FACT_TAX_INCOMES_WITHHOLD_GENERAL;

                    Result<MoneyAmountSum, string> taxableIncome = FindResultUtils.FindMoneyTransferIncomeValue(results,
                        TargetFilters.TargetCodeFunc(incomeTaxingCode));

                    return taxableIncome;
                }
                public override EvaluateSource GetNewValues(EvaluateSource initValues)
                {
                    Result<MoneyAmountSum, string> taxableIncome = GetTaxableIncome(InternalValues, InternalTarget);

                    if (ResultMonadUtils.HaveAnyResultFailed(taxableIncome))
                    {
                        return ReturnFailureAndError(initValues, taxableIncome.Error);
                    }

                    MoneyAmountSum taxableValues = taxableIncome.Value;

                    return new EvaluateSource
                    {
                        // PROPERTIES SET
                        IncomeWithhold = taxableValues.Balance(),
                        // PROPERTIES SET
                    };
                }
            }
        }
    }
}
