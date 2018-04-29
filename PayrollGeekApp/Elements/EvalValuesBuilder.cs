using System;
using System.Collections.Generic;

namespace ElementsLib.Elements
{
    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;

    using Module.Interfaces.Elements;
    using ResultMonad;

    public abstract class EvalValuesBuilder<TAGR>
    {
        public static string CONCEPT_VALUES_INVALID_TEXT = "Invalid source values!";
        public static string CONCEPT_RESULT_INVALID_TEXT = "Invalid dependent result values!";

        public EvalValuesBuilder(string errorMessage)
        {
            InternalFailure = false;

            InternalError = errorMessage;
        }
        protected string InternalError { get; set; } 
        protected bool InternalFailure { get; set; } 
        public ResultMonad.Result<TAGR, string> GetValues(ResultMonad.Result<TAGR, string> initValues)
        {
            if (initValues.IsFailure)
            {
                return initValues;
            }
            TAGR final = GetNewValues(initValues.Value);

            if (InternalFailure)
            {
                return Result.Fail<TAGR, string>(InternalError);
            }

            return Result.Ok<TAGR, string>(final);
        }
        public TAGR ReturnFailure(TAGR values)
        {
            InternalFailure = true;

            return values;
        }
        public TAGR ReturnFailureAndError(TAGR values, string message)
        {
            InternalFailure = true;

            InternalError = message;

            return values;
        }

        public abstract TAGR GetNewValues(TAGR initValues);
    }
    public abstract class EvalValuesSourceBuilder<TAGR> : EvalValuesBuilder<TAGR>
    {
        public EvalValuesSourceBuilder(ISourceValues evalValues) : base(CONCEPT_VALUES_INVALID_TEXT)
        {
            InternalValues = evalValues;
        }

        protected ISourceValues InternalValues { get; set; } 
    }
    public abstract class EvalValuesResultBuilder<TAGR> : EvalValuesBuilder<TAGR>
    {
        public EvalValuesResultBuilder(TargetItem evalTarget, IEnumerable<ResultPair> evalResults) : base(CONCEPT_RESULT_INVALID_TEXT)
        {
            InternalTarget = evalTarget;
            InternalValues = evalResults;
        }
        protected TargetItem InternalTarget { get; set; } 
        protected IEnumerable<ResultPair> InternalValues { get; set; } 
    }
}
