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
    using SourceItem = Sources.PositionWorkingSource;

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

    public class PositionWorkingArticle : GeneralArticle, ICloneable
    {
        protected delegate IEnumerable<ResultPack> EvaluateConceptDelegate(ConfigBase evalConfig, Period evalPeriod, IPeriodProfile evalProfile, Result<EvaluateSource, string> prepValues);

        public static string ARTICLE_DESCRIPTION_ERROR_FORMAT = "PositionWorkingArticle(ARTICLE_POSITION_WORKING, 9): {0}";

        public PositionWorkingArticle() : base((ConfigRole)ConfigRoleEnum.ARTICLE_POSITION_WORKING)
        {
            SourceValues = new PositionWorkingSource();

            InternalEvaluate = PositionWorkingConcept.EvaluateConcept;
        }

        public PositionWorkingArticle(ISourceValues values) : this()
        {
            PositionWorkingSource sourceValues = values as PositionWorkingSource;

            SourceValues = CloneUtils<PositionWorkingSource>.CloneOrNull(sourceValues);
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

        public PositionWorkingSource SourceValues { get; set; }

        public override void ImportSourceValues(ISourceValues values)
        {
            SourceValues = SetSourceValues<PositionWorkingSource>(values);
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
            PositionWorkingArticle cloneArticle = (PositionWorkingArticle)this.MemberwiseClone();

            cloneArticle.InternalConfig = CloneUtils<IArticleConfigFeatures>.CloneOrNull(this.InternalConfig);
            cloneArticle.InternalRole = this.InternalRole;
            cloneArticle.InternalEvaluate = this.InternalEvaluate;

            return cloneArticle;
        }

        public class EvaluateSource
        {
            public EvaluateSource()
            {
                ScheduleMonth = new TSeconds[0];
                AbsencesMonth = new TSeconds[0];
            }
            // PROPERTIES DEF
            public TSeconds[] ScheduleMonth { get; set; }
            public TSeconds[] AbsencesMonth { get; set; }
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

                public override EvaluateSource GetNewValues(EvaluateSource initValues)
                {
                    ConfigCode scheduleCode = (ConfigCode)ArticleCodeCz.FACT_POSITION_TIMESHEET;
                    ConfigCode absencesCode = (ConfigCode)ArticleCodeCz.FACT_POSITION_ABSENCE;

                    Result<MonthScheduleValue, string> scheduleResult = InternalValues
                        .FindResultValue<ArticleGeneralResult, MonthScheduleValue>(
                        TargetFilters.TargetCodePlusPartFunc(scheduleCode, InternalTarget.Head(), InternalTarget.Part()),
                        (x) => (x.IsTermMonthValue()));

                    Result<MonthScheduleValue, string> absencesResult = InternalValues
                        .FindResultValue<ArticleGeneralResult, MonthScheduleValue>(
                        TargetFilters.TargetCodePlusPartFunc(absencesCode, InternalTarget.Head(), InternalTarget.Part()),
                        (x) => (x.IsTermMonthValue()));

                    if (ResultMonadUtils.HaveAnyResultFailed(scheduleResult, absencesResult))
                    {
                        return ReturnFailureAndError(initValues, 
                            ResultMonadUtils.FirstFailedResultError(scheduleResult, absencesResult));
                    }

                    MonthScheduleValue scheduleValues = scheduleResult.Value;
                    MonthScheduleValue absencesValues = absencesResult.Value;

                    return new EvaluateSource
                    {
                        // PROPERTIES SET
                        ScheduleMonth = scheduleValues.HoursMonth,
                        AbsencesMonth = absencesValues.HoursMonth,
                        // PROPERTIES SET
                    };
                }
            }
        }
    }
}
