using System;
using System.Collections.Generic;

namespace ElementsLib.Module.Libs
{
    using ResultMonad;
    using System.Linq;

    public static class ResultMonadListExtensions
    {
        public static Result<IEnumerable<BValue>, TError> ToResultWithValueListAndError<AValue, TError, BValue>(
            this IEnumerable<Result<AValue, TError>> results,
            Func<AValue, BValue> onSuccessFunc)
        {
            Func<Result<IEnumerable<BValue>, TError>, Result<AValue, TError>,
                Func<AValue, BValue>,
                Result<IEnumerable<BValue>, TError>> agrFunc = (bAgr, a, abfunc) => {
                    if (bAgr.IsFailure)
                    {
                        return Result.Fail<IEnumerable<BValue>, TError>(bAgr.Error);
                    }
                    IEnumerable<BValue> bAgrList = bAgr.Value;
                    if (a.IsFailure)
                    {
                        return Result.Fail<IEnumerable<BValue>, TError>(a.Error);
                    }
                    BValue bResult = abfunc(a.Value);
                    return Result.Ok<IEnumerable<BValue>, TError>(bAgrList.Merge(bResult));
                };

            Result<IEnumerable<BValue>, TError> initResult = Result.Ok<IEnumerable<BValue>, TError>(new List<BValue>());
            return results.Aggregate(initResult, (agr, x) => agrFunc(agr, x, onSuccessFunc));
        }

        public static Result<IEnumerable<BValue>, TError> ToResultWithValueListAndError<AValue, TError, BValue>(
            this IEnumerable<Result<AValue, TError>> results,
            Func<AValue, Result<BValue, TError>> onSuccessFunc)
        {
            Func<Result<IEnumerable<BValue>, TError>, Result<AValue, TError>,
                Func<AValue, Result<BValue, TError>>,
                Result<IEnumerable<BValue>, TError>> agrFunc = (bAgr, a, abfunc) => {
                    if (bAgr.IsFailure)
                    {
                        return Result.Fail<IEnumerable<BValue>, TError>(bAgr.Error);
                    }
                    IEnumerable<BValue> bAgrList = bAgr.Value;
                    if (a.IsFailure)
                    {
                        return Result.Fail<IEnumerable<BValue>, TError>(a.Error);
                    }
                    Result<BValue, TError> bResult = abfunc(a.Value);
                    if (bResult.IsFailure)
                    {
                        return Result.Fail<IEnumerable<BValue>, TError>(bResult.Error);
                    }
                    return Result.Ok<IEnumerable<BValue>, TError>(bAgrList.Merge(bResult.Value));
                };

            Result<IEnumerable<BValue>, TError> initResult = Result.Ok<IEnumerable<BValue>, TError>(new List<BValue>());
            return results.Aggregate(initResult, (agr, x) => agrFunc(agr, x, onSuccessFunc));
        }

        public static Result<IEnumerable<BValue>, TError> ToResultWithValueListAndError<Target, AValue, TError, BValue>(
            this IEnumerable<KeyValuePair<Target, Result<AValue, TError>>> results,
            Func<Target, AValue, Result<BValue, TError>> onSuccessFunc)
        {
            Func<Result<IEnumerable<BValue>, TError>, KeyValuePair<Target, Result<AValue, TError>>,
                Func<Target, AValue, Result<BValue, TError>>,
                Result<IEnumerable<BValue>, TError>> agrFunc = (bAgr, a, abfunc) => {
                    if (bAgr.IsFailure)
                    {
                        return Result.Fail<IEnumerable<BValue>, TError>(bAgr.Error);
                    }
                    IEnumerable<BValue> bAgrList = bAgr.Value;
                    Result<AValue, TError> aResult = a.Value;
                    if (aResult.IsFailure)
                    {
                        return Result.Fail<IEnumerable<BValue>, TError>(aResult.Error);
                    }
                    Target aParamKey = a.Key;
                    AValue aParamVal = aResult.Value;
                    Result<BValue, TError> bResult = abfunc(aParamKey, aParamVal);
                    if (bResult.IsFailure)
                    {
                        return Result.Fail<IEnumerable<BValue>, TError>(bResult.Error);
                    }
                    return Result.Ok<IEnumerable<BValue>, TError>(bAgrList.Merge(bResult.Value));
                };

            Result<IEnumerable<BValue>, TError> initResult = Result.Ok<IEnumerable<BValue>, TError>(new List<BValue>());
            return results.Aggregate(initResult, (agr, x) => agrFunc(agr, x, onSuccessFunc));
        }

        public static Result<TValue, TError> FirstToResultWithValueAndError<Target, TValue, TError>(
            this IEnumerable<KeyValuePair<Target, Result<TValue, TError>>> results,
            Func<Target, bool> filterFunc, TError errorNotFound)
        {
            KeyValuePair<Target, Result<TValue, TError>> findResult = results.FirstOrDefault((x) => filterFunc(x.Key));

            if (findResult.Key == null)
            {
                return Result.Fail<TValue, TError>(errorNotFound);
            }
            Result<TValue, TError> packResult = findResult.Value;
            if (packResult.IsFailure)
            {
                return Result.Fail<TValue, TError>(packResult.Error);
            }
            return packResult;
        }
        public static Result<BValue, TError> FirstToResultWithValueAndError<Target, AValue, TError, BValue>(
            this IEnumerable<KeyValuePair<Target, Result<AValue, TError>>> results,
            Func<Target, bool> filterFunc, TError errorNotFound,
            Func<AValue, Result<BValue, TError>> convertFunc)
        {
            KeyValuePair<Target, Result<AValue, TError>> findResult = results.FirstOrDefault((x) => filterFunc(x.Key));

            if (findResult.Key == null)
            {
                return Result.Fail<BValue, TError>(errorNotFound);
            }
            Result<AValue, TError> packTarget = findResult.Value;
            if (packTarget.IsFailure)
            {
                return Result.Fail<BValue, TError>(packTarget.Error);
            }
            Result<BValue, TError> packResult = convertFunc(packTarget.Value);
            if (packResult.IsFailure)
            {
                return Result.Fail<BValue, TError>(packResult.Error);
            }
            return packResult;
        }
    }
}
