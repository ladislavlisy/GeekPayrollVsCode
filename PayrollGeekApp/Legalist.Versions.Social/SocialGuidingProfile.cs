using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Versions.Social
{
    using Module.Interfaces.Legalist;
    using Module.Items;
    public class SocialGuidingProfile : ISocialProfile
    {
        protected Period InternalPeriod { get; set; }
        protected ISocialGuides InternalGuides { get; set; }

        public SocialGuidingProfile(Period period, ISocialGuides guides)
        {
            this.InternalPeriod = period;

            this.InternalGuides = guides;
        }

        public ISocialGuides Guides()
        {
            return InternalGuides;
        }
    }
}
