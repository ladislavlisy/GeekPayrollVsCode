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

    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using TargetErrs = String;
    using SourcePack = ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ValidsPack = ResultMonad.Result<bool, string>;
    using SourceItem = Sources.ContractAttendItemSource;

    using Sources;
    using Concepts;
    using Module.Items;
    using Module.Libs;
    using Module.Interfaces.Elements;
    using Module.Interfaces.Legalist;
    using Utils;
    using Results;
    using Module.Codes;
    using Legalist.Constants;
    using System.Linq;
    using Module.Interfaces.Matrixus;
    using Matrixus.Config;

    public class ContractAttendItemArticle : GeneralArticle, ICloneable
    {
        protected delegate IEnumerable<ResultPack> EvaluateConceptDelegate(ConfigBase evalConfig, Period evalPeriod, IPeriodProfile evalProfile, Result<EvaluateSource, string> prepValues);

        public static string ARTICLE_DESCRIPTION_ERROR_FORMAT = "ContractAttendItemArticle(ARTICLE_CONTRACT_ATTEND_ITEM, 5): {0}";

        public ContractAttendItemArticle() : base((ConfigRole)ConfigRoleEnum.ARTICLE_CONTRACT_ATTEND_ITEM)
        {
            SourceValues = new ContractAttendItemSource();

            InternalEvaluate = ContractAttendItemConcept.EvaluateConcept;
        }

        public ContractAttendItemArticle(ISourceValues values) : this()
        {
            ContractAttendItemSource sourceValues = values as ContractAttendItemSource;

            SourceValues = CloneUtils<ContractAttendItemSource>.CloneOrNull(sourceValues);
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

        public ContractAttendItemSource SourceValues { get; set; }

        public override void ImportSourceValues(ISourceValues values)
        {
            SourceValues = SetSourceValues<ContractAttendItemSource>(values);
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
            ContractAttendItemArticle cloneArticle = (ContractAttendItemArticle)this.MemberwiseClone();

            cloneArticle.InternalConfig = CloneUtils<IArticleConfigFeatures>.CloneOrNull(this.InternalConfig);
            cloneArticle.InternalRole = this.InternalRole;
            cloneArticle.InternalEvaluate = this.InternalEvaluate;

            return cloneArticle;
        }

        public class EvaluateSource
        {
            public EvaluateSource()
            {
                AbsenceCode = 0;
                DayFrom = 0;
                DayStop = 0;
                SchedulePiece = new WorkDayPieceType[0];
                ScheduleHours = new TSeconds[0];
                ScheduleMonth = new TSeconds[0];
            }
            // PROPERTIES DEF
            public ConfigCode AbsenceCode { get; set; }
            public TDay DayFrom { get; set; }
            public TDay DayStop { get; set; }
            public WorkDayPieceType[] SchedulePiece { get; set; }
            public TSeconds[] ScheduleHours { get; set; }
            public TSeconds[] ScheduleMonth { get; set; }
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
                        AbsenceCode = 0,
                        DayFrom = conceptValues.DayFrom,
                        DayStop = conceptValues.DayStop,
                        SchedulePiece = conceptValues.PieceInDays.ToArray(),
                        ScheduleHours = conceptValues.HoursInDays.ToArray(),
                        ScheduleMonth = new TSeconds[0],
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
                    ConfigCode scheduleCode = (ConfigCode)ArticleCodeCz.FACT_CONTRACT_TIMESHEET;

                    Result<MonthScheduleValue, string> scheduleResult = InternalValues
                        .FindResultValue<ArticleGeneralResult, MonthScheduleValue>(
                        TargetFilters.TargetCodePlusPartFunc(scheduleCode, InternalTarget.Head(), InternalTarget.Part()),
                        (x) => (x.IsRealMonthValue()));

                    if (scheduleResult.IsFailure)
                    {
                        return ReturnFailureAndError(initValues, scheduleResult.Error);
                    }

                    MonthScheduleValue scheduleValues = scheduleResult.Value;

                    return new EvaluateSource
                    {
                        // PROPERTIES SET
                        AbsenceCode = initValues.AbsenceCode,
                        DayFrom = initValues.DayFrom,
                        DayStop = initValues.DayStop,
                        SchedulePiece = initValues.SchedulePiece,
                        ScheduleHours = initValues.ScheduleHours,
                        ScheduleMonth = scheduleValues.HoursMonth.ToArray(),
                        // PROPERTIES SET
                    };
                }
            }
        }
    }
}
