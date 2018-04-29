using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Social
{
    using BundleVersion = UInt16;

    using Module.Interfaces.Legalist;
    using Module.Items;

    public interface ISocialGuidesBuilder
    {
        BundleVersion BuilderVersion();
        ISocialGuides BuildPeriodGuides(Period period);
    }
}
