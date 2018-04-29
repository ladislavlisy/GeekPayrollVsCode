using System;

namespace ElementsLib.Legalist.Versions.Penzix
{
    using Profiles.Penzix;
    using Module.Interfaces.Legalist;
    using Module.Items;

    internal class PenzixProfileVersion2018 : IPenzixProfilePrototype
    {
        public IPenzixProfile CreatePeriodProfile(Period period, IPenzixGuides guides)
        {
            return new PenzixGuidingProfile(period, guides);
        }
    }
}
