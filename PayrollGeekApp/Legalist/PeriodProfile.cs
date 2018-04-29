using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist
{
    using Module.Interfaces.Legalist;
    using Module.Items;

    public class PeriodProfile : IPeriodProfile
    {
        public PeriodProfile(Period period, IBundleProfile bundle)
        {
            InternalPeriod = period;

            InternalEmploy = bundle.BuildEmployProfile(period);
            InternalHealth = bundle.BuildHealthProfile(period);
            InternalSocial = bundle.BuildSocialProfile(period);
            InternalTaxing = bundle.BuildTaxingProfile(period);
            InternalPenzix = bundle.BuildPenzixProfile(period);
        }

        protected Period InternalPeriod { get; set; }

        protected IEmployProfile InternalEmploy { get; set; }
        protected IHealthProfile InternalHealth { get; set; }
        protected ISocialProfile InternalSocial { get; set; }
        protected ITaxingProfile InternalTaxing { get; set; }
        protected IPenzixProfile InternalPenzix { get; set; }

        public bool IsPeriodValid(Period period)
        {
            return (InternalPeriod == period);
        }

        public IEmployProfile Employ()
        {
            return InternalEmploy;
        }
        public IHealthProfile Health()
        {
            return InternalHealth;
        }
        public ISocialProfile Social()
        {
            return InternalSocial;
        }
        public ITaxingProfile Taxing()
        {
            return InternalTaxing;
        }
        public IPenzixProfile Penzix()
        {
            return InternalPenzix;
        }
    }
}
