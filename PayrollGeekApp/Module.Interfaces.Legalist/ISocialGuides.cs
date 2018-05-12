using System;

namespace ElementsLib.Module.Interfaces.Legalist
{
    using TAmountDec = Decimal;
    using TAmountInt = Int32;
    public interface ISocialGuides
    {
        TAmountInt BasisMonthlyMinimum();
        TAmountDec BasisAnnualMaximum();
        TAmountDec FactorEmployer();
        TAmountDec FactorEmployerHigher();
        TAmountDec FactorEmployee();
        TAmountDec FactorEmployeeGarant();
        TAmountDec FactorReduceGarant();
        TAmountDec IncomeEmployMargin();
        TAmountDec IncomeAgreemMargin();
    }
}