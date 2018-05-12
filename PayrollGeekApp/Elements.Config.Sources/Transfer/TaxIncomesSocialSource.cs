using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using Legalist.Constants;
    using Module.Interfaces.Elements;

    public class TaxIncomesSocialSource : ISourceValues, ICloneable
    {

        public TaxIncomesSocialSource()
        {
        }

        public virtual object Clone()
        {
            TaxIncomesSocialSource cloneSource = (TaxIncomesSocialSource)this.MemberwiseClone();


            return cloneSource;
        }

    }
}
