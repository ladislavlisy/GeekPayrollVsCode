using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Utils
{
    using SourcePack = ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>;
    using ResultItem = Module.Interfaces.Elements.IArticleResult;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ValidsPack = ResultMonad.Result<bool, string>;

    using Module.Interfaces.Elements;
    using Module.Libs;
    using ResultMonad;

    public static class SourcesUtils
    {
        public static SourcePack Error(string errorText)
        {
            return Result.Fail<IArticleSource, string>(errorText);
        }
        public static SourcePack Ok(IArticleSource source)
        {
            return Result.Ok<IArticleSource, string>(source);
        }
    }
    public static class ResultsUtils
    {
        public static ResultPack Error(string errorText)
        {
            return Result.Fail<IArticleResult, string>(errorText);
        }
        public static ResultPack Ok(IArticleResult source)
        {
            return Result.Ok<IArticleResult, string>(source);
        }
    }
    public static class EvaluateUtils
    {
        public static IEnumerable<ResultPack> Error(string errorText)
        {
            return Result.Fail<ResultItem, string>(errorText).ToList();
        }
        public static IEnumerable<ResultPack> Errors(params string[] errorText)
        {
            return errorText.Select((e) => (Result.Fail<ResultItem, string>(e))).ToList();
        }
        public static IEnumerable<ResultPack> DecoratedError(string format, string message)
        {
            string conceptMessage = string.Format(format, message);

            return Error(conceptMessage);
        }
        public static IEnumerable<ResultPack> DecoratedErrors(string format, params string[] messages)
        {
            string[] conceptMessages = messages.Select((m) => string.Format(format, m)).ToArray();

            return Errors(conceptMessages);
        }
        public static IEnumerable<ResultPack> Results(params ResultPack[] results)
        {
            return results.Select((r) => (r)).ToList();
        }
        public static IEnumerable<ResultPack> Results(params ResultItem[] results)
        {
            return results.Select((r) => Result.Ok<ResultItem, string>(r)).ToList();
        }
    }
    public static class ValidateUtils
    {
        public static ValidsPack Error(string errorText)
        {
            return Result.Fail<bool, string>(errorText);
        }
        public static ValidsPack Ok()
        {
            return Result.Ok<bool, string>(true);
        }
    }
}
