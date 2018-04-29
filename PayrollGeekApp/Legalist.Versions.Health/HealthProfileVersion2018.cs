using System;

namespace ElementsLib.Legalist.Versions.Health
{
    using Profiles.Health;
    using Module.Interfaces.Legalist;
    using Module.Items;

    internal class HealthProfileVersion2018 : IHealthProfilePrototype
    {
        public IHealthProfile CreatePeriodProfile(Period period, IHealthGuides guides)
        {
            return new HealthGuidingProfile(period, guides);
        }
    }
}