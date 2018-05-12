using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Elements.Config.Concepts
{
    using ConfigCode = UInt16;
    using ConfigBase = Module.Interfaces.Matrixus.IArticleConfigFeatures;
    using ConfigRole = UInt16;

    using TDay = Byte;
    using TSeconds = Int32;

    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using SourcePack = ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ValidsPack = ResultMonad.Result<bool, string>;
    using MasterItem = Articles.PositionTimesheetArticle;

    using Module.Interfaces.Elements;
    using Module.Interfaces.Legalist;
    using Module.Items;
    using Utils;
    using Sources;
    using Results;
    using Module.Codes;
    using MaybeMonad;
    using ResultMonad;

    public static class PositionTimesheetConcept
    {
        public static string CONCEPT_DESCRIPTION_ERROR_FORMAT = "PositionTimesheetConcept(ARTICLE_POSITION_TIMESHEET, 8): {0}";
        public static string CONCEPT_PROFILE_NULL_TEXT = "Employ profile is null!";

        public static IEnumerable<ResultPack> EvaluateConcept(ConfigBase evalConfig, Period evalPeriod, IPeriodProfile evalProfile,
            Result<MasterItem.EvaluateSource, string> prepValues)
        {
            IEmployProfile conceptProfile = evalProfile.Employ();
            if (conceptProfile == null)
            {
                return EvaluateUtils.DecoratedError(CONCEPT_DESCRIPTION_ERROR_FORMAT, CONCEPT_PROFILE_NULL_TEXT);
            }

            MasterItem.EvaluateSource conceptValues = prepValues.Value;
            // EVALUATION
            TSeconds[] scheduleRealMonth = conceptValues.RealMonthHours.ToArray();
            TSeconds[] scheduleTermMonth = conceptProfile.TimesheetWorkSchedule(evalPeriod, scheduleRealMonth, conceptValues.DayTermFrom, conceptValues.DayTermStop);
            // EVALUATION

            IArticleResult conceptResult = new ArticleGeneralResult(evalConfig);

            // SET RESULT VALUES
            conceptResult.AddWorkMonthRealScheduleValue(scheduleRealMonth);
            conceptResult.AddWorkMonthTermScheduleValue(scheduleTermMonth);
            // SET RESULT VALUES

            return EvaluateUtils.Results(conceptResult);
        }
    }
}
