using System;

namespace ElementsLib.Legalist.Rounding
{
    using Operations;
    public static class RoundingInt
    {
        private static readonly decimal INT_ROUNDING_CONST = 0.5m;

        public static Int32 RoundToInt(decimal valueDec)
        {
            decimal roundRet = decimal.Floor(Math.Abs(valueDec) + INT_ROUNDING_CONST);

            return decimal.ToInt32(valueDec < 0m ? decimal.Negate(roundRet) : roundRet);
        }

        public static Int32 RoundUp(decimal valueDec)
        {
            decimal roundRet = decimal.Ceiling(Math.Abs(valueDec));

            return decimal.ToInt32(valueDec < 0m ? decimal.Negate(roundRet) : roundRet);
        }

        public static Int32 RoundDown(decimal valueDec)
        {
            decimal roundRet = decimal.Floor(Math.Abs(valueDec));

            return decimal.ToInt32(valueDec < 0m ? decimal.Negate(roundRet) : roundRet);
        }

        public static Int32 NearRoundUp(decimal valueDec, Int32 nearest = 100)
        {
            decimal dividRet = OperationsDec.Divide(valueDec, nearest);

            decimal multiRet = OperationsDec.Multiply(RoundUp(dividRet), nearest);

            return RoundToInt(multiRet);
        }

        public static Int32 NearRoundDown(decimal valueDec, Int32 nearest = 100)
        {
            decimal dividRet = OperationsDec.Divide(valueDec, nearest);

            decimal multiRet = OperationsDec.Multiply(RoundDown(dividRet), nearest);

            return RoundToInt(multiRet);
        }
    }
}

