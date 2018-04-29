using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Health
{
    using BundleVersion = UInt16;

    using Module.Interfaces.Legalist;
    using Module.Items;

    public interface IHealthGuidesBuilder
    {
        BundleVersion BuilderVersion();
        IHealthGuides BuildPeriodGuides(Period period);
    }
}
