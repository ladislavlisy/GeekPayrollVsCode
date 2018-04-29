using System;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using System.Linq;

namespace ElementsLib.Elements
{
    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;

    using Module.Interfaces.Elements;

    public class ArticleResultStore : IArticleResultStore
    {
        #region FACT_SOURCE_MODEL
        protected IDictionary<TargetItem, ResultPack> model;

        public IEnumerator<ResultPair> GetEnumerator()
        {
            return model.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return model.GetEnumerator();
        }
        public ICollection<TargetItem> Keys
        {
            get { return model.Keys; }
        }
        public IEnumerable<TargetItem> GetTargets()
        {
            return model.Keys.ToList();
        }

        public IEnumerable<ResultPair> GetModel()
        {
            return model.ToList();
        }

        #endregion

        public ArticleResultStore()
        {
            model = new Dictionary<TargetItem, ResultPack>();
        }

        public void CopyModel(IArticleResultStore source)
        {
            model = source.GetModel().ToDictionary((kv) => (kv.Key), (kv) => (kv.Value));
        }
    }
}
