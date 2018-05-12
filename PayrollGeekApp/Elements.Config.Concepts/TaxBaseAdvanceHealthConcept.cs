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
    using MasterItem = Articles.TaxBaseAdvanceHealthArticle;

    using TAmountDec = Decimal;

    using Legalist.Constants;
    using Module.Interfaces.Elements;
    using Module.Interfaces.Legalist;
    using Module.Items;
    using Utils;
    using Sources;
    using Results;
    using ResultMonad;

    public static class TaxBaseAdvanceHealthConcept
    {
        public static string CONCEPT_DESCRIPTION_ERROR_FORMAT = "TaxBaseAdvanceHealthConcept(ARTICLE_TAX_BASE_ADVANCE_HEALTH, 1015): {0}";
        public static string CONCEPT_PROFILE_NULL_TEXT = "Taxing profile is null!";
        public static string HEALTHS_PROFILE_NULL_TEXT = "Health profile is null!";

        public static IEnumerable<ResultPack> EvaluateConcept(ConfigBase evalConfig, Period evalPeriod, IPeriodProfile evalProfile,
            Result<MasterItem.EvaluateSource, string> prepValues)
        {
            ITaxingProfile conceptProfile = evalProfile.Taxing();
            if (conceptProfile == null)
            {
                return EvaluateUtils.DecoratedError(CONCEPT_DESCRIPTION_ERROR_FORMAT, CONCEPT_PROFILE_NULL_TEXT);
            }
            IHealthProfile healthsProfile = evalProfile.Health();
            if (healthsProfile == null)
            {
                return EvaluateUtils.DecoratedError(CONCEPT_DESCRIPTION_ERROR_FORMAT, HEALTHS_PROFILE_NULL_TEXT);
            }

            MasterItem.EvaluateSource conceptValues = prepValues.Value;
            // EVALUATION
            TAmountDec startedBasisAmount = conceptProfile.TaxableIncomesAdvanceTaxingMode(evalPeriod,
                conceptValues.GeneralIncome, conceptValues.ExcludeIncome,
                conceptValues.LolevelIncome, conceptValues.TaskAgrIncome, conceptValues.PartnerIncome);

            TAmountDec compoundPercFactor = healthsProfile.FactorCompound();
            TAmountDec roundedBasisAmount = conceptProfile.DecRoundUp(startedBasisAmount);
            TAmountDec cutdownBasisAmount = conceptProfile.TaxablePartialAdvanceHealth(evalPeriod, roundedBasisAmount, conceptValues.ExcludeIncome);
            TAmountDec cutdownAboveAmount = conceptProfile.CutDownPartialAdvanceHealth(evalPeriod, roundedBasisAmount, conceptValues.ExcludeIncome);
            TAmountDec finaledBasisAmount = conceptProfile.EployerPartialAdvanceHealth(evalPeriod, cutdownBasisAmount, compoundPercFactor);
            // EVALUATION

            IArticleResult conceptResult = new ArticleGeneralResult(evalConfig);
            // SET RESULT VALUES
            conceptResult.AddMoneyInsuranceBasisValue(startedBasisAmount, roundedBasisAmount, 
                cutdownBasisAmount, cutdownAboveAmount, finaledBasisAmount);
            // SET RESULT VALUES

            return EvaluateUtils.Results(conceptResult);
        }
    }
}
