using System;
using System.Reflection;

namespace ElementsLib.Service.Legalist
{
    using Module.Items;
    using Module.Interfaces;
    using Module.Interfaces.Legalist;
    using ElementsLib.Legalist.Config;

    public class LegalistService : ILegalistService
    {
        protected Assembly ModuleAssembly { get; set; }

        public LegalistService()
        {
            ModuleAssembly = null;

            VersionFactory = null;

            VersionProfile = null;
        }

        public void Initialize(IBundleVersionFactory versionFactory)
        {
            VersionFactory = versionFactory;

            VersionProfile = new BundleVersionCollection();

            VersionProfile.InitBundleProfiles(ModuleAssembly, versionFactory);
        }

        public IBundleVersionCollection Profile()
        {
            return VersionProfile;
        }

        public IPeriodProfile GetPeriodProfile(Period period)
        {
            return VersionProfile.GetPeriodProfile(period);
        }

        protected IBundleVersionFactory VersionFactory { get; set; }

        protected IBundleVersionCollection VersionProfile { get; set; }
    }
}
