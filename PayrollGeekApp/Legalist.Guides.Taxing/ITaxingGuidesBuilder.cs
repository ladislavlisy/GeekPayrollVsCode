using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Taxing
{
    using BundleVersion = UInt16;
    using TAmountDec = Decimal;
    using TAmountInt = Int32;

    using Module.Interfaces.Legalist;
    using Module.Items;
    using Constants;

    public interface ITaxingGuidesBuilder
    {
        BundleVersion BuilderVersion();
        ITaxingGuides BuildPeriodGuides(Period period);

        TAmountInt AllowancePayer(Period period);
        TAmountInt AllowanceDisab1st(Period period);
        TAmountInt AllowanceDisab2nd(Period period);
        TAmountInt AllowanceDisab3rd(Period period);
        TAmountInt AllowanceStudy(Period period);
        TAmountInt AllowanceChild1st(Period period);
        TAmountInt AllowanceChild2nd(Period period);
        TAmountInt AllowanceChild3rd(Period period);
        TAmountDec FactorAdvances(Period period);
        TAmountDec FactorWithhold(Period period);
        TAmountDec FactorSolidary(Period period);
        TAmountInt MinValidAmountOfTaxBonus(Period period);
        TAmountInt MaxValidAmountOfTaxBonus(Period period);
        TAmountInt MinValidIncomeOfTaxBonus(Period period);
        TAmountInt MaxValidIncomeOfRounding(Period period);
        TAmountInt MaxTaskAgrIncomeWithhold(Period period);
        TAmountInt MaxLoLevelIncomeWithhold(Period period);
        TaxingPartnerIncome TaxPartnerIncomeWithhold(Period period);
        TAmountInt MaxHealthAnnualBasisAdvance(Period period);
        TAmountInt MaxSocialAnnualBasisAdvance(Period period);
        TAmountInt MaxHealthAnnualBasisWithhold(Period period);
        TAmountInt MaxSocialAnnualBasisWithhold(Period period);
        TAmountInt MinValidIncomeOfSolidary(Period period);
    }
}
