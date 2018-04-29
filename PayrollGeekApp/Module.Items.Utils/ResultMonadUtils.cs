using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Module.Items.Utils
{
    using Libs;
    using ResultMonad;

    public static class ResultMonadUtils
    {
        public static bool HaveAnyResultFailed<TValue, TError>(params Result<TValue, TError>[] results)
        {
            return results.Aggregate(false, (agr, x) => (agr || x.IsFailure));
        }
        public static bool HaveAnyResultFailed<AValue, BValue, TError>(Result<AValue, TError> resultA, Result<BValue, TError> resultB)
        {
            return (resultA.IsFailure || resultB.IsFailure);
        }
        public static bool HaveAnyResultFailed<AValue, BValue, CValue, TError>(Result<AValue, TError> resultA, Result<BValue, TError> resultB, Result<CValue, TError> resultC)
        {
            return (resultA.IsFailure || resultB.IsFailure || resultC.IsFailure);
        }
        public static bool HaveAnyResultFailed<AValue, BValue, CValue, DValue, TError>(Result<AValue, TError> resultA, Result<BValue, TError> resultB, Result<CValue, TError> resultC, Result<DValue, TError> resultD)
        {
            return (resultA.IsFailure || resultB.IsFailure || resultC.IsFailure || resultD.IsFailure);
        }
        public static Result<IEnumerable<Tuple<TAValue, TBValue>>, TError> Zip2ToResultWithZipListAndError<TAValue, TBValue, TError>(
            IEnumerable<TAValue> resultsA, IEnumerable<TBValue> resultsB,
            Func<Result<TAValue, TError>> defaultResultA, Func<Result<TBValue, TError>> defaultResultB, 
            Func<TAValue, TBValue, int> compareFunc)
        {
            IEnumerable<Tuple<TAValue, TBValue>> resultList = new List<Tuple<TAValue, TBValue>>();

            var enumA = resultsA.GetEnumerator();
            var enumB = resultsB.GetEnumerator();

            Result<TAValue, TError> resultItemA = defaultResultA();
            bool bIsCurrentA = enumA.MoveNext();
            Result<TBValue, TError> resultItemB = defaultResultB();
            bool bIsCurrentB = enumB.MoveNext();
            while (bIsCurrentA || bIsCurrentB)
            {
                resultItemA = defaultResultA();
                if (bIsCurrentA)
                {
                    resultItemA = Result.Ok<TAValue, TError>(enumA.Current);
                }
                resultItemB = defaultResultB();
                if (bIsCurrentB)
                {
                    resultItemB = Result.Ok<TBValue, TError>(enumB.Current);
                }
                if (resultItemA.IsFailure)
                {
                    return Result.Fail<IEnumerable<Tuple<TAValue, TBValue>>, TError>(resultItemA.Error);
                }
                if (resultItemB.IsFailure)
                {
                    return Result.Fail<IEnumerable<Tuple<TAValue, TBValue>>, TError>(resultItemB.Error);
                }
                int compareResults = compareFunc(resultItemA.Value, resultItemB.Value);
                if (compareResults > 0)
                {
                    bIsCurrentB = enumB.MoveNext();
                }
                else if (compareResults < 0)
                {
                    bIsCurrentA = enumA.MoveNext();
                }
                else
                {
                    resultList = resultList.Merge(new Tuple<TAValue, TBValue>(resultItemA.Value, resultItemB.Value));

                    bIsCurrentA = enumA.MoveNext();
                    bIsCurrentB = enumB.MoveNext();
                }
            }
            return Result.Ok<IEnumerable<Tuple<TAValue, TBValue>>, TError>(resultList);
        }
        public static Result<IEnumerable<KeyValuePair<TIndex, Tuple<TAValue, TBValue>>>, TError> Zip2ToResultWithKeyValListAndError<Target, TAValue, TBValue, TIndex, TError>(
            IEnumerable<KeyValuePair<Target, Result<TAValue, TError>>> resultsA, IEnumerable<KeyValuePair<Target, Result<TBValue, TError>>> resultsB,
            Func<Result<TAValue, TError>> defaultResultA, Func<Result<TBValue, TError>> defaultResultB,
            Func<Target, Target, TIndex> indexGenerator, 
            Func<Target, Target, int> compareFunc)
            where Target : class
        {
            IEnumerable<KeyValuePair<TIndex, Tuple<TAValue, TBValue>>> resultList = new List<KeyValuePair<TIndex, Tuple<TAValue, TBValue>>>();

            var enumA = resultsA.GetEnumerator();
            var enumB = resultsB.GetEnumerator();

            Target resultTargetA = null;
            Result<TAValue, TError> resultItemA = defaultResultA();
            bool bIsCurrentA = enumA.MoveNext();
            Target resultTargetB = null;
            Result<TBValue, TError> resultItemB = defaultResultB();
            bool bIsCurrentB = enumB.MoveNext();
            while (bIsCurrentA || bIsCurrentB)
            {
                resultItemA = defaultResultA();
                if (bIsCurrentA)
                {
                    resultTargetA = enumA.Current.Key;
                    resultItemA = enumA.Current.Value;
                }
                resultItemB = defaultResultB();
                if (bIsCurrentB)
                {
                    resultTargetB = enumB.Current.Key;
                    resultItemB = enumB.Current.Value;
                }
                if (resultItemA.IsFailure)
                {
                    return Result.Fail<IEnumerable<KeyValuePair<TIndex, Tuple<TAValue, TBValue>>>, TError>(resultItemA.Error);
                }
                if (resultItemB.IsFailure)
                {
                    return Result.Fail<IEnumerable<KeyValuePair<TIndex, Tuple<TAValue, TBValue>>>, TError>(resultItemB.Error);
                }
                int compareResults = compareFunc(resultTargetA, resultTargetB);
                if (compareResults > 0)
                {
                    bIsCurrentB = enumB.MoveNext();
                }
                else if (compareResults < 0)
                {
                    bIsCurrentA = enumA.MoveNext();
                }
                else
                {
                    TAValue resultValueA = resultItemA.Value;
                    TBValue resultValueB = resultItemB.Value;
                    TIndex resultKey = indexGenerator(resultTargetA, resultTargetB);
                    resultList = resultList.Merge(new KeyValuePair<TIndex, Tuple<TAValue, TBValue>>(resultKey, new Tuple<TAValue, TBValue>(resultValueA, resultValueB)));

                    bIsCurrentA = enumA.MoveNext();
                    bIsCurrentB = enumB.MoveNext();
                }
            }
            return Result.Ok<IEnumerable<KeyValuePair<TIndex, Tuple<TAValue, TBValue>>>, TError>(resultList);
        }
        public static Result<IEnumerable<KeyValuePair<TIndex, Tuple<TAValue, TBValue>>>, TError> Zip2ToResultWithKeyValListAndError<Target, TAValue, TBValue, TIndex, TError>(
            IEnumerable<KeyValuePair<Target, Result<TAValue, TError>>> resultsA, IEnumerable<KeyValuePair<Target, Result<TBValue, TError>>> resultsB,
            Func<Result<TAValue, TError>> defaultResultA, Func<Result<TBValue, TError>> defaultResultB,
            Func<Target, Target, TIndex> indexGenerator, 
            Func<Target, Result<TAValue, TError>, Target, Result<TBValue, TError>, int> compareFunc)
            where Target : class
        {
            IEnumerable<KeyValuePair<TIndex, Tuple<TAValue, TBValue>>> resultList = new List<KeyValuePair<TIndex, Tuple<TAValue, TBValue>>>();

            var enumA = resultsA.GetEnumerator();
            var enumB = resultsB.GetEnumerator();

            Target resultTargetA = null;
            Result<TAValue, TError> resultItemA = defaultResultA();
            bool bIsCurrentA = enumA.MoveNext();
            Target resultTargetB = null;
            Result<TBValue, TError> resultItemB = defaultResultB();
            bool bIsCurrentB = enumB.MoveNext();
            while (bIsCurrentA || bIsCurrentB)
            {
                resultItemA = defaultResultA();
                if (bIsCurrentA)
                {
                    resultTargetA = enumA.Current.Key;
                    resultItemA = enumA.Current.Value;
                }
                resultItemB = defaultResultB();
                if (bIsCurrentB)
                {
                    resultTargetB = enumB.Current.Key;
                    resultItemB = enumB.Current.Value;
                }
                if (resultItemA.IsFailure)
                {
                    return Result.Fail<IEnumerable<KeyValuePair<TIndex, Tuple<TAValue, TBValue>>>, TError>(resultItemA.Error);
                }
                if (resultItemB.IsFailure)
                {
                    return Result.Fail<IEnumerable<KeyValuePair<TIndex, Tuple<TAValue, TBValue>>>, TError>(resultItemB.Error);
                }
                int compareResults = compareFunc(resultTargetA, resultItemA, resultTargetB, resultItemB);
                if (compareResults > 0)
                {
                    bIsCurrentB = enumB.MoveNext();
                }
                else if (compareResults < 0)
                {
                    bIsCurrentA = enumA.MoveNext();
                }
                else
                {
                    TAValue resultValueA = resultItemA.Value;
                    TBValue resultValueB = resultItemB.Value;
                    TIndex resultKey = indexGenerator(resultTargetA, resultTargetB);
                    resultList = resultList.Merge(new KeyValuePair<TIndex, Tuple<TAValue, TBValue>>(resultKey, new Tuple<TAValue, TBValue>(resultValueA, resultValueB)));

                    bIsCurrentA = enumA.MoveNext();
                    bIsCurrentB = enumB.MoveNext();
                }
            }
            return Result.Ok<IEnumerable<KeyValuePair<TIndex, Tuple<TAValue, TBValue>>>, TError>(resultList);
        }
    }
}
