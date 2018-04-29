using System;
using MaybeMonad;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Module.Items.Utils
{
    public static class MaybeMonadUtils
    {
        public static bool HaveAnyResultNullValue<TValue>(params TValue[] results) where TValue : class
        {
            return results.Aggregate(false, (agr, x) => (agr || x == null));
        }
        public static bool HaveAnyResultNoValues<TValue>(params Maybe<TValue>[] results)
        {
            return results.Aggregate(false, (agr, x) => (agr || x.HasNoValue));
        }
        public static bool HaveAnyResultNoValues<AValue, BValue>(Maybe<AValue> resultA, Maybe<BValue> resultB)
        {
            return (resultA.HasNoValue || resultB.HasNoValue);
        }
        public static bool HaveAnyResultNoValues<AValue, BValue, CValue>(Maybe<AValue> resultA, Maybe<BValue> resultB, Maybe<CValue> resultC)
        {
            return (resultA.HasNoValue || resultB.HasNoValue || resultC.HasNoValue);
        }
        public static bool HaveAnyResultNoValues<AValue, BValue, CValue, DValue>(Maybe<AValue> resultA, Maybe<BValue> resultB, Maybe<CValue> resultC, Maybe<DValue> resultD)
        {
            return (resultA.HasNoValue || resultB.HasNoValue || resultC.HasNoValue || resultD.HasNoValue);
        }
    }
}
