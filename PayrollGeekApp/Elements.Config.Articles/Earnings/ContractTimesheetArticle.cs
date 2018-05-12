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

    using TargetHead = UInt16;
    using TargetPart = UInt16;
    using TDay = Byte;
    using TSeconds = Int32;

    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using TargetErrs = String;
    using SourcePack = ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ResultItem = Module.Interfaces.Elements.IArticleResult;
    using ValidsPack = ResultMonad.Result<bool, string>;
    using SourceItem = Sources.ContractTimesheetSource;

    using Sources;
    using Concepts;
    using Module.Items;
    using Module.Libs;
    using Module.Interfaces.Elements;
    using Module.Interfaces.Legalist;
    using Utils;
    using Results;
    using Legalist.Constants;
    using Module.Codes;
    using System.Linq;
    using ResultMonad.Extensions.ResultWithValueAndErrorMonad.OnSuccess;
    using MaybeMonad;
    using Module.Items.Utils;
    using Module.Interfaces.Matrixus;
    using Matrixus.Config;

    public class ContractTimesheetArticle : GeneralArticle, ICloneable
    {
        protected delegate IEnumerable<ResultPack> EvaluateConceptDelegate(ConfigBase evalConfig, Period evalPeriod, IPeriodProfile evalProfile, Result<EvaluateSource, string> prepValues);

        public static string ARTICLE_DESCRIPTION_ERROR_FORMAT = "ContractTimesheetArticle(ARTICLE_CONTRACT_TIMESHEET, 2): {0}";

        public ContractTimesheetArticle() : base((ConfigRole)ConfigRoleEnum.ARTICLE_CONTRACT_TIMESHEET)
        {
            SourceValues = new ContractTimesheetSource();

            InternalEvaluate = ContractTimesheetConcept.EvaluateConcept;
        }

        public ContractTimesheetArticle(ISourceValues values) : this()
        {
            ContractTimesheetSource sourceValues = values as ContractTimesheetSource;

            SourceValues = CloneUtils<ContractTimesheetSource>.CloneOrNull(sourceValues);
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

        public ContractTimesheetSource SourceValues { get; set; }

        public override void ImportSourceValues(ISourceValues values)
        {
            SourceValues = SetSourceValues<ContractTimesheetSource>(values);
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
            ContractTimesheetArticle cloneArticle = (ContractTimesheetArticle)this.MemberwiseClone();

            cloneArticle.InternalConfig = CloneUtils<IArticleConfigFeatures>.CloneOrNull(this.InternalConfig);
            cloneArticle.InternalRole = this.InternalRole;
            cloneArticle.InternalEvaluate = this.InternalEvaluate;

            return cloneArticle;
        }

        public class EvaluateSource
        {
            public EvaluateSource()
            {
                PositionList = new List<PositionEvaluateSource>();
            }
            public class PositionEvaluateSource
            {
                public PositionEvaluateSource()
                {
                    PositionPart = 0;
                    DateFrom = null;
                    DayPeriodFrom = 0;
                    DateStop = null;
                    DayPeriodStop = 0;
                    PositionType = WorkPositionType.POSITION_EXCLUSIVE;
                    ScheduleMonth = new TSeconds[0];
                    ScheduleLimit = new TSeconds[0];
                }
                public TargetPart PositionPart { get; set; }
                public DateTime? DateFrom { get; set; }
                public TDay DayPeriodFrom { get; set; }
                public DateTime? DateStop { get; set; }
                public TDay DayPeriodStop { get; set; }
                public WorkPositionType PositionType { get; set; }
                public TSeconds[] ScheduleMonth { get; set; }
                public TSeconds[] ScheduleLimit { get; set; }
            }

            internal class ComparePositionTerms : IComparer<PositionEvaluateSource>
            {
                public int CompareDate(DateTime? x, DateTime? y)
                {
                    if (x.HasValue && y.HasValue)
                    {
                        DateTime xv = x.Value;
                        DateTime yv = y.Value;

                        return xv.CompareTo(yv);
                    }
                    else if (x.HasValue)
                    {
                        return 1;
                    }
                    else if (y.HasValue)
                    {
                        return -1;
                    }
                    return 0;
                }
                public int Compare(PositionEvaluateSource x, PositionEvaluateSource y)
                {
                    int compareFrom = CompareDate(x.DateFrom, y.DateFrom);
                    if (compareFrom == 0)
                    {
                        int compareStop = CompareDate(x.DateStop, y.DateStop);
                        if (compareStop == 0)
                        {
                            return x.PositionPart.CompareTo(y.PositionPart);
                        }
                        return compareStop;
                    }
                    return compareFrom;
                }
            }
            // PROPERTIES DEF
            public IList<PositionEvaluateSource> PositionList { get; set; }
            // PROPERTIES DEF
            public class SourceBuilder : EvalValuesSourceBuilder<EvaluateSource>
            {
                public SourceBuilder(ISourceValues evalValues) : base(evalValues)
                {
                }

                public override EvaluateSource GetNewValues(EvaluateSource initValues)
                {
                    // PROPERTIES SET
                    // PROPERTIES SET
                    return initValues;
                }
            }
            public class ResultBuilder : EvalValuesResultBuilder<EvaluateSource>
            {
                public ResultBuilder(TargetItem evalTarget, IEnumerable<ResultPair> evalResults) : base(evalTarget, evalResults)
                {
                }

                protected ResultMonad.Result<PositionEvaluateSource, string> BuildItem(TargetPart part, ResultItem resultTerm, ResultItem resultWork)
                {
                    ArticleGeneralResult termResult = resultTerm as ArticleGeneralResult;
                    ArticleGeneralResult workResult = resultWork as ArticleGeneralResult;
                    if (MaybeMonadUtils.HaveAnyResultNullValue(termResult, workResult))
                    {
                        return Result.Fail<PositionEvaluateSource, string>(CONCEPT_RESULT_INVALID_TEXT);
                    }

                    Maybe<PositionFromStopValue> termValues = termResult.ReturnPositionTermFromStopValue();
                    Maybe<MonthFromStopValue> daysValues = termResult.ReturnMonthFromStopValue();
                    Maybe<MonthScheduleValue> realValues = workResult.ReturnRealMonthValue();
                    Maybe<MonthScheduleValue> restValues = workResult.ReturnTermMonthValue();
                    if (MaybeMonadUtils.HaveAnyResultNoValues(termValues, daysValues, realValues, restValues))
                    {
                        return Result.Fail<PositionEvaluateSource, string>(CONCEPT_RESULT_INVALID_TEXT);
                    }

                    PositionFromStopValue termPosition = termValues.Value;
                    MonthFromStopValue daysPosition = daysValues.Value;
                    MonthScheduleValue realSchedule = realValues.Value;
                    MonthScheduleValue restSchedule = restValues.Value;

                    PositionEvaluateSource buildResult = new PositionEvaluateSource
                    {
                        PositionPart = part,
                        DateFrom = termPosition.DateFrom,
                        DayPeriodFrom = daysPosition.PeriodDayFrom,
                        DateStop = termPosition.DateStop,
                        DayPeriodStop = daysPosition.PeriodDayStop,
                        PositionType = termPosition.PositionType,
                        ScheduleMonth = realSchedule.HoursMonth,
                        ScheduleLimit = restSchedule.HoursMonth
                    };
                    return Result.Ok<PositionEvaluateSource, string>(buildResult);
                }

                private Result<IEnumerable<KeyValuePair<TargetPart, Tuple<ResultItem, ResultItem>>>, string> GetZip2Position(IEnumerable<ResultPair> positionList, IEnumerable<ResultPair> scheduleList)
                {
                    Func<Result<ResultItem, string>> defaultPosition = () => (Result.Fail<ResultItem, string>("position missing"));
                    Func<Result<ResultItem, string>> defaultSchedule = () => (Result.Fail<ResultItem, string>("schedule missing"));

                    Func<TargetItem, TargetItem, TargetPart> indexBuilder = (xa, xb) => (xb.Part());
                    Func<TargetItem, TargetItem, int> compareTarget = (xa, xb) => (xa.Seed().CompareTo(xb.Part()));

                    var positionZips = ResultMonadUtils.Zip2ToResultWithKeyValListAndError(positionList, scheduleList, 
                        defaultPosition, defaultSchedule, indexBuilder, compareTarget);

                    return positionZips;
                }
                private Result<IEnumerable<PositionEvaluateSource>, string> GetPositionValues()
                {
                    ConfigCode positionCode = (ConfigCode)ArticleCodeCz.FACT_POSITION_TERM;
                    ConfigCode scheduleCode = (ConfigCode)ArticleCodeCz.FACT_POSITION_TIMESHEET;

                    Result<IEnumerable<ResultPair>, string> positionList = InternalValues
                        .GetTypedResultsInListAndError<ArticleGeneralResult>(
                            TargetFilters.TargetCodePlusHeadFunc(positionCode, InternalTarget.Head()));
                    Result<IEnumerable<ResultPair>, string> scheduleList = InternalValues
                        .GetTypedResultsInListAndError<ArticleGeneralResult>(
                            TargetFilters.TargetCodePlusHeadFunc(scheduleCode, InternalTarget.Head()));
                    if (ResultMonadUtils.HaveAnyResultFailed(positionList, scheduleList))
                    {
                        return Result.Fail<IEnumerable<PositionEvaluateSource>, string>(
                            ResultMonadUtils.FirstFailedResultError(positionList, scheduleList));
                    }

                    var positionZips = GetZip2Position(positionList.Value, scheduleList.Value);
                    if (positionZips.IsFailure)
                    {
                        return Result.Fail<IEnumerable<PositionEvaluateSource>, string>(positionZips.Error);
                    }
                    var positionStream = positionZips.Value.Select((tp) => (BuildItem(tp.Key, tp.Value.Item1, tp.Value.Item2))).ToList();

                    return positionStream.ToResultWithValueListAndError((tp) => (tp));
                }
                public override EvaluateSource GetNewValues(EvaluateSource initValues)
                {
                    var positionValues = GetPositionValues();

                    if (positionValues.IsFailure)
                    {
                        return ReturnFailureAndError(initValues, positionValues.Error);
                    }

                    var completeSorted = positionValues.Value.OrderBy((p) => (p), new ComparePositionTerms());

                    return new EvaluateSource
                    {
                        // PROPERTIES SET
                        PositionList = completeSorted.ToList(),
                        // PROPERTIES SET
                    };
                }
            }
        }
    }
}
