using System;

namespace ElementsLib.Legalist.Versions.Social
{
    using TAmountDec = Decimal;
    using TAmountInt = Int32;

    using Config;
    using Guides.Social;
    using Module.Items;

    public class SocialGuidesVersion2018 : SocialGuidesBuilder
    {
        public SocialGuidesVersion2018() : base(SocialPropertiesVersion2018.VERSION_MIN,
            SocialPropertiesVersion2018.BASIS_MONTHLY_MINIMUM,
            SocialPropertiesVersion2018.BASIS_ANNUAL_MAXIMUM,
            SocialPropertiesVersion2018.FACTOR_EMPLOYER,
            SocialPropertiesVersion2018.FACTOR_EMPLOYER_HIGHER,
            SocialPropertiesVersion2018.FACTOR_EMPLOYEE,
            SocialPropertiesVersion2018.FACTOR_EMPLOYEE_GARANT,
            SocialPropertiesVersion2018.FACTOR_REDUCE_GARANT,
            SocialPropertiesVersion2018.INCOME_EMPLOY_MARGIN,
            SocialPropertiesVersion2018.INCOME_AGREEM_MARGIN)
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
        public override TAmountDec FactorEmployer(Period period)
        {
            return _FactorEmployer;
        }
        public override TAmountDec FactorEmployerHigher(Period period)
        {
            return _FactorEmployerHigher;
        }
        public override TAmountDec FactorEmployee(Period period)
        {
            return _FactorEmployee;
        }
        public override TAmountDec FactorEmployeeGarant(Period period)
        {
            return _FactorEmployeeGarant;
        }
        public override TAmountDec FactorReduceGarant(Period period)
        {
            return _FactorReduceGarant;
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