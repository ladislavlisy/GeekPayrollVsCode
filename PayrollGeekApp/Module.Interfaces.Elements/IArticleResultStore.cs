using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Interfaces.Elements
{
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;

    public interface IArticleResultStore : IEnumerable<KeyValuePair<IArticleTarget, ResultPack>>
    {
        IEnumerable<IArticleTarget> GetTargets();
        IEnumerable<ResultPair> GetModel();
    }
}
