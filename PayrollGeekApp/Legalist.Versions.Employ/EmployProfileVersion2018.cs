using System;

namespace ElementsLib.Legalist.Versions.Employ
{
    using Profiles.Employ;
    using Module.Interfaces.Legalist;
    using Module.Items;

    internal class EmployProfileVersion2018 : IEmployProfilePrototype
    {
        public IEmployProfile CreatePeriodProfile(Period period, IEmployGuides guides)
        {
            return new EmployGuidingProfile(period, guides);
        }

    }
}