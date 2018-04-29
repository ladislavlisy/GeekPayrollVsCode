using System;

namespace ElementsLib.Legalist.Versions.Taxing
{
    using Profiles.Taxing;
    using Module.Interfaces.Legalist;
    using Module.Items;

    internal class TaxingProfileVersion2018 : ITaxingProfilePrototype
    {
        public ITaxingProfile CreatePeriodProfile(Period period, ITaxingGuides guides)
        {
            return new TaxingGuidingProfile(period, guides);
        }
    }
}
