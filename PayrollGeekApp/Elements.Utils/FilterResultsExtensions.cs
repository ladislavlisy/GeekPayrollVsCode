using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Elements.Utils
{
    using ConfigCode = UInt16;

    using TargetHead = UInt16;
    using TargetPart = UInt16;
    using TargetSeed = UInt16;

    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using ResultItem = Module.Interfaces.Elements.IArticleResult;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;

    using Module.Libs;
    using ResultMonad;
    using MaybeMonad;
    using Module.Interfaces.Elements;

    public static class FilterResultsExtensions
    {
        public static string ERROR_TEXT_CONTRACT_NOT_FOUND = "Contract Result not found!";
        public static string ERROR_TEXT_POSITION_NOT_FOUND = "Position Result not found!";
        public static string ERROR_TEXT_CONTRACT_CODE_NOT_FOUND = "Result for Contract Target and Code not found!";
        public static string ERROR_TEXT_POSITION_CODE_NOT_FOUND = "Result for Position Target and Code not found!";
        public static ResultPack FindContractResultForCode(this IEnumerable<ResultPair> evalResults, ConfigCode contractCode, TargetSeed contractSeed)
        {
            Func<TargetItem, bool> filterFunc = (x) => (x.IsEqualByCodePlusSeed(contractCode, contractSeed));

            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterFunc, ERROR_TEXT_CONTRACT_NOT_FOUND);

            return findResult;
        }

        public static Result<TResult, string> FindContractTypeResultForCode<TResult>(this IEnumerable<ResultPair> evalResults, ConfigCode contractCode, TargetSeed contractSeed) where TResult : class, ResultItem
        {
            Func<TargetItem, bool> filterFunc = (x) => (x.IsEqualByCodePlusSeed(contractCode, contractSeed));

            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterFunc, ERROR_TEXT_CONTRACT_NOT_FOUND);
            if (findResult.IsFailure)
            {
                return Result.Fail<TResult, string>(findResult.Error);
            }
            TResult typeResult = findResult.Value as TResult;
            if (typeResult == null)
            {
                return Result.Fail<TResult, string>("Failed casting");
            }
            return Result.Ok<TResult, string>(typeResult);
        }
        public static Result<TRValue, string> FindContractResultValueForCode<TResult, TRValue>(this IEnumerable<ResultPair> evalResults, 
            ConfigCode contractCode, TargetSeed contractSeed, 
            Func<IArticleResultValues, bool> getValsFunc) 
            where TResult : class, ResultItem
            where TRValue : class, IArticleResultValues
        {
            Func<TargetItem, bool> filterFunc = (x) => (x.IsEqualByCodePlusSeed(contractCode, contractSeed));

            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterFunc, ERROR_TEXT_CONTRACT_NOT_FOUND);
            if (findResult.IsFailure)
            {
                return Result.Fail<TRValue, string>(findResult.Error);
            }
            TResult typeResult = findResult.Value as TResult;
            if (typeResult == null)
            {
                return Result.Fail<TRValue, string>("Failed casting");
            }
            Maybe<TRValue> typeValues = typeResult.ReturnValue<TRValue>(getValsFunc);
            if (typeValues.HasNoValue)
            {
                return Result.Fail<TRValue, string>("Failed value lookup");
            }
            return Result.Ok<TRValue, string>(typeValues.Value);
        }

        public static ResultPack FindPositionResultForCode(this IEnumerable<ResultPair> evalResults, ConfigCode positionCode, TargetHead contractCode, TargetSeed positionSeed)
        {
            Func<TargetItem, bool> filterFunc = (x) => (x.IsEqualByCodePlusHeadAndSeed(positionCode, contractCode, positionSeed));

            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterFunc, ERROR_TEXT_POSITION_NOT_FOUND);

            return findResult;
        }

        public static Result<TResult, string> FindPositionTypeResultForCode<TResult>(this IEnumerable<ResultPair> evalResults, ConfigCode positionCode, TargetHead contractCode, TargetSeed positionSeed) where TResult : class, ResultItem
        {
            Func<TargetItem, bool> filterFunc = (x) => (x.IsEqualByCodePlusHeadAndSeed(positionCode, contractCode, positionSeed));

            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterFunc, ERROR_TEXT_CONTRACT_NOT_FOUND);
            if (findResult.IsFailure)
            {
                return Result.Fail<TResult, string>(findResult.Error);
            }
            TResult typeResult = findResult.Value as TResult;
            if (typeResult == null)
            {
                return Result.Fail<TResult, string>("Failed casting");
            }
            return Result.Ok<TResult, string>(typeResult);
        }
        public static Result<TRValue, string> FindPositionResultValueForCode<TResult, TRValue>(this IEnumerable<ResultPair> evalResults, ConfigCode positionCode, TargetHead contractCode, TargetSeed positionSeed, 
            Func<IArticleResultValues, bool> getValsFunc) 
            where TResult : class, ResultItem
            where TRValue : class, IArticleResultValues
        {
            Func<TargetItem, bool> filterFunc = (x) => (x.IsEqualByCodePlusHeadAndSeed(positionCode, contractCode, positionSeed));

            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterFunc, ERROR_TEXT_CONTRACT_NOT_FOUND);
            if (findResult.IsFailure)
            {
                return Result.Fail<TRValue, string>(findResult.Error);
            }
            TResult typeResult = findResult.Value as TResult;
            if (typeResult == null)
            {
                return Result.Fail<TRValue, string>("Failed casting");
            }
            Maybe<TRValue> typeValues = typeResult.ReturnValue<TRValue>(getValsFunc);
            if (typeValues.HasNoValue)
            {
                return Result.Fail<TRValue, string>("Failed value lookup");
            }
            return Result.Ok<TRValue, string>(typeValues.Value);
        }

        public static ResultPack FindResultForCodePlusHead(this IEnumerable<ResultPair> evalResults, ConfigCode findCode, TargetHead headCode)
        {
            TargetPart partCode = ArticleTarget.PART_CODE_NULL;

            Func<TargetItem, bool> filterFunc = (x) => (x.IsEqualByCodePlusHeadAndPart(findCode, headCode, partCode));

            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterFunc, ERROR_TEXT_CONTRACT_CODE_NOT_FOUND);

            return findResult;
        }
        public static Result<TResult, string> FindTypeResultForCodePlusHead<TResult>(this IEnumerable<ResultPair> evalResults, ConfigCode findCode, TargetHead headCode) where TResult : class, ResultItem
        {
            TargetPart partCode = ArticleTarget.PART_CODE_NULL;

            Func<TargetItem, bool> filterFunc = (x) => (x.IsEqualByCodePlusHeadAndPart(findCode, headCode, partCode));

            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterFunc, ERROR_TEXT_CONTRACT_NOT_FOUND);
            if (findResult.IsFailure)
            {
                return Result.Fail<TResult, string>(findResult.Error);
            }
            TResult typeResult = findResult.Value as TResult;
            if (typeResult == null)
            {
                return Result.Fail<TResult, string>("Failed casting");
            }
            return Result.Ok<TResult, string>(typeResult);
        }
        public static Result<TRValue, string> FindResultValueForCodePlusHead<TResult, TRValue>(this IEnumerable<ResultPair> evalResults, ConfigCode findCode, TargetHead headCode,
            Func<IArticleResultValues, bool> getValsFunc)
            where TResult : class, ResultItem
            where TRValue : class, IArticleResultValues
        {
            TargetPart partCode = ArticleTarget.PART_CODE_NULL;

            Func<TargetItem, bool> filterFunc = (x) => (x.IsEqualByCodePlusHeadAndPart(findCode, headCode, partCode));

            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterFunc, ERROR_TEXT_CONTRACT_NOT_FOUND);
            if (findResult.IsFailure)
            {
                return Result.Fail<TRValue, string>(findResult.Error);
            }
            TResult typeResult = findResult.Value as TResult;
            if (typeResult == null)
            {
                return Result.Fail<TRValue, string>("Failed casting");
            }
            Maybe<TRValue> typeValues = typeResult.ReturnValue<TRValue>(getValsFunc);
            if (typeValues.HasNoValue)
            {
                return Result.Fail<TRValue, string>("Failed value lookup");
            }
            return Result.Ok<TRValue, string>(typeValues.Value);
        }


        public static ResultPack FindResultForCodePlusPart(this IEnumerable<ResultPair> evalResults, ConfigCode findCode, TargetHead headCode, TargetPart partCode)
        {
            Func<TargetItem, bool> filterFunc = (x) => (x.IsEqualByCodePlusHeadAndPart(findCode, headCode, partCode));

            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterFunc, ERROR_TEXT_POSITION_CODE_NOT_FOUND);

            return findResult;
        }
        public static Result<TResult, string> FindTypeResultForCodePlusPart<TResult>(this IEnumerable<ResultPair> evalResults, ConfigCode findCode, TargetHead headCode, TargetPart partCode) where TResult : class, ResultItem
        {
            Func<TargetItem, bool> filterFunc = (x) => (x.IsEqualByCodePlusHeadAndPart(findCode, headCode, partCode));

            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterFunc, ERROR_TEXT_CONTRACT_NOT_FOUND);
            if (findResult.IsFailure)
            {
                return Result.Fail<TResult, string>(findResult.Error);
            }
            TResult typeResult = findResult.Value as TResult;
            if (typeResult == null)
            {
                return Result.Fail<TResult, string>("Failed casting");
            }
            return Result.Ok<TResult, string>(typeResult);
        }
        public static Result<TRValue, string> FindResultValueForCodePlusPart<TResult, TRValue>(this IEnumerable<ResultPair> evalResults, ConfigCode findCode, TargetHead headCode, TargetPart partCode,
            Func<IArticleResultValues, bool> getValsFunc)
            where TResult : class, ResultItem
            where TRValue : class, IArticleResultValues
        {
            Func<TargetItem, bool> filterFunc = (x) => (x.IsEqualByCodePlusHeadAndPart(findCode, headCode, partCode));

            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterFunc, ERROR_TEXT_CONTRACT_NOT_FOUND);
            if (findResult.IsFailure)
            {
                return Result.Fail<TRValue, string>(findResult.Error);
            }
            TResult typeResult = findResult.Value as TResult;
            if (typeResult == null)
            {
                return Result.Fail<TRValue, string>("Failed casting");
            }
            Maybe<TRValue> typeValues = typeResult.ReturnValue<TRValue>(getValsFunc);
            if (typeValues.HasNoValue)
            {
                return Result.Fail<TRValue, string>("Failed value lookup");
            }
            return Result.Ok<TRValue, string>(typeValues.Value);
        }


        public static IEnumerable<ResultPair> GetResultForCodePlusHead(this IEnumerable<ResultPair> evalResults, ConfigCode findCode, TargetHead headCode)
        {
            Func<ResultPair, bool> filterFunc = (x) => (x.Key.IsEqualByCodePlusHead(findCode, headCode));

            IEnumerable<ResultPair> findResults = evalResults.Where(filterFunc);

            return findResults;
        }
    }
}
