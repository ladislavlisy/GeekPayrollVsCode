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
    using MasterItem = Articles.PositionScheduleArticle;

    using Module.Interfaces.Elements;
    using Module.Interfaces.Legalist;
    using Module.Items;
    using Utils;
    using Sources;
    using Results;
    using Legalist.Constants;
    using ResultMonad;

    public static class PositionScheduleConcept
    {
        public static string CONCEPT_DESCRIPTION_ERROR_FORMAT = "PositionScheduleConcept(ARTICLE_POSITION_SCHEDULE, 7): {0}";
        public static string CONCEPT_PROFILE_NULL_TEXT = "Employ profile is null!";
        public static string SCHEDULE_TYPE_NOTIMPLEMENTED_TEXT = "Schedule type is not implemented!";

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
	        TSeconds[] hoursFullWeeks = new TSeconds[0];
	        TSeconds[] hoursRealWeeks = new TSeconds[0];
            TSeconds[] hoursFullMonth = new TSeconds[0];
            TSeconds[] hoursRealMonth = new TSeconds[0];

            if (conceptValues.ScheduleType == WorkScheduleType.SCHEDULE_NORMALY_WEEK)
            {
                hoursFullWeeks = conceptProfile.TimesheetWeekSchedule(evalPeriod, conceptValues.ShiftLiable, 5);
                hoursRealWeeks = conceptProfile.TimesheetWeekSchedule(evalPeriod, conceptValues.ShiftActual, 5);
                hoursFullMonth = conceptProfile.TimesheetFullSchedule(evalPeriod, hoursFullWeeks);
                hoursRealMonth = conceptProfile.TimesheetFullSchedule(evalPeriod, hoursRealWeeks);
            }
            else
            {
                return EvaluateUtils.DecoratedErrors(CONCEPT_DESCRIPTION_ERROR_FORMAT, SCHEDULE_TYPE_NOTIMPLEMENTED_TEXT);
            }
            // EVALUATION

            IArticleResult conceptResult = new ArticleGeneralResult(evalCode);
            // SET RESULT VALUES
            conceptResult.AddWorkWeeksFullScheduleValue(hoursFullWeeks);
            conceptResult.AddWorkWeeksRealScheduleValue(hoursRealWeeks);
            conceptResult.AddWorkMonthFullScheduleValue(hoursFullMonth);
            conceptResult.AddWorkMonthRealScheduleValue(hoursRealMonth);
            // SET RESULT VALUES

            return EvaluateUtils.Results(conceptResult);
        }
    }
}
