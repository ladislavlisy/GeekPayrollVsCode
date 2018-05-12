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
    using SourceItem = Sources.PositionAbsenceSource;

    using Sources;
    using Concepts;
    using Module.Items;
    using Module.Libs;
    using Module.Interfaces.Elements;
    using Module.Interfaces.Legalist;
    using Utils;
    using Results;
    using Module.Codes;
    using System.Linq;
    using Module.Items.Utils;
    using MaybeMonad;
    using Legalist.Constants;
    using Module.Interfaces.Matrixus;
    using Matrixus.Config;

    public class PositionAbsenceArticle : GeneralArticle, ICloneable
    {
        protected delegate IEnumerable<ResultPack> EvaluateConceptDelegate(ConfigBase evalConfig, Period evalPeriod, IPeriodProfile evalProfile, Result<EvaluateSource, string> prepValues);

        public static string ARTICLE_DESCRIPTION_ERROR_FORMAT = "PositionAbsenceArticle(ARTICLE_POSITION_ABSENCE, 10): {0}";

        public PositionAbsenceArticle() : base((ConfigRole)ConfigRoleEnum.ARTICLE_POSITION_ABSENCE)
        {
            SourceValues = new PositionAbsenceSource();

            InternalEvaluate = PositionAbsenceConcept.EvaluateConcept;
        }

        public PositionAbsenceArticle(ISourceValues values) : this()
        {
            PositionAbsenceSource sourceValues = values as PositionAbsenceSource;

            SourceValues = CloneUtils<PositionAbsenceSource>.CloneOrNull(sourceValues);
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

        public PositionAbsenceSource SourceValues { get; set; }

        public override void ImportSourceValues(ISourceValues values)
        {
            SourceValues = SetSourceValues<PositionAbsenceSource>(values);
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
            PositionAbsenceArticle cloneArticle = (PositionAbsenceArticle)this.MemberwiseClone();

            cloneArticle.InternalConfig = CloneUtils<IArticleConfigFeatures>.CloneOrNull(this.InternalConfig);
            cloneArticle.InternalRole = this.InternalRole;
            cloneArticle.InternalEvaluate = this.InternalEvaluate;

            return cloneArticle;
        }

        public class EvaluateSource
        {
            public EvaluateSource()
            {
                PositionType = WorkPositionType.POSITION_EXCLUSIVE;
                DayPositionFrom = 0;
                DayPositionStop = 0;
                AbsenceList = new List<AbsenceEvaluateSource>();

            }
            public class AbsenceEvaluateSource
            {
                public AbsenceEvaluateSource()
                {
                    AbsenceCode = 0;
                    DayPeriodFrom = 0;
                    DayPeriodStop = 0;
                    ScheduleMonth = new TSeconds[0];
                }
                public ConfigCode AbsenceCode { get; set; }
                public TDay DayPeriodFrom { get; set; }
                public TDay DayPeriodStop { get; set; }
                public TSeconds[] ScheduleMonth { get; set; }
            }

            internal class CompareAbsenceTerms : IComparer<AbsenceEvaluateSource>
            {
                public int CompareDate(TDay x, TDay y)
                {
                    if (x!=0 && y!=0)
                    {
                        return x.CompareTo(y);
                    }
                    else if (x!=0)
                    {
                        return 1;
                    }
                    else if (y!=0)
                    {
                        return -1;
                    }
                    return 0;
                }
                public int Compare(AbsenceEvaluateSource x, AbsenceEvaluateSource y)
                {
                    int compareFrom = CompareDate(x.DayPeriodFrom, y.DayPeriodFrom);
                    if (compareFrom == 0)
                    {
                        int compareStop = CompareDate(x.DayPeriodStop, y.DayPeriodStop);
                        if (compareStop == 0)
                        {
                            return x.AbsenceCode.CompareTo(y.AbsenceCode);
                        }
                        return compareStop;
                    }
                    return compareFrom;
                }
            }
            // PROPERTIES DEF
            public WorkPositionType PositionType { get; set; }
            public TDay DayPositionFrom { get; set; }
            public TDay DayPositionStop { get; set; }
            public IList<AbsenceEvaluateSource> AbsenceList { get; set; }
            // PROPERTIES DEF
            public class SourceBuilder : EvalValuesSourceBuilder<EvaluateSource>
            {
                public SourceBuilder(ISourceValues evalValues) : base(evalValues)
                {
                }

                public override EvaluateSource GetNewValues(EvaluateSource initValues)
                {
#if GET_SOURCE_VALUE
                    SourceItem conceptValues = InternalValues as SourceItem;
                    if (conceptValues == null)
                    {
                        return ReturnFailure(initValues);
                    }
                    return new EvaluateSource
                    {
                        // PROPERTIES SET
                        // PROPERTIES SET
                    };
#else
                    return initValues;
#endif
                }
            }
            public class ResultBuilder : EvalValuesResultBuilder<EvaluateSource>
            {
                public ResultBuilder(TargetItem evalTarget, IEnumerable<ResultPair> evalResults) : base(evalTarget, evalResults)
                {
                }

                protected ResultMonad.Result<AbsenceEvaluateSource, string> BuildItem(TargetItem target, MonthAttendanceValue resultValue)
                {
                    AbsenceEvaluateSource buildResult = new AbsenceEvaluateSource
                    {
                        AbsenceCode = target.Code(),
                        DayPeriodFrom = resultValue.PeriodDayFrom,
                        DayPeriodStop = resultValue.PeriodDayStop,
                        ScheduleMonth = resultValue.HoursMonth,
                    };
                    return Result.Ok<AbsenceEvaluateSource, string>(buildResult);
                }
                private Result<IEnumerable<AbsenceEvaluateSource>, string> GetAbsenceValues()
                {
                    ConfigCode filterCode = (ConfigCode)ArticleCodeCz.FACT_CONTRACT_ATTEND_ITEM;
                    TargetHead filterHead = InternalTarget.Head();

                    Result<IEnumerable<AbsenceEvaluateSource>, string> absenceList = InternalValues
                        .GetResultValuesInListAndError<ArticleGeneralResult, MonthAttendanceValue, AbsenceEvaluateSource>(
                            TargetFilters.TargetCodePlusHeadFunc(filterCode, filterHead), ArticleFilters.SelectAllFunc,
                            ResultFilters.MonthAttendanceFunc, BuildItem);

                    return absenceList;
                }
                public override EvaluateSource GetNewValues(EvaluateSource initValues)
                {
                    ConfigCode termCode = (ConfigCode)ArticleCodeCz.FACT_POSITION_TERM;

                    Result<MonthFromStopValue, string> termFindResult = InternalValues
                        .FindResultValue<ArticleGeneralResult, MonthFromStopValue>(
                            TargetFilters.TargetCodePlusHeadAndSeedFunc(termCode, InternalTarget.Head(), InternalTarget.Part()),
                            (x) => (x.IsMonthFromStopValue()));

                    if (termFindResult.IsFailure)
                    {
                        return ReturnFailureAndError(initValues, termFindResult.Error);
                    }

                    MonthFromStopValue termPrepValues = termFindResult.Value;

                    var absenceValues = GetAbsenceValues();

                    if (absenceValues.IsFailure)
                    {
                        return ReturnFailureAndError(initValues, absenceValues.Error);
                    }

                    var completeSorted = absenceValues.Value.OrderBy((p) => (p), new CompareAbsenceTerms());

                    return new EvaluateSource
                    {
                        // PROPERTIES SET
                        DayPositionFrom = termPrepValues.PeriodDayFrom,
                        DayPositionStop = termPrepValues.PeriodDayStop,
                        AbsenceList = completeSorted.ToList(),
                        // PROPERTIES SET
                    };
                }
            }
        }
    }
}
