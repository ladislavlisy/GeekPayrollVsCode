using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Profiles.Employ
{
    using Module.Items;
    using Module.Interfaces.Legalist;
    using Guides.Employ;

    public class EmployBuilder : IEmployBuilder
    {
        public EmployBuilder(IEmployGuidesBuilder guidesBuilder, IEmployProfilePrototype profilePrototype)
        {
            InternalGuides = guidesBuilder;

            InternalProfile = profilePrototype;
        }

        protected IEmployGuidesBuilder InternalGuides { get; set; }

        protected IEmployProfilePrototype InternalProfile { get; set; }
        public IEmployProfile BuildPeriodProfile(Period period)
        {
            IEmployGuides guides = InternalGuides.BuildPeriodGuides(period);

            return InternalProfile.CreatePeriodProfile(period, guides);
        }
    }
}
