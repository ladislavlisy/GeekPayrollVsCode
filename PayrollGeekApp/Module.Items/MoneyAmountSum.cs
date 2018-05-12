using System;

namespace ElementsLib.Module.Items
{
    using TAmountDec = Decimal;
    public class MoneyAmountSum
    {
        protected TAmountDec InternalBalance { get; set; }
        public MoneyAmountSum(TAmountDec initBalance)
        {
            InternalBalance = initBalance;
        }
        public TAmountDec Balance()
        {
            return InternalBalance;
        }
        public MoneyAmountSum Aggregate(TAmountDec other)
        {
            return new MoneyAmountSum(InternalBalance + other);
        }
    }
}
