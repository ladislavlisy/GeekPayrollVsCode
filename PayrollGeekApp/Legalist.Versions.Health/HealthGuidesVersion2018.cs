using System;

namespace ElementsLib.Legalist.Versions.Health
{
    using Config;
    using Guides.Health;
    using Module.Items;

    public class HealthGuidesVersion2018 : HealthGuidesBuilder
    {
        public HealthGuidesVersion2018() :
            base(HealthPropertiesVersion2018.VERSION_MIN)
        {
        }
    }
}