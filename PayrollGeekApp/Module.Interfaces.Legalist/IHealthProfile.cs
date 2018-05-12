using System;

namespace ElementsLib.Module.Interfaces.Legalist
{
    using TAmountDec = Decimal;

    using ElementsLib.Legalist.Constants;
    using Items;
    public interface IHealthProfile
    {
        IHealthGuides Guides();

        TAmountDec FactorCompound();
        TAmountDec IncludeGeneralIncomes(Period evalPeriod, WorkHealthTerms summarize,
            TAmountDec includeIncome, TAmountDec excludeIncome);
        TAmountDec ExcludeGeneralIncomes(Period evalPeriod, WorkHealthTerms summarize,
            TAmountDec includeIncome, TAmountDec excludeIncome);
    }
}