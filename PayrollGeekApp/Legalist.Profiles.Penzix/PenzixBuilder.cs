using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Profiles.Penzix
{
    using Module.Items;
    using Module.Interfaces.Legalist;
    using Guides.Penzix;

    public class PenzixBuilder : IPenzixBuilder
    {
        public PenzixBuilder(IPenzixGuidesBuilder guidesBuilder, IPenzixProfilePrototype profilePrototype)
        {
            InternalGuides = guidesBuilder;

            InternalProfile = profilePrototype;
        }

        protected IPenzixGuidesBuilder InternalGuides { get; set; }

        protected IPenzixProfilePrototype InternalProfile { get; set; }
        public IPenzixProfile BuildPeriodProfile(Period period)
        {
            IPenzixGuides guides = InternalGuides.BuildPeriodGuides(period);

            return InternalProfile.CreatePeriodProfile(period, guides);
        }
    }
}
