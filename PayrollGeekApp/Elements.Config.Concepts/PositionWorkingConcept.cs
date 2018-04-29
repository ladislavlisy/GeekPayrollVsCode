using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Elements.Config.Concepts
{
    using ConfigCode = UInt16;
    using ConfigRole = UInt16;

    using TSeconds = Int32;

    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using SourcePack = ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ValidsPack = ResultMonad.Result<bool, string>;
    using MasterItem = Articles.PositionWorkingArticle;

    using Module.Interfaces.Elements;
    using Module.Interfaces.Legalist;
    using Module.Items;
    using Utils;
    using Sources;
    using Results;
    using ResultMonad;
    using Module.Items.Utils;

    public static class PositionWorkingConcept
    {
        public static string CONCEPT_DESCRIPTION_ERROR_FORMAT = "PositionWorkingConcept(ARTICLE_POSITION_WORKING, 9): {0}";
        public static string CONCEPT_PROFILE_NULL_TEXT = "Employ profile is null!";

        public static IEnumerable<ResultPack> EvaluateConcept(ConfigCode evalCode, Period evalPeriod, IPeriodProfile evalProfile,
            Result<MasterItem.EvaluateSource, string> prepValues)
        {
            IEmployProfile conceptProfile = evalProfile.Employ();
            if (conceptProfile == null)
            {
                return EvaluateUtils.DecoratedError(CONCEPT_DESCRIPTION_ERROR_FORMAT, CONCEPT_PROFILE_NULL_TEXT);
            }

            MasterItem.EvaluateSource conceptValues = prepValues.Value;
            // EVALUATION
            TSeconds[] scheduleWorked = PeriodUtils.ScheduleFromTemplate(conceptValues.ScheduleMonth, 1, 32);
            // EVALUATION

            IArticleResult conceptResult = new ArticleGeneralResult(evalCode);
            // SET RESULT VALUES
            conceptResult.AddWorkMonthTermScheduleValue(scheduleWorked);
            // SET RESULT VALUES

            return EvaluateUtils.Results(conceptResult);
        }
    }
}
