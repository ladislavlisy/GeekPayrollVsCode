using System;

namespace ElementsLib.Legalist.Versions.Taxing
{
    using TAmountDec = Decimal;
    using TAmountInt = Int32;

    using Config;
    using Guides.Taxing;
    using Module.Items;
    using Constants;

    public class TaxingGuidesVersion2018 : TaxingGuidesBuilder
    {
        public TaxingGuidesVersion2018() : base(TaxingPropertiesVersion2018.VERSION_MIN,
            TaxingPropertiesVersion2018.ALLOWANCE_PAYER,
            TaxingPropertiesVersion2018.ALLOWANCE_DISAB_1ST,
            TaxingPropertiesVersion2018.ALLOWANCE_DISAB_2ND,
            TaxingPropertiesVersion2018.ALLOWANCE_DISAB_3RD,
            TaxingPropertiesVersion2018.ALLOWANCE_STUDY,
            TaxingPropertiesVersion2018.ALLOWANCE_CHILD_1ST,
            TaxingPropertiesVersion2018.ALLOWANCE_CHILD_2ND,
            TaxingPropertiesVersion2018.ALLOWANCE_CHILD_3RD,
            TaxingPropertiesVersion2018.FACTOR_ADVANCES,
            TaxingPropertiesVersion2018.FACTOR_WITHHOLD,
            TaxingPropertiesVersion2018.FACTOR_SOLIDARY,
            TaxingPropertiesVersion2018.MIN_VALID_AMOUNT_OF_TAXBONUS,
            TaxingPropertiesVersion2018.MAX_VALID_AMOUNT_OF_TAXBONUS,
            TaxingPropertiesVersion2018.MIN_VALID_INCOME_OF_TAXBONUS,
            TaxingPropertiesVersion2018.MAX_VALID_INCOME_OF_ROUNDING,
            TaxingPropertiesVersion2018.MAX_TASKAGR_INCOMES_WITHHOLD,
            TaxingPropertiesVersion2018.MAX_LOLEVEL_INCOMES_WITHHOLD,
            TaxingPropertiesVersion2018.TAX_PARTNER_INCOMES_WITHHOLD,
            TaxingPropertiesVersion2018.MAX_HEALTH_ANNUAL_BASIS_ADVANCE,
            TaxingPropertiesVersion2018.MAX_SOCIAL_ANNUAL_BASIS_ADVANCE,
            TaxingPropertiesVersion2018.MAX_HEALTH_ANNUAL_BASIS_WITHHOLD,
            TaxingPropertiesVersion2018.MAX_SOCIAL_ANNUAL_BASIS_WITHHOLD,
            TaxingPropertiesVersion2018.MIN_VALID_INCOME_OF_SOLIDARY)
        {
        }
        public override TAmountInt AllowancePayer(Period period)
        {
            return _AllowancePayer;
        }
        public override TAmountInt AllowanceDisab1st(Period period)
        {
            return _AllowanceDisab1st;
        }
        public override TAmountInt AllowanceDisab2nd(Period period)
        {
            return _AllowanceDisab2nd;
        }
        public override TAmountInt AllowanceDisab3rd(Period period)
        {
            return _AllowanceDisab3rd;
        }
        public override TAmountInt AllowanceStudy(Period period)
        {
            return _AllowanceStudy;
        }
        public override TAmountInt AllowanceChild1st(Period period)
        {
            return _AllowanceChild1st;
        }
        public override TAmountInt AllowanceChild2nd(Period period)
        {
            return _AllowanceChild2nd;
        }
        public override TAmountInt AllowanceChild3rd(Period period)
        {
            return _AllowanceChild3rd;
        }
        public override TAmountDec FactorAdvances(Period period)
        {
            return _FactorAdvances;
        }
        public override TAmountDec FactorWithhold(Period period)
        {
            return _FactorWithhold;
        }
        public override TAmountDec FactorSolidary(Period period)
        {
            return _FactorSolidary;
        }
        public override TAmountInt MinValidAmountOfTaxBonus(Period period)
        {
            return _MinValidAmountOfTaxBonus;
        }
        public override TAmountInt MaxValidAmountOfTaxBonus(Period period)
        {
            return _MaxValidAmountOfTaxBonus;
        }
        public override TAmountInt MinValidIncomeOfTaxBonus(Period period)
        {
            return _MinValidIncomeOfTaxBonus;
        }
        public override TAmountInt MaxValidIncomeOfRounding(Period period)
        {
            return _MaxValidIncomeOfRounding;
        }
        public override TAmountInt MaxTaskAgrIncomeWithhold(Period period)
        {
            return _MaxTaskAgrIncomeWithhold;
        }
        public override TAmountInt MaxLoLevelIncomeWithhold(Period period)
        {
            return _MaxLoLevelIncomeWithhold;
        }
        public override TaxingPartnerIncome TaxPartnerIncomeWithhold(Period period)
        {
            return _TaxPartnerIncomeWithhold;
        }
        public override TAmountInt MaxHealthAnnualBasisAdvance(Period period)
        {
            return _MaxHealthAnnualBasisAdvance;
        }
        public override TAmountInt MaxSocialAnnualBasisAdvance(Period period)
        {
            return _MaxSocialAnnualBasisAdvance;
        }
        public override TAmountInt MaxHealthAnnualBasisWithhold(Period period)
        {
            return _MaxHealthAnnualBasisWithhold;
        }
        public override TAmountInt MaxSocialAnnualBasisWithhold(Period period)
        {
            return _MaxSocialAnnualBasisWithhold;
        }
        public override TAmountInt MinValidIncomeOfSolidary(Period period)
        {
            return _MinValidIncomeOfSolidary;
        }
    }
}