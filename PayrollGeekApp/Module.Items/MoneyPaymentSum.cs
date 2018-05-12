using System;

namespace ElementsLib.Module.Items
{
    using TAmountDec = Decimal;
    public class MoneyPaymentSum
    {
        protected TAmountDec InternalBalance { get; set; }
        public MoneyPaymentSum(TAmountDec initBalance)
        {
            InternalBalance = initBalance;
        }
        public TAmountDec Balance()
        {
            return InternalBalance;
        }
        public MoneyPaymentSum Aggregate(TAmountDec other)
        {
            return new MoneyPaymentSum(InternalBalance + other);
        }
    }
}
