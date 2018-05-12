using System;
using System.Collections.Generic;
using System.Reflection;

namespace ElementsLib.Module.Interfaces.Matrixus
{
    using ConfigCode = UInt16;
    using ConfigBase = Module.Interfaces.Matrixus.IArticleConfigFeatures;
    using ConfigType = UInt16;
    using ConfigBind = UInt16;
    using ConfigSort = Int32;

    using ConfigItem = Module.Interfaces.Matrixus.IArticleConfigDetail;
    using ConfigData = Module.Interfaces.Permadom.ArticleCodeConfigData;
    using SourceItem = Module.Interfaces.Elements.IArticleSource;
    using SourceVals = Module.Interfaces.Elements.ISourceValues;
    using SourceErrs = String;

    public interface IArticleDetailCollection : IConfigCollection<ConfigItem, ConfigCode>
    {
        void LoadConfigData(IArticleMasterCollection masterStore, IEnumerable<ConfigData> configList, IArticleConfigFactory configFactory);
        IDictionary<ConfigCode, ConfigSort> Ranks();
        ResultMonad.Result<SourceItem, SourceErrs> CloneInstanceForCode(ConfigCode configCode, SourceVals sourceVals);
        ConfigType GetConfigType(ConfigCode configCode);
        ConfigBind GetConfigBind(ConfigCode configCode);
        IEnumerable<ConfigCode> GetSuccessQueue(ConfigCode configCode);
    }
}
