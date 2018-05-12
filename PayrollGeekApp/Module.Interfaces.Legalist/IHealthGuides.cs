using System;

namespace ElementsLib.Module.Interfaces.Legalist
{
    using TAmountDec = Decimal;
    using TAmountInt = Int32;
    public interface IHealthGuides
    {
        TAmountInt BasisMonthlyMinimum();
        TAmountDec BasisAnnualMaximum();
        TAmountDec FactorCompound();
        TAmountDec IncomeEmployMargin();
        TAmountDec IncomeAgreemMargin();
    }
}