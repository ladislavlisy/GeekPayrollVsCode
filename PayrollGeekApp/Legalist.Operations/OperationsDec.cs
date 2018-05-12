using System;

namespace ElementsLib.Legalist.Operations
{
    public static class OperationsDec
    {
        public static decimal Multiply(decimal op1, decimal op2)
        {
            return decimal.Multiply(op1, op2);
        }

        public static decimal Divide(decimal op1, decimal div)
        {
            if (div == 0m)
            {
                return 0m;
            }
            return decimal.Divide(op1, div);
        }

        public static decimal MultiplyAndDivide(decimal op1, decimal op2, decimal div)
        {
            if (div == 0m)
            {
                return 0m;
            }
            decimal multiRet = decimal.Multiply(op1, op2);

            decimal dividRet = decimal.Divide(multiRet, div);

            return dividRet;
        }

        public static decimal DecimalCast(Int32 number)
        {
            return new decimal(number);
        }

        public static decimal MinIncMaxDecValue(decimal valueToMinMax, decimal accValueToMax, decimal minLimitTo, decimal maxLimitTo)
        {
            decimal minBase = MinIncValue(valueToMinMax, minLimitTo);

            decimal maxBase = MaxDecAccumValue(minBase, accValueToMax, maxLimitTo);

            return maxBase;
        }

        public static decimal MaxDecAccumValue(decimal valueToMax, decimal accumToMax, decimal maxLimitTo)
        {
            if (maxLimitTo > 0m)
            {
                decimal valueToReduce = Math.Min(decimal.Add(valueToMax, accumToMax), maxLimitTo);

                return Math.Max(0, decimal.Subtract(valueToReduce, accumToMax));
            }
            return valueToMax;
        }

        public static decimal MaxDecAccumAbove(decimal valueToMax, decimal accumToMax, decimal maxLimitTo)
        {
            if (maxLimitTo > 0m)
            {
                decimal underToLimits = MaxDecAccumValue(valueToMax, accumToMax, maxLimitTo);

                return Math.Max(0, valueToMax - underToLimits);
            }
            return decimal.Zero;
        }

        public static decimal MinIncValue(decimal valueToMin, decimal minLimitTo)
        {
            if (minLimitTo > 0m)
            {
                if (minLimitTo > valueToMin)
                {
                    return minLimitTo;
                }
            }
            return valueToMin;
        }

        public static decimal MaxDecValue(decimal valueToMax, decimal maxLimitTo)
        {
            if (maxLimitTo > 0m)
            {
                return Math.Min(valueToMax, maxLimitTo);
            }
            return valueToMax;
        }

        public static decimal SuppressNegative(bool suppress, decimal valueDec)
        {
            if (suppress && valueDec < decimal.Zero)
            {
                return decimal.Zero;
            }
            return valueDec;
        }

    }
}

