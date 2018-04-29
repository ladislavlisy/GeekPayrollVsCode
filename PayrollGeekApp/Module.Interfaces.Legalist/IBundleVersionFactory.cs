using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Interfaces.Legalist
{
    using VersionName = String;
    using VersionCode = UInt16;
    using VersionItem = Module.Interfaces.Legalist.IBundleProfile;
    using VersionPair = KeyValuePair<UInt16, Module.Interfaces.Legalist.IBundleProfile>;

    public interface IBundleVersionFactory
    {
        IEnumerable<VersionPair> CreateVersionList(Assembly bundleAssembly);
        VersionCode CreateVersionCode(VersionCode versionCode);
        VersionItem CreateVersionItem(Assembly bundleAssembly, VersionCode versionCode);
     }
}
