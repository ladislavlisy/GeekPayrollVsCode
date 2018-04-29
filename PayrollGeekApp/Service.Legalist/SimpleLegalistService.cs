using System;

namespace ElementsLib.Service.Legalist
{
    using ElementsLib.Legalist.Config;
    using ElementsLib.Module.Interfaces.Legalist;

    public class SimpleLegalistService : LegalistService
    {
        public SimpleLegalistService() : base()
        {
            ModuleAssembly = typeof(LegalistService).Assembly;
        }

        public void InitializeService()
        {
            IBundleVersionFactory versionFactory = new BundleVersionFactory();

            Initialize(versionFactory);
        }
    }
}
