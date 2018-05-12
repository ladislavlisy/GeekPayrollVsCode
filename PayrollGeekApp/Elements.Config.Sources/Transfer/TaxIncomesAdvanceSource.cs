using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using Legalist.Constants;
    using Module.Interfaces.Elements;

    public class TaxIncomesAdvanceSource : ISourceValues, ICloneable
    {

        public TaxIncomesAdvanceSource()
        {
        }

        public virtual object Clone()
        {
            TaxIncomesAdvanceSource cloneSource = (TaxIncomesAdvanceSource)this.MemberwiseClone();


            return cloneSource;
        }

    }
}
