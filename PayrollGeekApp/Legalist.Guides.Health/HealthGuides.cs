using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Health
{
    using Module.Interfaces.Legalist;
    using Module.Items;
    using Operations;

    public class HealthGuides : IHealthGuides
    {
        protected Period InternalPeriod { get; set; }

        public HealthGuides(Period period)
        {
            this.InternalPeriod = period;
        }
    }
}
