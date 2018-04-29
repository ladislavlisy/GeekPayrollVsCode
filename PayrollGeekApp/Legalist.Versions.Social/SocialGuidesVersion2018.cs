using System;

namespace ElementsLib.Legalist.Versions.Social
{
    using Config;
    using Guides.Social;
    using Module.Items;

    public class SocialGuidesVersion2018 : SocialGuidesBuilder
    {
        public SocialGuidesVersion2018() :
            base(SocialPropertiesVersion2018.VERSION_MIN)
        {
        }
    }
}