using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using Legalist.Constants;
    using Module.Interfaces.Elements;

    public class TaxBaseAdvanceSocialSource : ISourceValues, ICloneable
    {

        public TaxBaseAdvanceSocialSource()
        {
        }

        public virtual object Clone()
        {
            TaxBaseAdvanceSocialSource cloneSource = (TaxBaseAdvanceSocialSource)this.MemberwiseClone();


            return cloneSource;
        }

    }
}
