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
    using SourceItem = Sources.TaxIncomesGeneralSource;
    using ResultType = Results.ArticleGeneralResult;

    using TAmountDec = Decimal;

    using Sources;
    using Concepts;
    using Module.Items;
    using Module.Libs;
    using Module.Interfaces.Elements;
    using Module.Interfaces.Legalist;
    using Utils;
    using Results;
    using Legalist.Constants;
    using Module.Interfaces.Matrixus;
    using Matrixus.Config;
    using Module.Codes;
    using Module.Items.Utils;

    public class InsIncomesHealthArticle : GeneralArticle, ICloneable
    {
        protected delegate IEnumerable<ResultPack> EvaluateConceptDelegate(ConfigBase evalConfig, Period evalPeriod, IPeriodProfile evalProfile, Result<EvaluateSource, string> prepValues);

        public static string ARTICLE_DESCRIPTION_ERROR_FORMAT = "InsIncomesHealthArticle(ARTICLE_INS_INCOMES_HEALTH, 1009): {0}";

        public InsIncomesHealthArticle() : base((ConfigRole)ConfigRoleEnum.ARTICLE_INS_INCOMES_HEALTH)
        {
            SourceValues = new InsIncomesHealthSource();

            InternalEvaluate = InsIncomesHealthConcept.EvaluateConcept;
        }

        public InsIncomesHealthArticle(ISourceValues values) : this()
        {
            InsIncomesHealthSource sourceValues = values as InsIncomesHealthSource;

            SourceValues = CloneUtils<InsIncomesHealthSource>.CloneOrNull(sourceValues);
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

        public InsIncomesHealthSource SourceValues { get; set; }

        public override void ImportSourceValues(ISourceValues values)
        {
            SourceValues = SetSourceValues<InsIncomesHealthSource>(values);
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
            InsIncomesHealthArticle cloneArticle = (InsIncomesHealthArticle)this.MemberwiseClone();

            cloneArticle.InternalConfig = CloneUtils<IArticleConfigFeatures>.CloneOrNull(this.InternalConfig);
            cloneArticle.InternalRole = this.InternalRole;
            cloneArticle.InternalEvaluate = this.InternalEvaluate;

            return cloneArticle;
        }

        public class EvaluateSource
        {
            public EvaluateSource()
            {
                SummarizeType = WorkHealthTerms.HEALTH_TERM_EMPLOYMENT;
                IncludeIncome = decimal.Zero;
                ExcludeIncome = decimal.Zero;
            }
            // PROPERTIES DEF
            public WorkHealthTerms SummarizeType { get; set; }
            public TAmountDec IncludeIncome { get; set; }
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

                private Result<MoneyPaymentSum, string> GetIncludeIncome(IEnumerable<ResultPair> results, TargetItem target)
                {
                    Result<MoneyPaymentSum, string> taxableIncome = GetSumResultUtils.GetSumMoneyPayment(results,
                        TargetFilters.TargetHeadFunc(target.Head()), ArticleFilters.InsIncomeHealthFunc);

                    return taxableIncome;
                }
                private Result<MoneyPaymentSum, string> GetExcludeIncome(IEnumerable<ResultPair> results, TargetItem target)
                {
                    Result<MoneyPaymentSum, string> taxableIncome = GetSumResultUtils.GetSumMoneyPayment(results,
                        TargetFilters.TargetHeadFunc(target.Head()), ArticleFilters.InsExcludeHealthFunc);

                    return taxableIncome;
                }

                public override EvaluateSource GetNewValues(EvaluateSource initValues)
                {
                    ConfigCode declaracyCode = (ConfigCode)ArticleCodeCz.FACT_INS_DECLARATION_HEALTH;

                    Result<DeclarationHealthValue, string> declaracyResult = InternalValues
                        .FindResultValue<ArticleGeneralResult, DeclarationHealthValue>(
                            TargetFilters.TargetCodePlusHeadAndNullPartFunc(declaracyCode, InternalTarget.Head()),
                            (x) => (x.IsDeclarationHealthValue()));

                    Result<MoneyPaymentSum, string> includeIncome = GetIncludeIncome(InternalValues, InternalTarget);
                    Result<MoneyPaymentSum, string> excludeIncome = GetExcludeIncome(InternalValues, InternalTarget);

                    if (ResultMonadUtils.HaveAnyResultFailed(declaracyResult, includeIncome, excludeIncome))
                    {
                        return ReturnFailureAndError(initValues,
                            ResultMonadUtils.FirstFailedResultError(declaracyResult, includeIncome, excludeIncome));
                    }

                    DeclarationHealthValue declaracyValues = declaracyResult.Value;

                    MoneyPaymentSum includeValues = includeIncome.Value;
                    MoneyPaymentSum excludeValues = excludeIncome.Value;

                    return new EvaluateSource
                    {
                        // PROPERTIES SET
                        SummarizeType = declaracyValues.SummarizeType,
                        IncludeIncome = includeValues.Balance(),
                        ExcludeIncome = excludeValues.Balance(),
                        // PROPERTIES SET
                    };
                }
            }
        }
    }
}
