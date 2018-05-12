using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using Legalist.Constants;
    using Module.Interfaces.Elements;

    public class TaxIncomesWithholdSource : ISourceValues, ICloneable
    {

        public TaxIncomesWithholdSource()
        {
        }

        public virtual object Clone()
        {
            TaxIncomesWithholdSource cloneSource = (TaxIncomesWithholdSource)this.MemberwiseClone();


            return cloneSource;
        }

    }
}
