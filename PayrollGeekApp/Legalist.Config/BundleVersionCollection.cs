using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Legalist.Config
{
    using VersionCode = UInt16;

    using VersionItem = Module.Interfaces.Legalist.IBundleProfile;
    using VersionPair = KeyValuePair<UInt16, Module.Interfaces.Legalist.IBundleProfile>;

    using Module.Interfaces.Legalist;
    using Module.Items;
    using Exceptions;
    using Bundles;
    using System.Reflection;

    public class BundleVersionCollection : IBundleVersionCollection
    {
        const VersionCode VERSION_IMPL_MIN = 2010;

        const VersionCode VERSION_IMPL_MAX = 2018;

        protected IList<VersionCode> VERSION_IMPL_LIST = new List<VersionCode>()
        {
            (VersionCode)2017,
            (VersionCode)2018,
        };

        protected IDictionary<VersionCode, VersionItem> Profiles { get; set; }
        private IBundleProfile DefaultProfile { get; set; }

        public BundleVersionCollection()
        {
            this.Profiles = new Dictionary<VersionCode, VersionItem>();

            this.DefaultProfile = new BundleProfileVersion2018();
        }

        public void InitBundleProfiles(Assembly bundleAssembly, IBundleVersionFactory bundleFactory)
        {
            IEnumerable<VersionPair> versionTypeList = bundleFactory.CreateVersionList(bundleAssembly);

            ConfigureProfiles(versionTypeList, bundleFactory.CreateVersionItem(bundleAssembly, VERSION_IMPL_MAX));
        }

        public void LoadBundleProfiles(Assembly bundleAssembly, IBundleVersionFactory bundleFactory)
        {
            IEnumerable<VersionPair> versionTypeList = VERSION_IMPL_LIST.Select((c) => (new VersionPair(
                bundleFactory.CreateVersionCode(c), bundleFactory.CreateVersionItem(bundleAssembly, c)))).ToList();

            ConfigureProfiles(versionTypeList, bundleFactory.CreateVersionItem(bundleAssembly, VERSION_IMPL_MAX));
        }

        protected void ConfigureProfiles(IEnumerable<VersionPair> versionList, VersionItem defaultProfile)
        {
            Profiles = versionList.ToDictionary(kv => kv.Key, kv => kv.Value);

            DefaultProfile = defaultProfile;
        }

        public IPeriodProfile GetPeriodProfile(Period period)
        {
            var profile = Profiles.FirstOrDefault((p) => IsValidForPeriod(period, p.Key));

            if (profile.Value != null)
            {
                return new PeriodProfile(period, profile.Value);
            }

            if (IsPeriodInFuture(period))
            {
                return new PeriodProfile(period, DefaultProfile);
            }
            else if (IsOutOfPeriod(period))
            {
                throw new OutOfRangeYear();
            }
            else
            {
                throw new NotImplementedYear();
            }
        }

        private bool IsValidForPeriod(Period period, VersionCode profileVersion)
        {
            return (period.YearUInt() == profileVersion);
        }
        private bool IsOutOfPeriod(Period period)
        {
            return (period.YearUInt() <= VERSION_IMPL_MAX && period.YearUInt() < VERSION_IMPL_MIN);
        }
        private bool IsPeriodInFuture(Period period)
        {
            return (period.YearUInt() > VERSION_IMPL_MAX);
        }
    }
}
