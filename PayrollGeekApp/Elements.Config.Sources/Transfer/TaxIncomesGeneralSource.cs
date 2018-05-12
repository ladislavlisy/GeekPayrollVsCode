using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using Legalist.Constants;
    using Module.Interfaces.Elements;

    public class TaxIncomesGeneralSource : ISourceValues, ICloneable
    {

        public TaxIncomesGeneralSource()
        {
        }

        public virtual object Clone()
        {
            TaxIncomesGeneralSource cloneSource = (TaxIncomesGeneralSource)this.MemberwiseClone();


            return cloneSource;
        }

    }
}
