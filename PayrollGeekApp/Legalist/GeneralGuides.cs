using System;

namespace ElementsLib.Legalist.Guides
{
    using BundleVersion = UInt16;

    using Module.Items;

    public class GeneralGuides
    {
        protected GeneralGuides(BundleVersion version)
        {
            InternalVersion = version;
        }

        protected BundleVersion InternalVersion { get; private set; }

        public bool IsPeriodValid(Period period)
        {
            return (period.YearUInt() == InternalVersion);
        }
    }
}