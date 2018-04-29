using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Profiles.Health
{
    using Module.Items;
    using Module.Interfaces.Legalist;
    using Guides.Health;

    public class HealthBuilder : IHealthBuilder
    {
        public HealthBuilder(IHealthGuidesBuilder guidesBuilder, IHealthProfilePrototype profilePrototype)
        {
            InternalGuides = guidesBuilder;

            InternalProfile = profilePrototype;
        }

        protected IHealthGuidesBuilder InternalGuides { get; set; }

        protected IHealthProfilePrototype InternalProfile { get; set; }
        public IHealthProfile BuildPeriodProfile(Period period)
        {
            IHealthGuides guides = InternalGuides.BuildPeriodGuides(period);

            return InternalProfile.CreatePeriodProfile(period, guides);
        }
    }
}
