using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Social
{
    using BundleVersion = UInt16;

    using Operations;
    using Module.Interfaces.Legalist;
    using Module.Items;

    public abstract class SocialGuidesBuilder : GeneralGuides, ISocialGuidesBuilder
    {
        public SocialGuidesBuilder(BundleVersion version) : base(version)
        {
        }

        public BundleVersion BuilderVersion()
        {
            return InternalVersion;
        }

        public ISocialGuides BuildPeriodGuides(Period period)
        {
            return new SocialGuides(period);
        }
    }
}
