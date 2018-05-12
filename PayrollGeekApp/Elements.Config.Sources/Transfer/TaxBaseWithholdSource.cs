using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using Legalist.Constants;
    using Module.Interfaces.Elements;

    public class TaxBaseWithholdSource : ISourceValues, ICloneable
    {

        public TaxBaseWithholdSource()
        {
        }

        public virtual object Clone()
        {
            TaxBaseWithholdSource cloneSource = (TaxBaseWithholdSource)this.MemberwiseClone();


            return cloneSource;
        }

    }
}
