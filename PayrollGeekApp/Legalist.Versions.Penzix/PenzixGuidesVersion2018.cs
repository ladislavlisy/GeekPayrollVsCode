using System;

namespace ElementsLib.Legalist.Versions.Penzix
{
    using TAmountDec = Decimal;
    using TAmountInt = Int32;

    using Config;
    using Guides.Penzix;
    using Module.Items;

    public class PenzixGuidesVersion2018 : PenzixGuidesBuilder
    {
        public PenzixGuidesVersion2018() : base(PenzixPropertiesVersion2018.VERSION_MIN,
            PenzixPropertiesVersion2018.FACTOR_EMPLOYEE)
        {
        }
        public override TAmountDec FactorEmployee(Period period)
        {
            return _FactorEmployee;
        }
    }
}