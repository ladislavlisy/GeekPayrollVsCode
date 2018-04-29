using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Profiles.Taxing
{
    using Module.Items;
    using Module.Interfaces.Legalist;
    using Guides.Taxing;

    public class TaxingBuilder : ITaxingBuilder
    {
        public TaxingBuilder(ITaxingGuidesBuilder guidesBuilder, ITaxingProfilePrototype profilePrototype)
        {
            InternalGuides = guidesBuilder;

            InternalProfile = profilePrototype;
        }

        protected ITaxingGuidesBuilder InternalGuides { get; set; }

        protected ITaxingProfilePrototype InternalProfile { get; set; }
        public ITaxingProfile BuildPeriodProfile(Period period)
        {
            ITaxingGuides guides = InternalGuides.BuildPeriodGuides(period);

            return InternalProfile.CreatePeriodProfile(period, guides);
        }
    }
}
