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
    using MasterItem = Articles.InsIncomesHealthArticle;

    using TAmountDec = Decimal;

    using Module.Interfaces.Elements;
    using Module.Interfaces.Legalist;
    using Module.Items;
    using Utils;
    using Sources;
    using Results;
    using ResultMonad;

    public static class InsIncomesHealthConcept
    {
        public static string CONCEPT_DESCRIPTION_ERROR_FORMAT = "InsIncomesHealthConcept(ARTICLE_INS_INCOMES_HEALTH, 1009): {0}";
        public static string CONCEPT_PROFILE_NULL_TEXT = "Health profile is null!";

        public static IEnumerable<ResultPack> EvaluateConcept(ConfigBase evalConfig, Period evalPeriod, IPeriodProfile evalProfile,
            Result<MasterItem.EvaluateSource, string> prepValues)
        {
            IHealthProfile conceptProfile = evalProfile.Health();
            if (conceptProfile == null)
            {
                return EvaluateUtils.DecoratedError(CONCEPT_DESCRIPTION_ERROR_FORMAT, CONCEPT_PROFILE_NULL_TEXT);
            }

            MasterItem.EvaluateSource conceptValues = prepValues.Value;
            // EVALUATION
            TAmountDec incomeTotalsGeneral = conceptProfile.IncludeGeneralIncomes(evalPeriod, 
                conceptValues.SummarizeType, conceptValues.IncludeIncome, conceptValues.ExcludeIncome);
            TAmountDec incomeTotalsExclude = conceptProfile.ExcludeGeneralIncomes(evalPeriod,
                conceptValues.SummarizeType, conceptValues.IncludeIncome, conceptValues.ExcludeIncome);
            // EVALUATION

            IArticleResult conceptResult = new ArticleGeneralResult(evalConfig);
            // SET RESULT VALUES
            conceptResult.AddIncomeInsHealthValue(conceptValues.SummarizeType, incomeTotalsGeneral, incomeTotalsExclude);
            // SET RESULT VALUES

            return EvaluateUtils.Results(conceptResult);
        }
    }
}
