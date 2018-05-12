using System;

namespace ElementsLib.Legalist.Rounding
{
    using Operations;
    public static class RoundingDec
    {
        public static decimal RoundUp(decimal valueDec)
        {
            decimal roundRet = decimal.Ceiling(Math.Abs(valueDec));

            return (valueDec < 0m ? decimal.Negate(roundRet) : roundRet);
        }

        public static decimal RoundDown(decimal valueDec)
        {
            decimal roundRet = decimal.Floor(Math.Abs(valueDec));

            return (valueDec < 0m ? decimal.Negate(roundRet) : roundRet);
        }

        public static decimal NearRoundUp(decimal valueDec, Int32 nearest = 100)
        {
            decimal dividRet = OperationsDec.Divide(valueDec, nearest);

            decimal multiRet = OperationsDec.Multiply(RoundUp(dividRet), nearest);

            return multiRet;
        }


        public static decimal NearRoundDown(decimal valueDec, Int32 nearest = 100)
        {
            decimal dividRet = OperationsDec.Divide(valueDec, nearest);

            decimal multiRet = OperationsDec.Multiply(RoundDown(dividRet), nearest);

            return multiRet;
        }
    }
}

