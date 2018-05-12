using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using Legalist.Constants;
    using Module.Interfaces.Elements;

    public class InsIncomesSocialSource : ISourceValues, ICloneable
    {

        public InsIncomesSocialSource()
        {
        }

        public virtual object Clone()
        {
            InsIncomesSocialSource cloneSource = (InsIncomesSocialSource)this.MemberwiseClone();


            return cloneSource;
        }

    }
}
