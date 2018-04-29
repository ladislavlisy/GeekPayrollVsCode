using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Elements.Config.Articles
{
    using ConfigCodeEnum = Module.Codes.ArticleCodeCz;
    using ConfigCode = UInt16;
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
    using SourceItem = Sources.ContractWorkingSource;

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
    using Module.Items.Utils;
    using ResultMonad;
    using MaybeMonad;

    public class ContractWorkingArticle : GeneralArticle, ICloneable
    {
        protected delegate IEnumerable<ResultPack> EvaluateConceptDelegate(ConfigCode evalCode, Period evalPeriod, IPeriodProfile evalProfile, Result<EvaluateSource, string> prepValues);

        public static string ARTICLE_DESCRIPTION_ERROR_FORMAT = "ContractWorkingArticle(ARTICLE_CONTRACT_WORKING, 3): {0}";

        public ContractWorkingArticle() : base((ConfigRole)ConfigRoleEnum.ARTICLE_CONTRACT_WORKING)
        {
            SourceValues = new ContractWorkingSource();

            InternalEvaluate = ContractWorkingConcept.EvaluateConcept;
        }

        public ContractWorkingArticle(ISourceValues values) : this()
        {
            ContractWorkingSource sourceValues = values as ContractWorkingSource;

            SourceValues = CloneUtils<ContractWorkingSource>.CloneOrNull(sourceValues);
        }

        protected EvaluateConceptDelegate InternalEvaluate { get; set; }

        protected override IEnumerable<ResultPack> EvaluateArticleResults(TargetItem evalTarget, ConfigCode evalCode, ISourceValues evalValues, Period evalPeriod, IPeriodProfile evalProfile, IEnumerable<ResultPair> evalResults)
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
            return InternalEvaluate(evalCode, evalPeriod, evalProfile, bundleValues);
        }

        public ContractWorkingSource SourceValues { get; set; }

        public override void ImportSourceValues(ISourceValues values)
        {
            SourceValues = SetSourceValues<ContractWorkingSource>(values);
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
            ContractWorkingArticle cloneArticle = (ContractWorkingArticle)this.MemberwiseClone();

            cloneArticle.InternalCode = this.InternalCode;
            cloneArticle.InternalRole = this.InternalRole;
            cloneArticle.InternalEvaluate = this.InternalEvaluate;

            return cloneArticle;
        }

        public class EvaluateSource
        {
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
                    ScheduleWorks = new TSeconds[0];
                }
                public TargetPart PositionPart { get; set; }
                public DateTime? DateFrom { get; set; }
                public TDay DayPeriodFrom { get; set; }
                public DateTime? DateStop { get; set; }
                public TDay DayPeriodStop { get; set; }
                public WorkPositionType PositionType { get; set; }
                public TSeconds[] ScheduleWorks { get; set; }
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

                    Maybe<TermFromStopPositionValue> termValues = termResult.ReturnPositionTermFromStopValue();
                    Maybe<MonthFromStopResultValue> daysValues = termResult.ReturnMonthFromStopValue();
                    Maybe<WorkMonthResultValue> workValues = workResult.ReturnTermMonthValue();
                    if (MaybeMonadUtils.HaveAnyResultNoValues(termValues, daysValues, workValues))
                    {
                        return Result.Fail<PositionEvaluateSource, string>(CONCEPT_RESULT_INVALID_TEXT);
                    }

                    TermFromStopPositionValue termPosition = termValues.Value;
                    MonthFromStopResultValue daysPosition = daysValues.Value;
                    WorkMonthResultValue workSchedule = workValues.Value;

                    PositionEvaluateSource buildResult = new PositionEvaluateSource
                    {
                        PositionPart = part,
                        DateFrom = termPosition.DateFrom,
                        DayPeriodFrom = daysPosition.PeriodDayFrom,
                        DateStop = termPosition.DateStop,
                        DayPeriodStop = daysPosition.PeriodDayStop,
                        PositionType = termPosition.PositionType,
                        ScheduleWorks = workSchedule.HoursMonth,
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
                    ConfigCode scheduleCode = (ConfigCode)ArticleCodeCz.FACT_POSITION_WORKING;

                    IEnumerable<ResultPair> positionGets = InternalValues.GetResultForCodePlusHead(positionCode, InternalTarget.Head());
                    IEnumerable<ResultPair> positionList = positionGets.OrderBy((c) => (c.Key.Seed()));

                    IEnumerable<ResultPair> scheduleGets = InternalValues.GetResultForCodePlusHead(scheduleCode, InternalTarget.Head());
                    IEnumerable<ResultPair> scheduleList = scheduleGets.OrderBy((c) => (c.Key.Part()));

                    var positionZips = GetZip2Position(positionList, scheduleList);
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
                        PositionList = completeSorted.ToList()
                        // PROPERTIES SET
                    };
                }
            }
        }
    }
}
