using ElementsLib.Module.Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Evaluate.Sources
{
    using ConfigCodeEnum = Module.Codes.ArticleCodeCz;
    using ConfigCode = UInt16;
    using ConfigRoleEnum = Module.Codes.ArticleRoleCz;
    using ConfigRole = UInt16;

    using TargetHead = UInt16;
    using TargetPart = UInt16;
    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ResultItem = Module.Interfaces.Elements.IArticleResult;

    using ResultMonad;
    using Utils;
    using Module.Libs;
    using Config.Results;
    using Module.Items.Utils;
    using MaybeMonad;
    using Matrixus.Config;

    public static class PositionDetailsBuilder
    {
        public static string RESULT_DETAILS_INVALID_TEXT = "Invalid result details values!";
        public static string RESULT_DETAILS_INVALID_TERM = "position term item missing";
        public static string RESULT_DETAILS_INVALID_DATA = "schedule data item missing";
        private static ResultMonad.Result<PositionScheduleEvalDetail, string> BuildItem(TargetPart partCode, ResultItem termItem, ResultItem dataItem)
        {
            ArticleGeneralResult resultTerm = termItem as ArticleGeneralResult;
            ArticleGeneralResult resultData = dataItem as ArticleGeneralResult;
            if (MaybeMonadUtils.HaveAnyResultNullValue(resultTerm, resultData))
            {
                return Result.Fail<PositionScheduleEvalDetail, string>(RESULT_DETAILS_INVALID_TEXT);
            }

            Maybe<PositionFromStopValue> termValues = resultTerm.ReturnPositionTermFromStopValue();
            Maybe<MonthFromStopValue> daysValues = resultTerm.ReturnMonthFromStopValue();
            Maybe<MonthScheduleValue> workValues = resultData.ReturnTermMonthValue();
            if (MaybeMonadUtils.HaveAnyResultNoValues(termValues, daysValues, workValues))
            {
                return Result.Fail<PositionScheduleEvalDetail, string>(RESULT_DETAILS_INVALID_TEXT);
            }

            PositionFromStopValue termPosition = termValues.Value;
            MonthFromStopValue daysPosition = daysValues.Value;
            MonthScheduleValue workSchedule = workValues.Value;

            PositionScheduleEvalDetail buildResult = new PositionScheduleEvalDetail
            {
                PositionPart = partCode,
                DateFrom = termPosition.DateFrom,
                DayPeriodFrom = daysPosition.PeriodDayFrom,
                DateStop = termPosition.DateStop,
                DayPeriodStop = daysPosition.PeriodDayStop,
                PositionType = termPosition.PositionType,
                ScheduleWorks = workSchedule.HoursMonth,
            };
            return Result.Ok<PositionScheduleEvalDetail, string>(buildResult);
        }

        private static Result<IEnumerable<KeyValuePair<TargetPart, Tuple<ResultItem, ResultItem>>>, string> GetZip2Position(IEnumerable<ResultPair> positionList, IEnumerable<ResultPair> scheduleList)
        {
            Func<Result<ResultItem, string>> defaultTerm = () => (Result.Fail<ResultItem, string>(RESULT_DETAILS_INVALID_TERM));
            Func<Result<ResultItem, string>> defaultData = () => (Result.Fail<ResultItem, string>(RESULT_DETAILS_INVALID_DATA));

            Func<TargetItem, TargetItem, TargetPart> indexBuilder = (xa, xb) => (xb.Part());
            Func<TargetItem, TargetItem, int> compareTarget = (xa, xb) => (xa.Seed().CompareTo(xb.Part()));

            var positionZips = ResultMonadUtils.Zip2ToResultWithKeyValListAndError(positionList, scheduleList,
                defaultTerm, defaultData, indexBuilder, compareTarget);

            return positionZips;
        }
        public static Result<IEnumerable<PositionScheduleEvalDetail>, string> GetPositionValues(IEnumerable<ResultPair> results, ConfigCode termCode, ConfigCode dataCode, TargetHead headCode)
        {
            Result<IEnumerable<ResultPair>, string> termList = results
                .GetTypedResultsInListAndError<ArticleGeneralResult>(
                    TargetFilters.TargetCodePlusHeadFunc(termCode, headCode));
            Result<IEnumerable<ResultPair>, string> dataList = results
                .GetTypedResultsInListAndError<ArticleGeneralResult>(
                    TargetFilters.TargetCodePlusHeadFunc(dataCode, headCode));
            if (ResultMonadUtils.HaveAnyResultFailed(termList, dataList))
            {
                return Result.Fail<IEnumerable<PositionScheduleEvalDetail>, string>(
                      ResultMonadUtils.FirstFailedResultError(termList, dataList));
            }

            var zipsList = GetZip2Position(termList.Value, dataList.Value);
            if (zipsList.IsFailure)
            {
                return Result.Fail<IEnumerable<PositionScheduleEvalDetail>, string>(zipsList.Error);
            }
            var valsList = zipsList.Value.Select((tp) => (BuildItem(tp.Key, tp.Value.Item1, tp.Value.Item2))).ToList();

            return valsList.ToResultWithValueListAndError((tp) => (tp));
        }
    }
}
