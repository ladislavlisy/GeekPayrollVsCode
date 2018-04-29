using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Interfaces.Matrixus
{
    using ConfigRole = UInt16;
    using ConfigItem = Module.Interfaces.Matrixus.IArticleConfigMaster;
    using ConfigData = Module.Interfaces.Permadom.ArticleRoleConfigData;

    using Elements;
    using System.Reflection;

    public interface IArticleMasterCollection : IConfigCollection<ConfigItem, ConfigRole>
    {
        void LoadConfigData(Assembly configAssembly, IEnumerable<ConfigData> configList, IArticleConfigFactory configFactory);
    }
}
