using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Taxing
{
    using BundleVersion = UInt16;

    using Module.Interfaces.Legalist;
    using Module.Items;

    public interface ITaxingGuidesBuilder
    {
        BundleVersion BuilderVersion();
        ITaxingGuides BuildPeriodGuides(Period period);
    }
}
