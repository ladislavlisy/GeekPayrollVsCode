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

    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using TargetErrs = String;
    using SourcePack = ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ValidsPack = ResultMonad.Result<bool, string>;
    using SourceItem = Sources.PositionTermSource;
    using ResultType = ResultMonad.Result<Results.ArticleGeneralResult, string>;

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
    using Legalist.Constants;

    public class PositionTermArticle : GeneralArticle, ICloneable
    {
        protected delegate IEnumerable<ResultPack> EvaluateConceptDelegate(ConfigCode evalCode, Period evalPeriod, IPeriodProfile evalProfile, Result<EvaluateSource, string> prepValues);

        public static string ARTICLE_DESCRIPTION_ERROR_FORMAT = "PositionTermArticle(ARTICLE_POSITION_TERM, 6): {0}";

        public PositionTermArticle() : base((ConfigRole)ConfigRoleEnum.ARTICLE_POSITION_TERM)
        {
            SourceValues = new PositionTermSource();

            InternalEvaluate = PositionTermConcept.EvaluateConcept;
        }

        public PositionTermArticle(ISourceValues values) : this()
        {
            PositionTermSource sourceValues = values as PositionTermSource;

            SourceValues = CloneUtils<PositionTermSource>.CloneOrNull(sourceValues);
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

        public PositionTermSource SourceValues { get; set; }

        public override void ImportSourceValues(ISourceValues values)
        {
            SourceValues = SetSourceValues<PositionTermSource>(values);
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
            PositionTermArticle cloneArticle = (PositionTermArticle)this.MemberwiseClone();

            cloneArticle.InternalCode = this.InternalCode;
            cloneArticle.InternalRole = this.InternalRole;
            cloneArticle.InternalEvaluate = this.InternalEvaluate;

            return cloneArticle;
        }

        public class EvaluateSource
        {
            public EvaluateSource()
            {
                DateTermFrom = null;
                DateTermStop = null;
                PositionType = WorkPositionType.POSITION_EXCLUSIVE;
                DayContractFrom = 0;
                DayContractStop = 0;
            }
            // PROPERTIES DEF
            public DateTime? DateTermFrom { get; set; }
            public DateTime? DateTermStop { get; set; }
            public WorkPositionType PositionType {get; set;}
            public TDay DayContractFrom { get; set; }
            public TDay DayContractStop { get; set; }
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
                        DateTermFrom = conceptValues.DateFrom,
                        DateTermStop = conceptValues.DateStop,
                        PositionType = conceptValues.PositionType
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
                    ConfigCode termCode = (ConfigCode)ArticleCodeCz.FACT_CONTRACT_TERM;

                    Result<MonthFromStopResultValue, string> termFindResult = InternalValues
                        .FindContractResultValueForCode<ArticleGeneralResult, MonthFromStopResultValue>(
                        termCode, InternalTarget.Head(), 
                        (x) => (x.IsMonthFromStopValue()));

                    if (termFindResult.IsFailure)
                    {
                        return ReturnFailure(initValues);
                    }

                    MonthFromStopResultValue termPrepValues = termFindResult.Value;

                    return new EvaluateSource
                    {
                        // PROPERTIES SET
                        DateTermFrom = initValues.DateTermFrom,
                        DateTermStop = initValues.DateTermStop,
                        PositionType = initValues.PositionType,
                        DayContractFrom = termPrepValues.PeriodDayFrom,
                        DayContractStop = termPrepValues.PeriodDayStop
                        // PROPERTIES SET
                    };
                }
            }
        }
    }
}
