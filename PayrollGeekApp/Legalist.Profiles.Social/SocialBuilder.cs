using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Profiles.Social
{
    using Module.Items;
    using Module.Interfaces.Legalist;
    using Guides.Social;

    public class SocialBuilder : ISocialBuilder
    {
        public SocialBuilder(ISocialGuidesBuilder guidesBuilder, ISocialProfilePrototype profilePrototype)
        {
            InternalGuides = guidesBuilder;

            InternalProfile = profilePrototype;
        }

        protected ISocialGuidesBuilder InternalGuides { get; set; }

        protected ISocialProfilePrototype InternalProfile { get; set; }
        public ISocialProfile BuildPeriodProfile(Period period)
        {
            ISocialGuides guides = InternalGuides.BuildPeriodGuides(period);

            return InternalProfile.CreatePeriodProfile(period, guides);
        }
    }
}
