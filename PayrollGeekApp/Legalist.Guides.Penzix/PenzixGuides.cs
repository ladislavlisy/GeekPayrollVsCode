using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Penzix
{
    using Module.Interfaces.Legalist;
    using Module.Items;
    using Operations;

    public class PenzixGuides : IPenzixGuides
    {
        protected Period InternalPeriod { get; set; }

        public PenzixGuides(Period period)
        {
            this.InternalPeriod = period;
        }
    }
}
