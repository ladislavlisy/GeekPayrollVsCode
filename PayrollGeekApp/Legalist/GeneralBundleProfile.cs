using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist
{
    using BundleVersion = UInt16;

    using Module.Interfaces.Legalist;
    using Module.Items;

    public class GeneralBundleProfile : IBundleProfile
    {
        public const BundleVersion BUNDLE_VERSION_NONE = 0;
        public GeneralBundleProfile(BundleVersion version)
        {
            InternalVersion = version;
        }

        protected BundleVersion InternalVersion { get; set; }
        protected IEmployBuilder EmployVersionBuilder { get; set; }
        protected IHealthBuilder HealthVersionBuilder { get; set; }
        protected ISocialBuilder SocialVersionBuilder { get; set; }
        protected ITaxingBuilder TaxingVersionBuilder { get; set; }
        protected IPenzixBuilder PenzixVersionBuilder { get; set; }

        public IEmployProfile BuildEmployProfile(Period period)
        {
            return EmployVersionBuilder.BuildPeriodProfile(period);
        }
        public IHealthProfile BuildHealthProfile(Period period)
        {
            return HealthVersionBuilder.BuildPeriodProfile(period);
        }
        public ISocialProfile BuildSocialProfile(Period period)
        {
            return SocialVersionBuilder.BuildPeriodProfile(period);
        }
        public ITaxingProfile BuildTaxingProfile(Period period)
        {
            return TaxingVersionBuilder.BuildPeriodProfile(period);
        }
        public IPenzixProfile BuildPenzixProfile(Period period)
        {
            return PenzixVersionBuilder.BuildPeriodProfile(period);
        }
    }
}
