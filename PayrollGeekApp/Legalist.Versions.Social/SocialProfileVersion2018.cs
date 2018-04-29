using System;

namespace ElementsLib.Legalist.Versions.Social
{
    using Profiles.Social;
    using Module.Interfaces.Legalist;
    using Module.Items;

    internal class SocialProfileVersion2018 : ISocialProfilePrototype
    {
        public ISocialProfile CreatePeriodProfile(Period period, ISocialGuides guides)
        {
            return new SocialGuidingProfile(period, guides);
        }
    }
}
