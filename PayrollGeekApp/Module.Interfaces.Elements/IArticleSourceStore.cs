using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Interfaces.Elements
{
    using ConfigCode = UInt16;
    using TargetHead = UInt16;
    using TargetPart = UInt16;
    using TargetSeed = UInt16;
    using TargetSort = UInt16;
    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using TargetData = Module.Interfaces.Permadom.ArticleData;
    using SourcePair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>>;
    using SourcePack = ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>;

    public interface IArticleSourceStore : IEnumerable<KeyValuePair<IArticleTarget, SourcePack>>
    {
        void LoadSourceData(IEnumerable<TargetData> sourceData);
        ICollection<TargetItem> Keys();
        IEnumerable<TargetItem> GetTargets();
        IEnumerable<SourcePair> GetModel();
        void CopyModel(IArticleSourceStore source);
        void AddGeneralItems(IEnumerable<TargetItem> targets);
        TargetItem StoreGeneralItem(TargetHead codeHead, TargetPart codePart, ConfigCode codeBody, TargetSeed seedBody, ISourceValues tagsBody);
        TargetItem StoreGeneralItem(TargetData dataItem);
        void EvolveStream(ConfigCode contractCode, ConfigCode positionCode);
        IList<SourcePair> GetEvaluationPath();
    }
}
