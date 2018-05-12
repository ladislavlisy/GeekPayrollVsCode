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
    using SourceItem = Sources.TaxBaseAdvancePartialSource;

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
    using Module.Items.Utils;
    using Matrixus.Config;
    using Module.Codes;

    public class TaxBaseAdvancePartialArticle : GeneralArticle, ICloneable
    {
        protected delegate IEnumerable<ResultPack> EvaluateConceptDelegate(ConfigBase evalConfig, Period evalPeriod, IPeriodProfile evalProfile, Result<EvaluateSource, string> prepValues);

        public static string ARTICLE_DESCRIPTION_ERROR_FORMAT = "TaxBaseAdvancePartialArticle(ARTICLE_TAX_BASE_ADVANCE_PARTIAL, 1017): {0}";

        public TaxBaseAdvancePartialArticle() : base((ConfigRole)ConfigRoleEnum.ARTICLE_TAX_BASE_ADVANCE_PARTIAL)
        {
            SourceValues = new TaxBaseAdvancePartialSource();

            InternalEvaluate = TaxBaseAdvancePartialConcept.EvaluateConcept;
        }

        public TaxBaseAdvancePartialArticle(ISourceValues values) : this()
        {
            TaxBaseAdvancePartialSource sourceValues = values as TaxBaseAdvancePartialSource;

            SourceValues = CloneUtils<TaxBaseAdvancePartialSource>.CloneOrNull(sourceValues);
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

        public TaxBaseAdvancePartialSource SourceValues { get; set; }

        public override void ImportSourceValues(ISourceValues values)
        {
            SourceValues = SetSourceValues<TaxBaseAdvancePartialSource>(values);
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
            TaxBaseAdvancePartialArticle cloneArticle = (TaxBaseAdvancePartialArticle)this.MemberwiseClone();

            cloneArticle.InternalConfig = CloneUtils<IArticleConfigFeatures>.CloneOrNull(this.InternalConfig);
            cloneArticle.InternalRole = this.InternalRole;
            cloneArticle.InternalEvaluate = this.InternalEvaluate;

            return cloneArticle;
        }

        public class EvaluateSource
        {
            public EvaluateSource()
            {
                GeneralBaseAmount = TAmountDec.Zero;
                HealthsPartAmount = TAmountDec.Zero;
                SocialsPartAmount = TAmountDec.Zero;
            }

            // PROPERTIES DEF
            public TAmountDec GeneralBaseAmount { get; set; }
            public TAmountDec HealthsPartAmount { get; set; }
            public TAmountDec SocialsPartAmount { get; set; }
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

                private Result<MoneyAmountSum, string> GetGeneralBase(IEnumerable<ResultPair> results, TargetItem target)
                {
                    ConfigCode taxBaseCode = (ConfigCode)ArticleCodeCz.FACT_TAX_BASE_ADVANCE;

                    Result<MoneyAmountSum, string> taxBaseAmount = FindResultUtils.FindMoneyTaxingBasisValue(results,
                        TargetFilters.TargetCodeFunc(taxBaseCode));

                    return taxBaseAmount;
                }
                private Result<MoneyAmountSum, string> GetHealthsPart(IEnumerable<ResultPair> results, TargetItem target)
                {
                    ConfigCode taxPartCode = (ConfigCode)ArticleCodeCz.FACT_TAX_BASE_ADVANCE_HEALTH;

                    Result<MoneyAmountSum, string> taxPartAmount = FindResultUtils.FindMoneyInsuranceBasisValue(results, 
                        TargetFilters.TargetCodeFunc(taxPartCode));
                    return taxPartAmount;
                }
                private Result<MoneyAmountSum, string> GetSocialsPart(IEnumerable<ResultPair> results, TargetItem target)
                {
                    ConfigCode taxPartCode = (ConfigCode)ArticleCodeCz.FACT_TAX_BASE_ADVANCE_SOCIAL;

                    Result<MoneyAmountSum, string> taxPartAmount = FindResultUtils.FindMoneyInsuranceBasisValue(results,
                        TargetFilters.TargetCodeFunc(taxPartCode));
                    return taxPartAmount;
                }
                public override EvaluateSource GetNewValues(EvaluateSource initValues)
                {
                    Result<MoneyAmountSum, string> generalBaseResult = GetGeneralBase(InternalValues, InternalTarget);
                    Result<MoneyAmountSum, string> healthsPartResult = GetHealthsPart(InternalValues, InternalTarget);
                    Result<MoneyAmountSum, string> socialsPartResult = GetSocialsPart(InternalValues, InternalTarget);

                    if (ResultMonadUtils.HaveAnyResultFailed(generalBaseResult, healthsPartResult, socialsPartResult))
                    {
                        return ReturnFailureAndError(initValues, 
                            ResultMonadUtils.FirstFailedResultError(generalBaseResult, healthsPartResult, socialsPartResult));
                    }

                    MoneyAmountSum generalBaseValues = generalBaseResult.Value;
                    MoneyAmountSum healthsPartValues = healthsPartResult.Value;
                    MoneyAmountSum socialsPartValues = socialsPartResult.Value;

                    return new EvaluateSource
                    {
                        // PROPERTIES SET
                        GeneralBaseAmount = generalBaseValues.Balance(),
                        HealthsPartAmount = healthsPartValues.Balance(),
                        SocialsPartAmount = socialsPartValues.Balance(),
                        // PROPERTIES SET
                    };
                }
            }
        }
    }
}
