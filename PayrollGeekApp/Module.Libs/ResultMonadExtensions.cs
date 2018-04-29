using System;
using System.Collections.Generic;

namespace ElementsLib.Module.Libs
{
    using ResultMonad;
    using Items;
    using Interfaces.Legalist;

    public static class ResultMonadExtensions
    {
        public static string Description<TValue, TError>(this Result<TValue, TError> result)
        {
            if (result.IsFailure)
            {
                return string.Format("Error: {0}", result.Error.ToString());
            }
            else
            {
                return result.Value.ToString();
            }
        }
        public static string Description<Target, TValue, TError>(this KeyValuePair<Target, Result<TValue, TError>> value)
        {
            Target node = value.Key;

            Result<TValue, TError> item = value.Value;

            return string.Format("{0} {1}", node.ToString(), item.Description());
        }
        public static IEnumerable<Result<TValue, TError>> ToList<TValue, TError>(this Result<TValue, TError> result)
        {
            return new List<Result<TValue, TError>>() { result };
        }
        public static IEnumerable<Result<BValue, TError>> OnSuccessToResultSet<AValue, TError, BValue>(this Result<AValue, TError> result, 
            Func<AValue, IEnumerable<Result<BValue, TError>>> onSuccessFunc)
        {
            if (result.IsFailure)
            {
                return new List<Result<BValue, TError>>() { Result.Fail<BValue, TError>(result.Error) };
            }

            return onSuccessFunc(result.Value);
        }
        public static IEnumerable<Result<BValue, TError>> OnSuccessToResultSetEvaluate<Target, AValue, TError, BValue>(this Result<AValue, TError> result,
            Target evalTarget, Period evalPeriod, IPeriodProfile evalProfile, IEnumerable<KeyValuePair<Target, Result<BValue, TError>>> evalResults, 
            Func<AValue, Target, Period, IPeriodProfile, IEnumerable<KeyValuePair<Target, Result<BValue, TError>>>, IEnumerable<Result<BValue, TError>>> onSuccessFunc)
        {
            if (result.IsFailure)
            {
                return new List<Result<BValue, TError>> () { Result.Fail<BValue, TError>(result.Error) };
            }

            return onSuccessFunc(result.Value, evalTarget, evalPeriod, evalProfile, evalResults);
        }
    }
}
