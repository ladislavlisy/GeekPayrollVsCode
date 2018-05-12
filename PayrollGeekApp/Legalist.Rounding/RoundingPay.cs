using System;

namespace ElementsLib.Legalist.Rounding
{
    using Operations;
    class RoundingPay
    {
        public static decimal MonthlyAmountWithWorkingHours(decimal amountMonthly, decimal scheduleFactor, int scheduledHours, int workingsHours)
        {
            decimal amountFactor = FactorizeAmount(amountMonthly, scheduleFactor);

            decimal paymentValue = PaymentFromAmount(amountFactor, scheduledHours, workingsHours);

            return RoundingDec.RoundUp(paymentValue);
        }

        public static decimal FactorizeAmount(decimal amount, decimal factor)
        {
            decimal result = OperationsDec.Multiply(amount, factor);

            return result;
        }

        public static decimal PaymentFromAmount(decimal amountMonthly, Int32 scheduledHours, Int32 workingsHours)
        {
            Int32 totalHours = TotalHoursForPayment(scheduledHours, workingsHours);

            decimal payment = OperationsDec.MultiplyAndDivide(totalHours, amountMonthly, scheduledHours);

            return payment;
        }

        public static Int32 TotalHoursForPayment(Int32 scheduledHours, Int32 workingsHours)
        {
            Int32 totalsHours = Math.Max(0, workingsHours);

            Int32 resultHours = Math.Min(scheduledHours, totalsHours);

            return resultHours;
        }

    }
}

