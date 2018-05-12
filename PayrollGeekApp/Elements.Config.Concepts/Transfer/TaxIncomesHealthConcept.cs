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
    using MasterItem = Articles.TaxIncomesHealthArticle;

    using TAmountDec = Decimal;

    using Module.Interfaces.Elements;
    using Module.Interfaces.Legalist;
    using Module.Items;
    using Utils;
    using Sources;
    using Results;
    using ResultMonad;

    public static class TaxIncomesHealthConcept
    {
        public static string CONCEPT_DESCRIPTION_ERROR_FORMAT = "TaxIncomesHealthConcept(ARTICLE_TAX_INCOMES_HEALTH, 1005): {0}";
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
            TAmountDec incomeGeneralRelated = conceptProfile.TaxableGeneralIncomes(evalPeriod, conceptValues.SummarizeType,
                conceptValues.StatementType, conceptValues.DeclaracyType, conceptValues.ResidencyType,
                conceptValues.TaxableIncome, conceptValues.PartnerIncome, conceptValues.ExcludeIncome);
            TAmountDec incomeGeneralExclude = conceptProfile.ExcludeGeneralIncomes(evalPeriod, conceptValues.SummarizeType,
                conceptValues.StatementType, conceptValues.DeclaracyType, conceptValues.ResidencyType,
                conceptValues.TaxableIncome, conceptValues.PartnerIncome, conceptValues.ExcludeIncome);
            TAmountDec incomeLolevelRelated = conceptProfile.TaxableLolevelIncomes(evalPeriod, conceptValues.SummarizeType,
                conceptValues.StatementType, conceptValues.DeclaracyType, conceptValues.ResidencyType,
                conceptValues.TaxableIncome, conceptValues.PartnerIncome, conceptValues.ExcludeIncome);
            TAmountDec incomeAgrWorkRelated = conceptProfile.TaxableAgrWorkIncomes(evalPeriod, conceptValues.SummarizeType,
                conceptValues.StatementType, conceptValues.DeclaracyType, conceptValues.ResidencyType,
                conceptValues.TaxableIncome, conceptValues.PartnerIncome, conceptValues.ExcludeIncome);
            TAmountDec incomePartnerRelated = conceptProfile.TaxablePartnerIncomes(evalPeriod, conceptValues.SummarizeType,
                conceptValues.StatementType, conceptValues.DeclaracyType, conceptValues.ResidencyType,
                conceptValues.TaxableIncome, conceptValues.PartnerIncome, conceptValues.ExcludeIncome);
            // EVALUATION

            IArticleResult conceptResult = new ArticleGeneralResult(evalConfig);
            // SET RESULT VALUES
            conceptResult.AddIncomeTaxGeneralValue(conceptValues.SummarizeType,
                conceptValues.StatementType, conceptValues.ResidencyType,
                incomeGeneralRelated, incomeGeneralExclude, 
                incomeLolevelRelated, incomeAgrWorkRelated, incomePartnerRelated);
            // SET RESULT VALUES

            return EvaluateUtils.Results(conceptResult);
        }
    }
}
