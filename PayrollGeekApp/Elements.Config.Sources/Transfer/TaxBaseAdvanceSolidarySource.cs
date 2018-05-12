using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using Legalist.Constants;
    using Module.Interfaces.Elements;

    public class TaxBaseAdvanceSolidarySource : ISourceValues, ICloneable
    {

        public TaxBaseAdvanceSolidarySource()
        {
        }

        public virtual object Clone()
        {
            TaxBaseAdvanceSolidarySource cloneSource = (TaxBaseAdvanceSolidarySource)this.MemberwiseClone();


            return cloneSource;
        }

    }
}
