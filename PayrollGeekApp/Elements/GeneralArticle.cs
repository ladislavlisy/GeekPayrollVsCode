using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Elements
{
    using ConfigCode = UInt16;
    using ConfigRole = UInt16;

    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using SourcePack = ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ValidsPack = ResultMonad.Result<bool, string>;

    using Module.Codes;
    using Module.Interfaces.Elements;
    using Module.Items;
    using Module.Interfaces.Legalist;
    using Utils;
    using ResultMonad;

    public abstract class GeneralArticle : IArticleSource, ICloneable
    {
        public static string EXCEPTION_RESULT_NONE_TEXT = "EvaluateConceptDelegate is not implemented!";
        public static string EXCEPTION_VALUES_NULL_TEXT = "Source values are null!";
        public static string EXCEPTION_EXPERT_NULL_TEXT = "Expert profile is null!";
        public static string EXCEPTION_PERIOD_NULL_TEXT = "Period is null!";
        public static string EXCEPTION_TARGET_NULL_TEXT = "Target is null!";
        public static string EXCEPTION_RESULT_NULL_TEXT = "List of Results is null!";

        public abstract string ArticleDecorateMessage(string message);
        public abstract void ImportSourceValues(ISourceValues values);
        public abstract ISourceValues ExportSourceValues();
        public GeneralArticle(ConfigRole role)
        {
            InternalCode = 0;

            InternalRole = role;
        }

        protected ConfigCode InternalCode { get; set; }
        protected ConfigRole InternalRole { get; set; }

        public ConfigCode Code()
        {
            return InternalCode;
        }
        public ConfigCode Role()
        {
            return InternalRole;
        }

        public void SetSourceCode(ConfigCode code)
        {
            InternalCode = code;
        }
        public T SetSourceValues<T>(ISourceValues values) where T : class, ICloneable
        {
            T sourceValues = values as T;

            if (sourceValues == null)
            {
                return null;
            }
            return (T)sourceValues.Clone();
        }

        public SourcePack CloneSourceAndSetValues<T>(ConfigCode configCode, ISourceValues values) where T : class, IArticleSource
        {
            T cloneArticle = (T)Clone();

            try
            {
                cloneArticle.ImportSourceValues(values);
                cloneArticle.SetSourceCode(configCode);
            }
            catch (Exception e)
            {
                return SourcesUtils.Error(e.ToString());
            }

            return SourcesUtils.Ok(cloneArticle);
        }
        public IEnumerable<ResultPack> ArticleDecorateResultError(string message)
        {
            return EvaluateUtils.Error(ArticleDecorateMessage(message));
        }
        public IEnumerable<ResultPack> EvaluateResults(TargetItem evalTarget, Period evalPeriod, IPeriodProfile evalProfile, IEnumerable<ResultPair> evalResults)
        {
            ValidsPack validEvaluate = ValidateEvaluateIntent(evalTarget, evalPeriod, evalProfile, evalResults);
            if (validEvaluate.IsFailure)
            {
                return ArticleDecorateResultError(validEvaluate.Error);
            }
            ISourceValues evalValues = ExportSourceValues();

            return EvaluateArticleResults(evalTarget, InternalCode, evalValues, evalPeriod, evalProfile, evalResults);
            //return InternalEvaluate(evalTarget, InternalCode, evalValues, evalPeriod, evalProfile, evalResults);
        }
        protected ResultMonad.Result<ET, string> PrepareConceptValues<ET>(EvalValuesBuilder<ET> sourceBuilder, EvalValuesBuilder<ET> resultBuilder) where ET : class, new()
        {
            ResultMonad.Result<ET, string> initValues = Result.Ok<ET, string>(new ET());

            IList<EvalValuesBuilder<ET>> evalBuilders = new List<EvalValuesBuilder<ET>>()
            {
                sourceBuilder, resultBuilder,
            };

            return evalBuilders.Aggregate(initValues, (agr, x) => (x.GetValues(agr)));
        }
        protected abstract IEnumerable<ResultPack> EvaluateArticleResults(TargetItem evalTarget, ConfigCode evalCode, ISourceValues evalValues, Period evalPeriod, IPeriodProfile evalProfile, IEnumerable<ResultPair> evalResults);

        public virtual ValidsPack ValidateEvaluateIntent(TargetItem evalTarget, Period evalPeriod, IPeriodProfile evalProfile, IEnumerable<ResultPair> evalResults)
        {
            ValidsPack validParameters = ValidationOfParameters(evalTarget, evalPeriod, evalProfile, evalResults);
            if (validParameters.IsFailure)
            {
                return validParameters;
            }
            return ValidateUtils.Ok();
        }

        protected ValidsPack ValidationOfParameters(TargetItem evalTarget, Period evalPeriod, IPeriodProfile evalProfile, IEnumerable<ResultPair> evalResults)
        {
            if (evalProfile == null)
            {
                return ValidateUtils.Error(EXCEPTION_EXPERT_NULL_TEXT);
            }
            if (evalPeriod == null)
            {
                return ValidateUtils.Error(EXCEPTION_PERIOD_NULL_TEXT);
            }
            if (evalTarget == null)
            {
                return ValidateUtils.Error(EXCEPTION_TARGET_NULL_TEXT);
            }
            if (evalResults == null)
            {
                return ValidateUtils.Error(EXCEPTION_RESULT_NULL_TEXT);
            }
            return ValidateUtils.Ok();
        }

        public virtual object Clone()
        {
            GeneralArticle cloneArticle = (GeneralArticle)this.MemberwiseClone();
            cloneArticle.InternalCode = this.InternalCode;
            cloneArticle.InternalRole = this.InternalRole;

            return cloneArticle;
        }
        public override string ToString()
        {
            return ArticleRoleAdapter.GetSymbol(InternalRole);
        }
    }
}
