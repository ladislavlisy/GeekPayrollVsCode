using ElementsLib.Module.Interfaces.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements
{
    public class ResultValuesStore : IEnumerable<IArticleResultValues>, ICloneable
    {
        #region FACT_SOURCE_MODEL
        protected IList<IArticleResultValues> model;

        public IEnumerator<IArticleResultValues> GetEnumerator()
        {
            return model.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return model.GetEnumerator();
        }
        public IEnumerable<IArticleResultValues> GetModel()
        {
            return model.ToList();
        }
        #endregion

        public ResultValuesStore()
        {
            model = new List<IArticleResultValues>();
        }

        public ResultValuesStore(IEnumerable<IArticleResultValues> other)
        {
            model = other.ToList();
        }

        public ResultValuesStore Concat(params IArticleResultValues[] values)
        {
            return new ResultValuesStore(model.Concat(values));
        }
        public object Clone()
        {
            ResultValuesStore cloneResult = (ResultValuesStore)this.MemberwiseClone();
            cloneResult.model = this.model.ToList();

            return cloneResult;
        }
    }
}
