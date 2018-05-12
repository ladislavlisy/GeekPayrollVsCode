using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Elements.Utils
{
    using ConfigCode = UInt16;
    using ConfigBase = Module.Interfaces.Matrixus.IArticleConfigFeatures;

    using TargetHead = UInt16;
    using TargetPart = UInt16;
    using TargetSeed = UInt16;

    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using ResultItem = Module.Interfaces.Elements.IArticleResult;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ValuesItem = Module.Interfaces.Elements.IArticleResultValues;

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
        public static string ERROR_TEXT_RESULTS_CASTING_FAILED = "Failed casting";
        public static string ERROR_TEXT_RESULTS_LOOKUP_FAILED = "Failed value lookup";
        public static string ERROR_TEXT_RESULTS_SELECT_FAILED = "Failed value select";

        public static ResultPack FindResult(this IEnumerable<ResultPair> evalResults, Func<TargetItem, bool> filterTargetFunc)
        {
            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterTargetFunc, ERROR_TEXT_CONTRACT_CODE_NOT_FOUND);

            return findResult;
        }
        public static Result<TResult, string> FindTypeResult<TResult>(this IEnumerable<ResultPair> evalResults, 
            Func<TargetItem, bool> filterTargetFunc) where TResult : class, ResultItem
        {
            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterTargetFunc, ERROR_TEXT_CONTRACT_NOT_FOUND);
            if (findResult.IsFailure)
            {
                return Result.Fail<TResult, string>(findResult.Error);
            }
            ResultItem itemResult = findResult.Value;

            TResult typeResult = itemResult as TResult;
            if (typeResult == null)
            {
                return Result.Fail<TResult, string>(itemResult.DecoratedError(ERROR_TEXT_RESULTS_CASTING_FAILED));
            }
            return Result.Ok<TResult, string>(typeResult);
        }
        public static Result<TRValue, string> FindResultValue<TResult, TRValue>(this IEnumerable<ResultPair> evalResults, 
            Func<TargetItem, bool> filterTargetFunc,
            Func<ValuesItem, bool> selectValuesFunc)
            where TResult : class, ResultItem
            where TRValue : class, ValuesItem
        {
            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterTargetFunc, ERROR_TEXT_CONTRACT_NOT_FOUND);
            if (findResult.IsFailure)
            {
                return Result.Fail<TRValue, string>(findResult.Error);
            }
            ResultItem itemResult = findResult.Value;

            TResult typeResult = itemResult as TResult;
            if (typeResult == null)
            {
                return Result.Fail<TRValue, string>(itemResult.DecoratedError(ERROR_TEXT_RESULTS_CASTING_FAILED));
            }
            Maybe<TRValue> typeValues = typeResult.ReturnValue<TRValue>(selectValuesFunc);
            if (typeValues.HasNoValue)
            {
                return Result.Fail<TRValue, string>(typeResult.DecoratedError(ERROR_TEXT_RESULTS_LOOKUP_FAILED));
            }
            return Result.Ok<TRValue, string>(typeValues.Value);
        }
        public static Result<TAValue, string> FindAndTransformResultValue<TResult, TRValue, TAValue>(this IEnumerable<ResultPair> evalResults, 
            Func<TargetItem, bool> filterTargetFunc,
            Func<ValuesItem, bool> selectValuesFunc, Func<TRValue, Result<TAValue, string>> selectResultFunc)
            where TResult : class, ResultItem
            where TRValue : class, ValuesItem
        {
            ResultPack findResult = evalResults.FirstToResultWithValueAndError(filterTargetFunc, ERROR_TEXT_CONTRACT_NOT_FOUND);
            if (findResult.IsFailure)
            {
                return Result.Fail<TAValue, string>(findResult.Error);
            }
            ResultItem itemResult = findResult.Value;

            TResult typeResult = itemResult as TResult;
            if (typeResult == null)
            {
                return Result.Fail<TAValue, string>(itemResult.DecoratedError(ERROR_TEXT_RESULTS_CASTING_FAILED));
            }
            Maybe<TRValue> typeValues = typeResult.ReturnValue<TRValue>(selectValuesFunc);
            if (typeValues.HasNoValue)
            {
                return Result.Fail<TAValue, string>(typeResult.DecoratedError(ERROR_TEXT_RESULTS_LOOKUP_FAILED));
            }
            Result<TAValue, string> tranResult = selectResultFunc(typeValues.Value);
            if (tranResult.IsFailure)
            {
                return Result.Fail<TAValue, string>(typeResult.DecoratedError(ERROR_TEXT_RESULTS_SELECT_FAILED));
            }
            return Result.Ok<TAValue, string>(tranResult.Value);
        }

        public static Result<IEnumerable<ResultPair>, string> GetTypedResultsInListAndError<TResult>(this IEnumerable<ResultPair> evalResults,
            Func<TargetItem, bool> filterTargetFunc) where TResult : class, ResultItem
        {
            Func<Result<IEnumerable<ResultPair>, string>, ResultPair,
                Func<TargetItem, bool>, Result<IEnumerable<ResultPair>, string>> agrFunc = (bAgr, a, tFilter) => {
                    if (bAgr.IsFailure)
                    {
                        return Result.Fail<IEnumerable<ResultPair>, string>(bAgr.Error);
                    }
                    IEnumerable<ResultPair> bAgrList = bAgr.Value;
                    Result<ResultItem, string> aResult = a.Value;
                    if (aResult.IsFailure)
                    {
                        return Result.Fail<IEnumerable<ResultPair>, string>(aResult.Error);
                    }
                    TargetItem aParamKey = a.Key;
                    if (tFilter(aParamKey) == false)
                    {
                        return Result.Ok<IEnumerable<ResultPair>, string>(bAgrList);
                    }
                    ResultItem itemResult = aResult.Value;

                    TResult typeResult = itemResult as TResult;
                    if (typeResult == null)
                    {
                        return Result.Fail<IEnumerable<ResultPair>, string>(itemResult.DecoratedError(ERROR_TEXT_RESULTS_CASTING_FAILED));
                    }
                    IEnumerable<ResultPair> resultList = bAgrList.Merge(a).OrderBy((x) => (x.Key));

                    return Result.Ok<IEnumerable<ResultPair>, string>(resultList);
                };

            Result<IEnumerable<ResultPair>, string> initResult = Result.Ok<IEnumerable<ResultPair>, string>(new List<ResultPair>());
            return evalResults.Aggregate(initResult, (agr, x) => agrFunc(agr, x, filterTargetFunc));
        }
        public static Result<IEnumerable<TRValue>, string> GetResultValuesInListAndError<TResult, TRValue>(this IEnumerable<ResultPair> evalResults,
            Func<TargetItem, bool> filterTargetFunc, Func<TResult, bool> filterValuesFunc,
            Func<ValuesItem, bool> selectValuesFunc)
            where TResult : class, ResultItem
            where TRValue : class, ValuesItem
        {
            Func<Result<IEnumerable<TRValue>, string>, ResultPair,
                Func<TargetItem, bool>, Func<TResult, bool>, Func<ValuesItem, bool>,
                Result<IEnumerable<TRValue>, string>> agrFunc = (bAgr, a, tFilter, vFilter, exfunc) => {
                    if (bAgr.IsFailure)
                    {
                        return Result.Fail<IEnumerable<TRValue>, string>(bAgr.Error);
                    }
                    IEnumerable<TRValue> bAgrList = bAgr.Value;
                    Result<ResultItem, string> aResult = a.Value;
                    if (aResult.IsFailure)
                    {
                        return Result.Fail<IEnumerable<TRValue>, string>(aResult.Error);
                    }
                    TargetItem aParamKey = a.Key;
                    if (tFilter(aParamKey) == false)
                    {
                        return Result.Ok<IEnumerable<TRValue>, string>(bAgrList);
                    }
                    ResultItem aParamRes = aResult.Value;

                    TResult aParamVal = aParamRes as TResult;
                    if (aParamVal == null)
                    {
                        return Result.Fail<IEnumerable<TRValue>, string>(aParamRes.DecoratedError(ERROR_TEXT_RESULTS_CASTING_FAILED));
                    }
                    if (vFilter(aParamVal) == false)
                    {
                        return Result.Ok<IEnumerable<TRValue>, string>(bAgrList);
                    }
                    Maybe<TRValue> typeValues = aParamVal.ReturnValue<TRValue>(exfunc);
                    if (typeValues.HasNoValue)
                    {
                        return Result.Fail<IEnumerable<TRValue>, string>(aParamVal.DecoratedError(ERROR_TEXT_RESULTS_LOOKUP_FAILED));
                    }
                    IEnumerable<TRValue> resultList = bAgrList.Merge(typeValues.Value);

                    return Result.Ok<IEnumerable<TRValue>, string>(resultList);
                };

            Result<IEnumerable<TRValue>, string> initResult = Result.Ok<IEnumerable<TRValue>, string>(new List<TRValue>());
            return evalResults.Aggregate(initResult, (agr, x) => agrFunc(agr, x, filterTargetFunc, filterValuesFunc, selectValuesFunc));
        }
        public static Result<IEnumerable<TSValue>, string> GetResultValuesInListAndError<TResult, TRValue, TSValue>(this IEnumerable<ResultPair> evalResults,
            Func<TargetItem, bool> filterTargetFunc, Func<TResult, bool> filterValuesFunc,
            Func<ValuesItem, bool> selectValuesFunc, Func<TargetItem, TRValue, Result<TSValue, string>> selectResultFunc)
            where TResult : class, ResultItem
            where TRValue : class, ValuesItem
        {
            Func<Result<IEnumerable<TSValue>, string>, ResultPair,
                Func<TargetItem, bool>, Func<TResult, bool>, Func<ValuesItem, bool>, Func<TargetItem, TRValue, Result<TSValue, string>>,
                Result<IEnumerable<TSValue>, string>> agrFunc = (bAgr, a, tFilter, vFilter, vSelect, rSelect) => {
                    if (bAgr.IsFailure)
                    {
                        return Result.Fail<IEnumerable<TSValue>, string>(bAgr.Error);
                    }
                    IEnumerable<TSValue> bAgrList = bAgr.Value;
                    Result<ResultItem, string> aResult = a.Value;
                    if (aResult.IsFailure)
                    {
                        return Result.Fail<IEnumerable<TSValue>, string>(aResult.Error);
                    }
                    TargetItem aParamKey = a.Key;
                    if (tFilter(aParamKey) == false)
                    {
                        return Result.Ok<IEnumerable<TSValue>, string>(bAgrList);
                    }
                    ResultItem aParamRes = aResult.Value;

                    TResult aParamVal = aParamRes as TResult;
                    if (aParamVal == null)
                    {
                        return Result.Fail<IEnumerable<TSValue>, string>(aParamRes.DecoratedError(ERROR_TEXT_RESULTS_CASTING_FAILED));
                    }
                    if (vFilter(aParamVal) == false)
                    {
                        return Result.Ok<IEnumerable<TSValue>, string>(bAgrList);
                    }
                    Maybe<TRValue> typeValues = aParamVal.ReturnValue<TRValue>(vSelect);
                    if (typeValues.HasNoValue)
                    {
                        return Result.Fail<IEnumerable<TSValue>, string>(aParamVal.DecoratedError(ERROR_TEXT_RESULTS_LOOKUP_FAILED));
                    }
                    Result<TSValue, string> selResult = rSelect(aParamKey, typeValues.Value);
                    if (selResult.IsFailure)
                    {
                        return Result.Fail<IEnumerable<TSValue>, string>(aParamVal.DecoratedError(ERROR_TEXT_RESULTS_SELECT_FAILED));
                    }

                    IEnumerable<TSValue> resultList = bAgrList.Merge(selResult.Value);

                    return Result.Ok<IEnumerable<TSValue>, string>(resultList);
                };

            Result<IEnumerable<TSValue>, string> initResult = Result.Ok<IEnumerable<TSValue>, string>(new List<TSValue>());
            return evalResults.Aggregate(initResult, (agr, x) => agrFunc(agr, x, filterTargetFunc, filterValuesFunc, selectValuesFunc, selectResultFunc));
        }
        public static Result<TAValue, string> GetResultValuesInAggrAndError<TResult, TRValue, TAValue>(this IEnumerable<ResultPair> evalResults, TAValue initValues,
            Func<TargetItem, bool> filterTargetFunc, Func<TResult, bool> filterValuesFunc,
            Func<ValuesItem, bool> selectValuesFunc, Func<TAValue, TargetItem, TRValue, Result<TAValue, string>> selectResultFunc)
            where TResult : class, ResultItem
            where TRValue : class, ValuesItem
        {
            Func<Result<TAValue, string>, ResultPair,
                Func<TargetItem, bool>, Func<TResult, bool>, Func<ValuesItem, bool>, Func<TAValue, TargetItem, TRValue, Result<TAValue, string>>,
                Result<TAValue, string>> agrFunc = (bAgr, a, tFilter, vFilter, vSelect, rSelect) => {
                    if (bAgr.IsFailure)
                    {
                        return Result.Fail<TAValue, string>(bAgr.Error);
                    }
                    TAValue bAgrValue = bAgr.Value;
                    Result<ResultItem, string> aResult = a.Value;
                    if (aResult.IsFailure)
                    {
                        return Result.Fail<TAValue, string>(aResult.Error);
                    }
                    TargetItem aParamKey = a.Key;
                    if (tFilter(aParamKey) == false)
                    {
                        return Result.Ok<TAValue, string>(bAgrValue);
                    }
                    ResultItem aParamRes = aResult.Value;

                    TResult aParamVal = aParamRes as TResult;
                    if (aParamVal == null)
                    {
                        return Result.Fail<TAValue, string>(aParamRes.DecoratedError(ERROR_TEXT_RESULTS_CASTING_FAILED));
                    }
                    if (vFilter(aParamVal) == false)
                    {
                        return Result.Ok<TAValue, string>(bAgrValue);
                    }
                    Maybe<TRValue> typeValues = aParamVal.ReturnValue<TRValue>(vSelect);
                    if (typeValues.HasNoValue)
                    {
                        return Result.Fail<TAValue, string>(aParamVal.DecoratedError(ERROR_TEXT_RESULTS_LOOKUP_FAILED));
                    }
                    Result<TAValue, string> selResult = rSelect(bAgrValue, aParamKey, typeValues.Value);
                    if (selResult.IsFailure)
                    {
                        return Result.Fail<TAValue, string>(aParamVal.DecoratedError(ERROR_TEXT_RESULTS_SELECT_FAILED));
                    }

                    return Result.Ok<TAValue, string>(selResult.Value);
                };

            Result<TAValue, string> initResult = Result.Ok<TAValue, string>(initValues);
            return evalResults.Aggregate(initResult, (agr, x) => agrFunc(agr, x, filterTargetFunc, filterValuesFunc, selectValuesFunc, selectResultFunc));
        }
    }
}
