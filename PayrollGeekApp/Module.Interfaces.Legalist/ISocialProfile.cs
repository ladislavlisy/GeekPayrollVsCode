using System;

namespace ElementsLib.Module.Interfaces.Legalist
{
    using TAmountDec = Decimal;

    using ElementsLib.Legalist.Constants;
    using Items;
    public interface ISocialProfile
    {
        ISocialGuides Guides();

        TAmountDec FactorEmployer();
        TAmountDec IncludeGeneralIncomes(Period evalPeriod, WorkSocialTerms summarize,
            TAmountDec includeIncome, TAmountDec excludeIncome);
        TAmountDec ExcludeGeneralIncomes(Period evalPeriod, WorkSocialTerms summarize,
            TAmountDec includeIncome, TAmountDec excludeIncome);
    }
}