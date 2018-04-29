using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Taxing
{
    using BundleVersion = UInt16;

    using Operations;
    using Module.Interfaces.Legalist;
    using Module.Items;

    public abstract class TaxingGuidesBuilder : GeneralGuides, ITaxingGuidesBuilder
    {
        public TaxingGuidesBuilder(BundleVersion version) : base(version)
        {
        }

        public BundleVersion BuilderVersion()
        {
            return InternalVersion;
        }

        public ITaxingGuides BuildPeriodGuides(Period period)
        {
            return new TaxingGuides(period);
        }
    }
}
