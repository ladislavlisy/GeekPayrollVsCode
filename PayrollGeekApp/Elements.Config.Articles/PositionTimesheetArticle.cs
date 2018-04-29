using System;
using System.Collections.Generic;
using ResultMonad;

namespace ElementsLib.Elements.Config.Articles
{
    using ConfigCodeEnum = Module.Codes.ArticleCodeCz;
    using ConfigCode = UInt16;
    using ConfigRoleEnum = Module.Codes.ArticleRoleCz;
    using ConfigRole = UInt16;

    using TDay = Byte;
    using TSeconds = Int32;

    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using TargetErrs = String;
    using SourcePack = ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ValidsPack = ResultMonad.Result<bool, string>;
    using SourceItem = Sources.PositionTimesheetSource;

    using Sources;
    using Concepts;
    using Module.Items;
    using Module.Libs;
    using Module.Interfaces.Elements;
    using Module.Interfaces.Legalist;
    using Utils;
    using Results;
    using MaybeMonad;
    using Module.Codes;
    using Module.Items.Utils;

    public class PositionTimesheetArticle : GeneralArticle, ICloneable
    {
        protected delegate IEnumerable<ResultPack> EvaluateConceptDelegate(ConfigCode evalCode, Period evalPeriod, IPeriodProfile evalProfile, Result<EvaluateSource, string> prepValues);

        public static string ARTICLE_DESCRIPTION_ERROR_FORMAT = "PositionTimesheetArticle(ARTICLE_POSITION_TIMESHEET, 8): {0}";

        public PositionTimesheetArticle() : base((ConfigRole)ConfigRoleEnum.ARTICLE_POSITION_TIMESHEET)
        {
            SourceValues = new PositionTimesheetSource();

            InternalEvaluate = PositionTimesheetConcept.EvaluateConcept;
        }

        public PositionTimesheetArticle(ISourceValues values) : this()
        {
            PositionTimesheetSource sourceValues = values as PositionTimesheetSource;

            SourceValues = CloneUtils<PositionTimesheetSource>.CloneOrNull(sourceValues);
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

        public PositionTimesheetSource SourceValues { get; set; }

        public override void ImportSourceValues(ISourceValues values)
        {
            SourceValues = SetSourceValues<PositionTimesheetSource>(values);
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
            PositionTimesheetArticle cloneArticle = (PositionTimesheetArticle)this.MemberwiseClone();

            cloneArticle.InternalCode = this.InternalCode;
            cloneArticle.InternalRole = this.InternalRole;
            cloneArticle.InternalEvaluate = this.InternalEvaluate;

            return cloneArticle;
        }

        public class EvaluateSource
        {
            // PROPERTIES DEF
            public TDay DayTermFrom { get; set; }
            public TDay DayTermStop { get; set; }
            public TSeconds[] RealMonthHours { get; set; }
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

                public override EvaluateSource GetNewValues(EvaluateSource initValues)
                {
                    ConfigCode workCode = (ConfigCode)ArticleCodeCz.FACT_POSITION_SCHEDULE;

                    Result<WorkMonthResultValue, string> workFindResult = InternalValues
                        .FindResultValueForCodePlusPart<ArticleGeneralResult, WorkMonthResultValue>(
                        workCode, InternalTarget.Head(), InternalTarget.Part(),
                        (x) => (x.IsRealMonthValue()));
                    WorkMonthResultValue workValuesPrep = workFindResult.Value;

                    ConfigCode termCode = (ConfigCode)ArticleCodeCz.FACT_POSITION_TERM;

                    Result<MonthFromStopResultValue, string> termFindResult = InternalValues
                        .FindPositionResultValueForCode<ArticleGeneralResult, MonthFromStopResultValue>(
                        termCode, InternalTarget.Head(), InternalTarget.Part(),
                        (x) => (x.IsMonthFromStopValue()));

                    if (ResultMonadUtils.HaveAnyResultFailed(workFindResult, workFindResult))
                    {
                        return ReturnFailure(initValues);
                    }

                    MonthFromStopResultValue termValuesPrep = termFindResult.Value;

                    return new EvaluateSource
                    {
                        // PROPERTIES SET
                        DayTermFrom = termValuesPrep.PeriodDayFrom,
                        DayTermStop = termValuesPrep.PeriodDayStop,
                        RealMonthHours = workValuesPrep.HoursMonth
                        // PROPERTIES SET
                    };
                }
            }
        }
    }
}
