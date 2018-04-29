using System;

namespace ElementsLib.Elements.Config.Sources
{
    using Module.Interfaces.Elements;
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
