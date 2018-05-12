using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using Legalist.Constants;
    using Module.Interfaces.Elements;

    public class TaxBaseAdvanceSource : ISourceValues, ICloneable
    {

        public TaxBaseAdvanceSource()
        {
        }

        public virtual object Clone()
        {
            TaxBaseAdvanceSource cloneSource = (TaxBaseAdvanceSource)this.MemberwiseClone();


            return cloneSource;
        }

    }
}
