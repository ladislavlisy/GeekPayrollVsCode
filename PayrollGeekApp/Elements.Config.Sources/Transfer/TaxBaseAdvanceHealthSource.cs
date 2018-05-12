using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using Legalist.Constants;
    using Module.Interfaces.Elements;

    public class TaxBaseAdvanceHealthSource : ISourceValues, ICloneable
    {

        public TaxBaseAdvanceHealthSource()
        {
        }

        public virtual object Clone()
        {
            TaxBaseAdvanceHealthSource cloneSource = (TaxBaseAdvanceHealthSource)this.MemberwiseClone();


            return cloneSource;
        }

    }
}
