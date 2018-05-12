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
    using ValidsPack = ResultMonad.Result<bool, string>;
    using SourceItem = Sources.TaxDeclarationSource;

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

    public class TaxDeclarationArticle : GeneralArticle, ICloneable
    {
        protected delegate IEnumerable<ResultPack> EvaluateConceptDelegate(ConfigBase evalConfig, Period evalPeriod, IPeriodProfile evalProfile, Result<EvaluateSource, string> prepValues);

        public static string ARTICLE_DESCRIPTION_ERROR_FORMAT = "TaxDeclarationArticle(ARTICLE_TAX_DECLARATION, 1001): {0}";

        public TaxDeclarationArticle() : base((ConfigRole)ConfigRoleEnum.ARTICLE_TAX_DECLARATION)
        {
            SourceValues = new TaxDeclarationSource();

            InternalEvaluate = TaxDeclarationConcept.EvaluateConcept;
        }

        public TaxDeclarationArticle(ISourceValues values) : this()
        {
            TaxDeclarationSource sourceValues = values as TaxDeclarationSource;

            SourceValues = CloneUtils<TaxDeclarationSource>.CloneOrNull(sourceValues);
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

        public TaxDeclarationSource SourceValues { get; set; }

        public override void ImportSourceValues(ISourceValues values)
        {
            SourceValues = SetSourceValues<TaxDeclarationSource>(values);
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
            TaxDeclarationArticle cloneArticle = (TaxDeclarationArticle)this.MemberwiseClone();

            cloneArticle.InternalConfig = CloneUtils<IArticleConfigFeatures>.CloneOrNull(this.InternalConfig);
            cloneArticle.InternalRole = this.InternalRole;
            cloneArticle.InternalEvaluate = this.InternalEvaluate;

            return cloneArticle;
        }

        public class EvaluateSource
        {
            public EvaluateSource()
            {
                StatementType = 0;
                SummarizeType = WorkTaxingTerms.TAXING_TERM_EMPLOYMENT_POLICY;
                DeclaracyType = 0;
                ResidencyType = 0;
                HealthAnnuity = TAmountDec.Zero;
                SocialAnnuity = TAmountDec.Zero;
            }
            // PROPERTIES DEF
            public Byte StatementType { get; set; }
            public WorkTaxingTerms SummarizeType { get; set; }
            public Byte DeclaracyType { get; set; }
            public Byte ResidencyType { get; set; }
            public TAmountDec HealthAnnuity { get; set; }
            public TAmountDec SocialAnnuity { get; set; }
            // PROPERTIES DEF
            public class SourceBuilder : EvalValuesSourceBuilder<EvaluateSource>
            {
                public SourceBuilder(ISourceValues evalValues) : base(evalValues)
                {
                }

                public override EvaluateSource GetNewValues(EvaluateSource initValues)
                {
                    SourceItem conceptValues = InternalValues as SourceItem;
                    if (conceptValues == null)
                    {
                        return ReturnFailure(initValues);
                    }
                    return new EvaluateSource
                    {
                        // PROPERTIES SET
                        StatementType = conceptValues.StatementType,
                        SummarizeType = conceptValues.SummarizeType,
                        DeclaracyType = conceptValues.DeclaracyType,
                        ResidencyType = conceptValues.ResidencyType,
                        HealthAnnuity = conceptValues.HealthAnnuity,
                        SocialAnnuity = conceptValues.SocialAnnuity,
                        // PROPERTIES SET
                    };
                }
            }
            public class ResultBuilder : EvalValuesResultBuilder<EvaluateSource>
            {
                public ResultBuilder(TargetItem evalTarget, IEnumerable<ResultPair> evalResults) : base(evalTarget, evalResults)
                {
                }

                public override EvaluateSource GetNewValues(EvaluateSource initValues)
                {
                    // PROPERTIES SET
                    // PROPERTIES SET
                    return initValues;
                }
            }
        }
    }
}
