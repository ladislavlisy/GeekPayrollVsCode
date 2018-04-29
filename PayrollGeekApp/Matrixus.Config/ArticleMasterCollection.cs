using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ElementsLib.Matrixus.Config
{
    using ConfigRole = UInt16;
    using ConfigItem = Module.Interfaces.Matrixus.IArticleConfigMaster;
    using ConfigData = Module.Interfaces.Permadom.ArticleRoleConfigData;
    using ConfigPair = KeyValuePair<UInt16, Module.Interfaces.Matrixus.IArticleConfigMaster>;

    using Module.Libs;
    using Module.Interfaces.Elements;
    using Module.Interfaces.Matrixus;
    using Elements.Config;

    public class ArticleMasterCollection : GeneralConfigCollection<ConfigItem, ConfigRole>, IArticleMasterCollection
    {
        public ArticleMasterCollection() : base()
        {
        }

        public void LoadConfigData(Assembly configAssembly, IEnumerable<ConfigData> configList, IArticleConfigFactory configFactory)
        {
            IEnumerable<ConfigPair> configTypeList = configList.Select((c) => (new ConfigPair(
                c.Role, configFactory.CreateMasterItem(configAssembly, c.Role, c.Name, c.Path)))).ToList();

            ConfigureModel(configTypeList);
        }

        public ConfigItem FindArticleConfig(ConfigRole modelRole)
        {
            ConfigItem configModel = FindConfigByCode(modelRole);

            return configModel;
        }
    }
}
