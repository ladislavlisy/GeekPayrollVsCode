using System;

namespace ElementsLib.Legalist.Versions.Health
{
    using TAmountDec = Decimal;
    using TAmountInt = Int32;

    using Config;
    using Guides.Health;
    using Module.Items;

    public class HealthGuidesVersion2018 : HealthGuidesBuilder
    {
        public HealthGuidesVersion2018() : base(HealthPropertiesVersion2018.VERSION_MIN,
            HealthPropertiesVersion2018.BASIS_MONTHLY_MINIMUM,
            HealthPropertiesVersion2018.BASIS_ANNUAL_MAXIMUM,
            HealthPropertiesVersion2018.FACTOR_COMPOUND,
            HealthPropertiesVersion2018.INCOME_EMPLOY_MARGIN,
            HealthPropertiesVersion2018.INCOME_AGREEM_MARGIN)
        {
        }
        public override TAmountInt BasisMonthlyMinimum(Period period)
        {
            return _BasisMonthlyMinimum;
        }
        public override TAmountDec BasisAnnualMaximum(Period period)
        {
            return _BasisAnnualMaximum;
        }
        public override TAmountDec FactorCompound(Period period)
        {
            return _FactorCompound;
        }
        public override TAmountDec IncomeEmployMargin(Period period)
        {
            return _IncomeEmployMargin;
        }
        public override TAmountDec IncomeAgreemMargin(Period period)
        {
            return _IncomeAgreemMargin;
        }
    }
}