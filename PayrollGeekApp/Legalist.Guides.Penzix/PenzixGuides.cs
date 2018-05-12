using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Penzix
{
    using TAmountDec = Decimal;
    using TAmountInt = Int32;

    using Module.Interfaces.Legalist;
    using Module.Items;
    using Operations;

    public class PenzixGuides : IPenzixGuides
    {
        protected Period InternalPeriod { get; set; }
        protected TAmountDec __FactorEmployee { get; set; }

        public PenzixGuides(Period period, TAmountDec factorEmployee)
        {
            this.InternalPeriod = period;
        }
        public TAmountDec FactorEmployee()
        {
            return __FactorEmployee;
        }
    }
}
