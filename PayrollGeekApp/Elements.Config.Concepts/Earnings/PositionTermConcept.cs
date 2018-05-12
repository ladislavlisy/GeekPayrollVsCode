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
    using ResultItem = Module.Interfaces.Elements.IArticleResult;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ValidsPack = ResultMonad.Result<bool, string>;
    using MasterItem = Articles.PositionTermArticle;

    using TDay = Byte;

    using Module.Interfaces.Elements;
    using Module.Interfaces.Legalist;
    using Module.Items;
    using Utils;
    using Sources;
    using Results;
    using Module.Codes;
    using ResultMonad;
    using MaybeMonad;

    public static class PositionTermConcept
    {
        public static string CONCEPT_DESCRIPTION_ERROR_FORMAT = "PositionTermConcept(ARTICLE_POSITION_TERM, 6): {0}";
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
            TDay dayTermFrom = conceptProfile.DateFromInPeriod(evalPeriod, conceptValues.DateTermFrom);
            if (dayTermFrom < conceptValues.DayContractFrom)
            {
                dayTermFrom = conceptValues.DayContractFrom;
            }
            TDay dayTermStop = conceptProfile.DateStopInPeriod(evalPeriod, conceptValues.DateTermStop);
            if (dayTermStop > conceptValues.DayContractStop)
            {
                dayTermStop = conceptValues.DayContractStop;
            }
            // EVALUATION

            IArticleResult conceptResult = new ArticleGeneralResult(evalConfig);
            // SET RESULT VALUES
            conceptResult.AddPositionFromStop(conceptValues.DateTermFrom, conceptValues.DateTermStop, conceptValues.PositionType);
            conceptResult.AddMonthFromStop(dayTermFrom, dayTermStop);
            // SET RESULT VALUES

            return EvaluateUtils.Results(conceptResult);
        }
    }
}
