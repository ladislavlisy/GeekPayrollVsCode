using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Interfaces.Legalist
{
    using Items;
    using System.Reflection;

    public interface IBundleVersionCollection
    {
        void InitBundleProfiles(Assembly bundleAssembly, IBundleVersionFactory bundleFactory);
        void LoadBundleProfiles(Assembly bundleAssembly, IBundleVersionFactory bundleFactory);
        IPeriodProfile GetPeriodProfile(Period period);
    }
}
