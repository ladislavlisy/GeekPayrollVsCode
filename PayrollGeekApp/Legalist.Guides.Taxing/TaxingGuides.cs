using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Taxing
{
    using Module.Interfaces.Legalist;
    using Module.Items;
    using Operations;

    public class TaxingGuides : ITaxingGuides
    {
        protected Period InternalPeriod { get; set; }

        public TaxingGuides(Period period)
        {
            this.InternalPeriod = period;
        }
    }
}
