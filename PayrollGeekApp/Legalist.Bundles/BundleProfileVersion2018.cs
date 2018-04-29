using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Bundles
{
    using BundleVersion = UInt16;

    using Guides.Employ;
    using Profiles.Employ;
    using Versions.Employ;
    using Guides.Health;
    using Profiles.Health;
    using Versions.Health;
    using Guides.Social;
    using Profiles.Social;
    using Versions.Social;
    using Guides.Taxing;
    using Profiles.Taxing;
    using Versions.Taxing;
    using Guides.Penzix;
    using Profiles.Penzix;
    using Versions.Penzix;
    using Module.Interfaces.Legalist;

    class BundleProfileVersion2018 : GeneralBundleProfile
    {
        const BundleVersion BUNDLE_VERSION = 2018;
        public BundleProfileVersion2018() : base(BUNDLE_VERSION)
        {
            EmployVersionBuilder = BuildEmployVersion();

            HealthVersionBuilder = BuildHealthVersion();

            SocialVersionBuilder = BuildSocialVersion();

            TaxingVersionBuilder = BuildTaxingVersion();

            PenzixVersionBuilder = BuildPenzixVersion();
        }

        private IEmployBuilder BuildEmployVersion()
        {
            IEmployGuidesBuilder guidesBuilder = new EmployGuidesVersion2018();

            IEmployProfilePrototype profilePrototype = new EmployProfileVersion2018();

            return new EmployBuilder(guidesBuilder, profilePrototype);
        }
        private IHealthBuilder BuildHealthVersion()
        {
            IHealthGuidesBuilder guidesBuilder = new HealthGuidesVersion2018();

            IHealthProfilePrototype profilePrototype = new HealthProfileVersion2018();

            return new HealthBuilder(guidesBuilder, profilePrototype);
        }
        private ISocialBuilder BuildSocialVersion()
        {
            ISocialGuidesBuilder guidesBuilder = new SocialGuidesVersion2018();

            ISocialProfilePrototype profilePrototype = new SocialProfileVersion2018();

            return new SocialBuilder(guidesBuilder, profilePrototype);
        }
        private ITaxingBuilder BuildTaxingVersion()
        {
            ITaxingGuidesBuilder guidesBuilder = new TaxingGuidesVersion2018();

            ITaxingProfilePrototype profilePrototype = new TaxingProfileVersion2018();

            return new TaxingBuilder(guidesBuilder, profilePrototype);
        }
        private IPenzixBuilder BuildPenzixVersion()
        {
            IPenzixGuidesBuilder guidesBuilder = new PenzixGuidesVersion2018();

            IPenzixProfilePrototype profilePrototype = new PenzixProfileVersion2018();

            return new PenzixBuilder(guidesBuilder, profilePrototype);
        }

    }
}
