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

    using DetailData = IEnumerable<Permadom.ArticleCodeConfigData>;
    using MasterData = IEnumerable<Permadom.ArticleRoleConfigData>;

    using SourceItem = Module.Interfaces.Elements.IArticleSource;
    using SourceVals = Module.Interfaces.Elements.ISourceValues;
    using SourceErrs = String;


    public interface IArticleConfigProfile
    {
        void Initialize(Assembly configAssembly, MasterData configRoleData, DetailData configCodeData, IArticleConfigFactory configFactory);
        IDictionary<ConfigCode, ConfigSort> ArticleRanks();
        ResultMonad.Result<SourceItem, SourceErrs> CloneInstanceForCode(ConfigCode configCode, SourceVals sourceVals);
        ConfigType GetConfigType(ConfigCode configCode);
        ConfigBind GetConfigBind(ConfigCode configCode);
        IEnumerable<ConfigCode> GetSuccessQueue(ConfigCode configCode);
    }
}
