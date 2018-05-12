using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Elements.Config.Concepts
{
    using ConfigCode = UInt16;
    using ConfigBase = Module.Interfaces.Matrixus.IArticleConfigFeatures;
    using ConfigRole = UInt16;

    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using SourcePack = ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ValidsPack = ResultMonad.Result<bool, string>;
    using MasterItem = Articles.TaxBaseWithholdArticle;

    using TAmountDec = Decimal;

    using Legalist.Constants;
    using Module.Interfaces.Elements;
    using Module.Interfaces.Legalist;
    using Module.Items;
    using Utils;
    using Sources;
    using Results;
    using ResultMonad;

    public static class TaxBaseWithholdConcept
    {
        public static string CONCEPT_DESCRIPTION_ERROR_FORMAT = "TaxBaseWithholdConcept(ARTICLE_TAX_BASE_WITHHOLD, 1019): {0}";
        public static string CONCEPT_PROFILE_NULL_TEXT = "Taxing profile is null!";

        public static IEnumerable<ResultPack> EvaluateConcept(ConfigBase evalConfig, Period evalPeriod, IPeriodProfile evalProfile,
            Result<MasterItem.EvaluateSource, string> prepValues)
        {
            ITaxingProfile conceptProfile = evalProfile.Taxing();
            if (conceptProfile == null)
            {
                return EvaluateUtils.DecoratedError(CONCEPT_DESCRIPTION_ERROR_FORMAT, CONCEPT_PROFILE_NULL_TEXT);
            }

            MasterItem.EvaluateSource conceptValues = prepValues.Value;
            // EVALUATION
            TAmountDec basisSourced = conceptProfile.TaxableBaseWithholdTaxingMode(evalPeriod,
                conceptValues.IncomeWithhold);
            TAmountDec basisRounded = conceptProfile.TaxableBaseWithholdTaxingMode(evalPeriod,
                conceptValues.IncomeWithhold);
            TAmountDec basisFinally = conceptProfile.TaxableBaseWithholdTaxingMode(evalPeriod,
                conceptValues.IncomeWithhold);
            // EVALUATION

            IArticleResult conceptResult = new ArticleGeneralResult(evalConfig);
            // SET RESULT VALUES
            conceptResult.AddMoneyTaxingBasisValue(basisSourced, basisRounded, basisFinally);
            // SET RESULT VALUES

            return EvaluateUtils.Results(conceptResult);
        }
    }
}
