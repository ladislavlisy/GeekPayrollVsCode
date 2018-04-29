using System;

namespace ElementsLib.Legalist.Versions.Taxing
{
    using Config;
    using Guides.Taxing;
    using Module.Items;

    public class TaxingGuidesVersion2018 : TaxingGuidesBuilder
    {
        public TaxingGuidesVersion2018() :
            base(TaxingPropertiesVersion2018.VERSION_MIN)
        {
        }
    }
}