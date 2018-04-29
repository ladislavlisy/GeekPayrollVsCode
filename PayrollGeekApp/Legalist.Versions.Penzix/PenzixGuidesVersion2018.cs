using System;

namespace ElementsLib.Legalist.Versions.Penzix
{
    using Config;
    using Guides.Penzix;
    using Module.Items;

    public class PenzixGuidesVersion2018 : PenzixGuidesBuilder
    {
        public PenzixGuidesVersion2018() :
            base(PenzixPropertiesVersion2018.VERSION_MIN)
        {
        }
    }
}