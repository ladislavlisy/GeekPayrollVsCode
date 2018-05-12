using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using TSeconds = Int32;

    using Legalist.Constants;
    using Module.Interfaces.Elements;
    using Module.Libs;

    public class ContractWorkingSource : ISourceValues, ICloneable
    {
        public TSeconds[] HoursInPeriod { get; set; }

        public ContractWorkingSource()
        {
            HoursInPeriod = new TSeconds[0];
        }

        public ContractWorkingSource(TSeconds[] hoursInPeriod)
        {
            HoursInPeriod = hoursInPeriod.ToArray();
        }

        public virtual object Clone()
        {
            ContractWorkingSource cloneSource = (ContractWorkingSource)this.MemberwiseClone();

            cloneSource.HoursInPeriod = this.HoursInPeriod.ToArray();

            return cloneSource;
        }

    }
}
