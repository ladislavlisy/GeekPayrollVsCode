using System;
using System.Collections.Generic;
using System.Reflection;

namespace ElementsLib.Legalist.Config
{
    using VersionName = String;
    using VersionCode = UInt16;
    using VersionItem = Module.Interfaces.Legalist.IBundleProfile;
    using VersionPair = KeyValuePair<UInt16, Module.Interfaces.Legalist.IBundleProfile>;

    using Module.Common;
    using Module.Interfaces.Legalist;

    public class BundleVersionFactory : IBundleVersionFactory
    {
        private const string NAME_CLASS_PREFIX = "BundleProfile";
        private const string NAME_SPACE_PREFIX = "ElementsLib.Legalist.Bundles";

        public IEnumerable<VersionPair> CreateVersionList(Assembly bundleAssembly)
        {
            IList<VersionPair> versionList = new List<VersionPair>()
            {
                CreateVersionPair(bundleAssembly, 2018),
            };
            return versionList;
        }

        protected VersionPair CreateVersionPair(Assembly bundleAssembly, VersionCode bundleCode)
        {
            VersionCode versionCode = CreateVersionCode(bundleCode);

            VersionItem versionItem = CreateVersionItem(bundleAssembly, bundleCode);

            return new VersionPair(versionCode, versionItem);
        }

        public VersionItem CreateVersionItem(Assembly bundleAssembly, VersionCode versionCode)
        {
            VersionName versionName = CreateVersionName(versionCode);

            VersionItem versionItem = BundleSourceFor(bundleAssembly, versionName);

            return versionItem;
        }

        public VersionCode CreateVersionCode(VersionCode versionCode)
        {
            return versionCode;
        }
        protected VersionName CreateVersionName(VersionCode versionCode)
        {
            return versionCode.ToString();
        }

        protected IBundleProfile BundleSourceFor(Assembly bundleAssembly, VersionName symbolName)
        {
            string symbolClass = ClassNameFor(symbolName);

            string targetClass = "";

            return GeneralClazzFactory<IBundleProfile>.InstanceFor(bundleAssembly, NAME_SPACE_PREFIX, symbolClass, targetClass);
        }

        protected string ClassNameFor(string targetName)
        {
            string className = string.Join("Version", NAME_CLASS_PREFIX, targetName);

            return className;
        }

    }
}
