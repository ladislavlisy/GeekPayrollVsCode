using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using Legalist.Constants;
    using Module.Interfaces.Elements;

    public class TaxIncomesHealthSource : ISourceValues, ICloneable
    {

        public TaxIncomesHealthSource()
        {
        }

        public virtual object Clone()
        {
            TaxIncomesHealthSource cloneSource = (TaxIncomesHealthSource)this.MemberwiseClone();


            return cloneSource;
        }

    }
}
