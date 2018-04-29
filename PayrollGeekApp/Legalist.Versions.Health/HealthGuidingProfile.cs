using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Versions.Health
{
    using Module.Interfaces.Legalist;
    using Module.Items;
    public class HealthGuidingProfile : IHealthProfile
    {
        protected Period InternalPeriod { get; set; }
        protected IHealthGuides InternalGuides { get; set; }

        public HealthGuidingProfile(Period period, IHealthGuides guides)
        {
            this.InternalPeriod = period;

            this.InternalGuides = guides;
        }

        public IHealthGuides Guides()
        {
            return InternalGuides;
        }
    }
}
