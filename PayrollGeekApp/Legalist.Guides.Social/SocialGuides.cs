using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Social
{
    using Module.Interfaces.Legalist;
    using Module.Items;
    using Operations;

    public class SocialGuides : ISocialGuides
    {
        protected Period InternalPeriod { get; set; }

        public SocialGuides(Period period)
        {
            this.InternalPeriod = period;
        }
    }
}
