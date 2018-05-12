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

    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using TargetErrs = String;
    using SourcePack = ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ResultItem = Module.Interfaces.Elements.IArticleResult;
    using ValidsPack = ResultMonad.Result<bool, string>;
    using SourceItem = Sources.TaxBaseAdvanceSocialSource;

    using TAmountDec = Decimal;

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
    using Module.Items.Utils;
    using Matrixus.Config;

    public class TaxBaseAdvanceSocialArticle : GeneralArticle, ICloneable
    {
        protected delegate IEnumerable<ResultPack> EvaluateConceptDelegate(ConfigBase evalConfig, Period evalPeriod, IPeriodProfile evalProfile, Result<EvaluateSource, string> prepValues);

        public static string ARTICLE_DESCRIPTION_ERROR_FORMAT = "TaxBaseAdvanceSocialArticle(ARTICLE_TAX_BASE_ADVANCE_SOCIAL, 1016): {0}";

        public TaxBaseAdvanceSocialArticle() : base((ConfigRole)ConfigRoleEnum.ARTICLE_TAX_BASE_ADVANCE_SOCIAL)
        {
            SourceValues = new TaxBaseAdvanceSocialSource();

            InternalEvaluate = TaxBaseAdvanceSocialConcept.EvaluateConcept;
        }

        public TaxBaseAdvanceSocialArticle(ISourceValues values) : this()
        {
            TaxBaseAdvanceSocialSource sourceValues = values as TaxBaseAdvanceSocialSource;

            SourceValues = CloneUtils<TaxBaseAdvanceSocialSource>.CloneOrNull(sourceValues);
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

        public TaxBaseAdvanceSocialSource SourceValues { get; set; }

        public override void ImportSourceValues(ISourceValues values)
        {
            SourceValues = SetSourceValues<TaxBaseAdvanceSocialSource>(values);
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
            TaxBaseAdvanceSocialArticle cloneArticle = (TaxBaseAdvanceSocialArticle)this.MemberwiseClone();

            cloneArticle.InternalConfig = CloneUtils<IArticleConfigFeatures>.CloneOrNull(this.InternalConfig);
            cloneArticle.InternalRole = this.InternalRole;
            cloneArticle.InternalEvaluate = this.InternalEvaluate;

            return cloneArticle;
        }

        public class EvaluateSource
        {
            public EvaluateSource()
            {
                GeneralIncome = TAmountDec.Zero;
                LolevelIncome = TAmountDec.Zero;
                TaskAgrIncome = TAmountDec.Zero;
                PartnerIncome = TAmountDec.Zero;
                ExcludeIncome = TAmountDec.Zero;
            }

            // PROPERTIES DEF
            public TAmountDec GeneralIncome { get; set; }
            public TAmountDec LolevelIncome { get; set; }
            public TAmountDec TaskAgrIncome { get; set; }
            public TAmountDec PartnerIncome { get; set; }
            public TAmountDec ExcludeIncome { get; set; }
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

                private Result<TaxableIncomeSum, string> GetTaxableIncome(IEnumerable<ResultPair> results, TargetItem target)
                {
                    ConfigCode incomeTaxingCode = (ConfigCode)ArticleCodeCz.FACT_TAX_INCOMES_HEALTH;

                    Result<TaxableIncomeSum, string> taxableIncome = GetSumResultUtils.GetSumIncomeTaxGeneral(results,
                        TargetFilters.TargetCodeFunc(incomeTaxingCode), ArticleFilters.SelectAllFunc);

                    return taxableIncome;
                }
                public override EvaluateSource GetNewValues(EvaluateSource initValues)
                {
                    Result<TaxableIncomeSum, string> taxableIncome = GetTaxableIncome(InternalValues, InternalTarget);

                    if (ResultMonadUtils.HaveAnyResultFailed(taxableIncome))
                    {
                        return ReturnFailureAndError(initValues, taxableIncome.Error);
                    }

                    TaxableIncomeSum taxableValues = taxableIncome.Value;

                    return new EvaluateSource
                    {
                        // PROPERTIES SET
                        GeneralIncome = taxableValues.IncomeGeneral(),
                        ExcludeIncome = taxableValues.IncomeExclude(),
                        LolevelIncome = taxableValues.IncomeLolevel(),
                        TaskAgrIncome = taxableValues.IncomeTaskAgr(),
                        PartnerIncome = taxableValues.IncomePartner(),
                        // PROPERTIES SET
                    };
                }
            }
        }
    }
}
