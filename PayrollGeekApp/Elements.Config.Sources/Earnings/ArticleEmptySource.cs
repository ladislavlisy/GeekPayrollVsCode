using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using Legalist.Constants;
    using Module.Interfaces.Elements;
    using Module.Libs;

    public class ArticleEmptySource : ISourceValues, ICloneable
    {
        public ArticleEmptySource()
        {
        }
        public virtual object Clone()
        {
            ArticleEmptySource clone = (ArticleEmptySource)this.MemberwiseClone();

            return clone;
        }
    }
}
