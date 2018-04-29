using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Penzix
{
    using BundleVersion = UInt16;

    using Operations;
    using Module.Interfaces.Legalist;
    using Module.Items;

    public abstract class PenzixGuidesBuilder : GeneralGuides, IPenzixGuidesBuilder
    {
        public PenzixGuidesBuilder(BundleVersion version) : base(version)
        {
        }

        public BundleVersion BuilderVersion()
        {
            return InternalVersion;
        }

        public IPenzixGuides BuildPeriodGuides(Period period)
        {
            return new PenzixGuides(period);
        }
    }
}
