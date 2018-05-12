using System;

namespace ElementsLib.Module.Interfaces.Legalist
{
    using ElementsLib.Legalist.Constants;
    using TAmountDec = Decimal;
    using TAmountInt = Int32;
    public interface ITaxingGuides
    {
        TAmountInt AllowancePayer();
        TAmountInt AllowanceDisab1st();
        TAmountInt AllowanceDisab2nd();
        TAmountInt AllowanceDisab3rd();
        TAmountInt AllowanceStudy();
        TAmountInt AllowanceChild1st();
        TAmountInt AllowanceChild2nd();
        TAmountInt AllowanceChild3rd();
        TAmountDec FactorAdvances();
        TAmountDec FactorWithhold();
        TAmountDec FactorSolidary();
        TAmountInt MinValidAmountOfTaxBonus();
        TAmountInt MaxValidAmountOfTaxBonus();
        TAmountInt MinValidIncomeOfTaxBonus();
        TAmountInt MaxValidIncomeOfRounding();
        TAmountInt MaxTaskAgrIncomeWithhold();
        bool TaxTaskAgrIncomeWithhold();
        TAmountInt MaxLoLevelIncomeWithhold();
        bool TaxLoLevelIncomeWithhold();
        TaxingPartnerIncome TaxPartnerIncomeWithhold();
        TAmountInt MaxHealthAnnualBasisAdvance();
        TAmountInt MaxSocialAnnualBasisAdvance();
        TAmountInt MaxHealthAnnualBasisWithhold();
        TAmountInt MaxSocialAnnualBasisWithhold();
        TAmountInt MinValidIncomeOfSolidary();
    }
}