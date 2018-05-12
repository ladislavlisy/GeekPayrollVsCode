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

    using TDay = Byte;
    using TSeconds = Int32;
    using TAmountDec = Decimal;

    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using TargetErrs = String;
    using SourcePack = ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ValidsPack = ResultMonad.Result<bool, string>;
    using SourceItem = Sources.MonthlyAmountSource;

    using Sources;
    using Concepts;
    using Module.Items;
    using Module.Libs;
    using Module.Interfaces.Elements;
    using Module.Interfaces.Legalist;
    using Utils;
    using Results;
    using Module.Codes;
    using Module.Items.Utils;
    using Module.Interfaces.Matrixus;
    using Matrixus.Config;

    public class PositionMonthlyAmountArticle : GeneralArticle, ICloneable
    {
        protected delegate IEnumerable<ResultPack> EvaluateConceptDelegate(ConfigBase evalConfig, Period evalPeriod, IPeriodProfile evalProfile, Result<EvaluateSource, string> prepValues);

        public static string ARTICLE_DESCRIPTION_ERROR_FORMAT = "PositionMonthlyAmountArticle(ARTICLE_POSITION_MONTHLY_AMOUNT, 1000): {0}";

        public PositionMonthlyAmountArticle() : base((ConfigRole)ConfigRoleEnum.ARTICLE_POSITION_MONTHLY_AMOUNT)
        {
            SourceValues = new MonthlyAmountSource();

            InternalEvaluate = PositionMonthlyAmountConcept.EvaluateConcept;
        }

        public PositionMonthlyAmountArticle(ISourceValues values) : this()
        {
            MonthlyAmountSource sourceValues = values as MonthlyAmountSource;

            SourceValues = CloneUtils<MonthlyAmountSource>.CloneOrNull(sourceValues);
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

        public MonthlyAmountSource SourceValues { get; set; }

        public override void ImportSourceValues(ISourceValues values)
        {
            SourceValues = SetSourceValues<MonthlyAmountSource>(values);
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
            PositionMonthlyAmountArticle cloneArticle = (PositionMonthlyAmountArticle)this.MemberwiseClone();

            cloneArticle.InternalConfig = CloneUtils<IArticleConfigFeatures>.CloneOrNull(this.InternalConfig);
            cloneArticle.InternalRole = this.InternalRole;
            cloneArticle.InternalEvaluate = this.InternalEvaluate;

            return cloneArticle;
        }

        public class EvaluateSource
        {
            public EvaluateSource()
            {
                MonthlyAmount = 0m;
                ShiftLiable = 0;
                ShiftWorked = 0;
                HoursLiable = 0;
                HoursWorked = 0;
            }
            // PROPERTIES DEF
            public TAmountDec MonthlyAmount { get; set; }
            public TSeconds ShiftLiable { get; set; }
            public TSeconds ShiftWorked { get; set; }
            public TSeconds HoursLiable { get; set; }
            public TSeconds HoursWorked { get; set; }
            // PROPERTIES DEF
            public class SourceBuilder : EvalValuesSourceBuilder<EvaluateSource>
            {
                public SourceBuilder(ISourceValues evalValues) : base(evalValues)
                {
                }

                public override EvaluateSource GetNewValues(EvaluateSource initValues)
                {
                    SourceItem conceptValues = InternalValues as SourceItem;
                    if (conceptValues == null)
                    {
                        return ReturnFailure(initValues);
                    }
                    return new EvaluateSource
                    {
                        // PROPERTIES SET
                        MonthlyAmount = conceptValues.MonthlyAmount,
                        ShiftLiable = 0,
                        ShiftWorked = 0,
                        HoursLiable = 0,
                        HoursWorked = 0,
                        // PROPERTIES SET
                    };
                }
            }
            public class ResultBuilder : EvalValuesResultBuilder<EvaluateSource>
            {
                public ResultBuilder(TargetItem evalTarget, IEnumerable<ResultPair> evalResults) : base(evalTarget, evalResults)
                {
                }

                public override EvaluateSource GetNewValues(EvaluateSource initValues)
                {
                    ConfigCode scheduledCode = (ConfigCode)ArticleCodeCz.FACT_POSITION_SCHEDULE;
                    ConfigCode timesheetCode = (ConfigCode)ArticleCodeCz.FACT_POSITION_TIMESHEET;
                    ConfigCode worksheetCode = (ConfigCode)ArticleCodeCz.FACT_POSITION_WORKING;

                    Result<WeekScheduleValue, string> fullWeekResult = InternalValues
                        .FindResultValue<ArticleGeneralResult, WeekScheduleValue>(
                        TargetFilters.TargetCodePlusPartFunc(scheduledCode, InternalTarget.Head(), InternalTarget.Part()),
                        (x) => (x.IsFullWeeksValue()));
                    Result<WeekScheduleValue, string> realWeekResult = InternalValues
                        .FindResultValue<ArticleGeneralResult, WeekScheduleValue>(
                        TargetFilters.TargetCodePlusPartFunc(scheduledCode, InternalTarget.Head(), InternalTarget.Part()),
                        (x) => (x.IsRealWeeksValue()));
                    Result<MonthScheduleValue, string> timesheetResult = InternalValues
                        .FindResultValue<ArticleGeneralResult, MonthScheduleValue>(
                        TargetFilters.TargetCodePlusPartFunc(timesheetCode, InternalTarget.Head(), InternalTarget.Part()),
                        (x) => (x.IsRealMonthValue()));
                    Result<MonthScheduleValue, string> worksheetResult = InternalValues
                        .FindResultValue<ArticleGeneralResult, MonthScheduleValue>(
                        TargetFilters.TargetCodePlusPartFunc(worksheetCode, InternalTarget.Head(), InternalTarget.Part()),
                        (x) => (x.IsTermMonthValue()));

                    if (ResultMonadUtils.HaveAnyResultFailed(
                        fullWeekResult, 
                        realWeekResult, 
                        timesheetResult, 
                        worksheetResult))
                    {
                        return ReturnFailureAndError(initValues,
                            ResultMonadUtils.FirstFailedResultError(
                                fullWeekResult, 
                                realWeekResult, 
                                timesheetResult, 
                                worksheetResult));
                    }

                    WeekScheduleValue fullWeekValues = fullWeekResult.Value;
                    WeekScheduleValue realWeekValues = realWeekResult.Value;
                    MonthScheduleValue timesheetValues = timesheetResult.Value;
                    MonthScheduleValue worksheetValues = worksheetResult.Value;

                    return new EvaluateSource
                    {
                        // PROPERTIES SET
                        MonthlyAmount = initValues.MonthlyAmount,
                        ShiftLiable = PeriodUtils.TotalWeekHours(fullWeekValues.HoursWeek),
                        ShiftWorked = PeriodUtils.TotalWeekHours(realWeekValues.HoursWeek),
                        HoursLiable = PeriodUtils.TotalMonthHours(timesheetValues.HoursMonth),
                        HoursWorked = PeriodUtils.TotalMonthHours(worksheetValues.HoursMonth),
                        // PROPERTIES SET
                    };
                }
            }
        }
    }
}
