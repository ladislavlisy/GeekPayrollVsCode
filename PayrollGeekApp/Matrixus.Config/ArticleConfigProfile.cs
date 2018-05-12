using System;
using System.Collections.Generic;
using System.Reflection;

namespace ElementsLib.Matrixus.Config
{
    using ConfigCode = UInt16;
    using ConfigBase = Module.Interfaces.Matrixus.IArticleConfigFeatures;
    using ConfigType = UInt16;
    using ConfigBind = UInt16;
    using ConfigSort = Int32;

    using DetailData = IEnumerable<Module.Interfaces.Permadom.ArticleCodeConfigData>;
    using MasterData = IEnumerable<Module.Interfaces.Permadom.ArticleRoleConfigData>;

    using SourceItem = Module.Interfaces.Elements.IArticleSource;
    using SourceVals = Module.Interfaces.Elements.ISourceValues;
    using SourceErrs = String;

    using Module.Interfaces.Matrixus;

    public class ArticleConfigProfile : IArticleConfigProfile
    {
        protected IArticleDetailCollection detailBundle { get; set; }

        public ArticleConfigProfile()
        {
            detailBundle = new ArticleDetailCollection();
        }

        public void Initialize(Assembly configAssembly, MasterData configRoleData, DetailData configCodeData, IArticleConfigFactory configFactory)
        {
            IArticleMasterCollection masterBundle = new ArticleMasterCollection();

            masterBundle.LoadConfigData(configAssembly, configRoleData, configFactory);

            detailBundle.LoadConfigData(masterBundle, configCodeData, configFactory);
        }
        public IDictionary<ConfigCode, ConfigSort> ArticleRanks()
        {
            return detailBundle.Ranks();
        }
        public ResultMonad.Result<SourceItem, SourceErrs> CloneInstanceForCode(ConfigCode configCode, SourceVals sourceVals)
        {
            return detailBundle.CloneInstanceForCode(configCode, sourceVals);
        }
        public ConfigType GetConfigType(ConfigCode configCode)
        {
            return detailBundle.GetConfigType(configCode);
        }
        public ConfigBind GetConfigBind(ConfigCode configCode)
        {
            return detailBundle.GetConfigBind(configCode);
        }
        public IEnumerable<ConfigCode> GetSuccessQueue(ConfigCode configCode)
        {
            return detailBundle.GetSuccessQueue(configCode);
        }
    }
}
