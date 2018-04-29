using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Versions.Penzix
{
    using Module.Interfaces.Legalist;
    using Module.Items;
    public class PenzixGuidingProfile : IPenzixProfile
    {
        protected Period InternalPeriod { get; set; }
        protected IPenzixGuides InternalGuides { get; set; }

        public PenzixGuidingProfile(Period period, IPenzixGuides guides)
        {
            this.InternalPeriod = period;

            this.InternalGuides = guides;
        }

        public IPenzixGuides Guides()
        {
            return InternalGuides;
        }
    }
}
