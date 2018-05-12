using System;

namespace ElementsLib.Module.Interfaces.Legalist
{
    using TAmountDec = Decimal;
    using TAmountInt = Int32;

    using ElementsLib.Legalist.Constants;
    using Items;

    public interface ITaxingProfile
    {
        ITaxingGuides Guides();
        TAmountDec DecRoundUp(TAmountDec valueDec);
        TAmountInt IntRoundUp(TAmountDec valueDec);
        TAmountDec DecRoundDown(TAmountDec valueDec);
        TAmountInt IntRoundDown(TAmountDec valueDec);
        TAmountDec DecRoundUpHundreds(TAmountDec valueDec);
        TAmountDec DecFactorResult(TAmountDec valueDec, TAmountDec factor);
        TAmountInt RebateResult(TAmountDec rebateBasis, TAmountDec rebateApply, TAmountDec rebateClaim);
        TAmountDec TaxableGeneralIncomes(Period evalPeriod, WorkTaxingTerms summarize, 
            Byte statement, Byte declaracy, Byte residency,
            TAmountDec taxableIncome, TAmountDec partnerIncome, TAmountDec excludeIncome);
        TAmountDec TaxableLolevelIncomes(Period evalPeriod, WorkTaxingTerms summarize,
            Byte statement, Byte declaracy, Byte residency,
            TAmountDec taxableIncome, TAmountDec partnerIncome, TAmountDec excludeIncome);
        TAmountDec TaxableAgrWorkIncomes(Period evalPeriod, WorkTaxingTerms summarize,
            Byte statement, Byte declaracy, Byte residency,
            TAmountDec taxableIncome, TAmountDec partnerIncome, TAmountDec excludeIncome);
        TAmountDec TaxablePartnerIncomes(Period evalPeriod, WorkTaxingTerms summarize,
            Byte statement, Byte declaracy, Byte residency,
            TAmountDec taxableIncome, TAmountDec partnerIncome, TAmountDec excludeIncome);
        TAmountDec ExcludeGeneralIncomes(Period evalPeriod, WorkTaxingTerms summarize,
            Byte statement, Byte declaracy, Byte residency,
            TAmountDec taxableIncome, TAmountDec partnerIncome, TAmountDec excludeIncome);
        TAmountDec TaxableIncomesAdvanceTaxingMode(Period evalPeriod, 
            TAmountDec generalIncome, TAmountDec excludeIncome, 
            TAmountDec lolevelIncome, TAmountDec agrtaskIncome, TAmountDec partnerIncome);
        TAmountDec TaxableIncomesWithholdLolevelMode(Period evalPeriod, 
            TAmountDec generalIncome, TAmountDec excludeIncome, 
            TAmountDec lolevelIncome, TAmountDec agrtaskIncome, TAmountDec partnerIncome);
        TAmountDec TaxableIncomesWithholdTaskAgrMode(Period evalPeriod, 
            TAmountDec generalIncome, TAmountDec excludeIncome, 
            TAmountDec lolevelIncome, TAmountDec agrtaskIncome, TAmountDec partnerIncome);
        TAmountDec TaxableIncomesWithholdPartnerMode(Period evalPeriod, 
            TAmountDec generalIncome, TAmountDec excludeIncome, 
            TAmountDec lolevelIncome, TAmountDec agrtaskIncome, TAmountDec partnerIncome);
        TAmountDec TaxableBaseAdvanceTaxingMode(Period evalPeriod, TAmountDec generalIncome);
        TAmountDec TaxableBaseWithholdTaxingMode(Period evalPeriod, TAmountDec generalIncome);
        TAmountDec TaxablePartialAdvanceHealth(Period evalPeriod, TAmountDec generalIncome, TAmountDec annuityIncome);
        TAmountDec CutDownPartialAdvanceHealth(Period evalPeriod, TAmountDec generalIncome, TAmountDec annuityIncome);
        TAmountDec EployerPartialAdvanceHealth(Period evalPeriod, TAmountDec generalIncome, TAmountDec compoundFactor);
        TAmountDec TaxablePartialAdvanceSocial(Period evalPeriod, TAmountDec generalIncome, TAmountDec annuityIncome);
        TAmountDec CutDownPartialAdvanceSocial(Period evalPeriod, TAmountDec generalIncome, TAmountDec annuityIncome);
        TAmountDec EployerPartialAdvanceSocial(Period evalPeriod, TAmountDec generalIncome, TAmountDec employerFactor);

        TAmountDec BasisSolidaryRounded(TAmountDec generalIncome);
    }
}