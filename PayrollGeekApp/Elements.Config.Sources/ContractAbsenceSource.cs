using System;
using System.Linq;

namespace ElementsLib.Elements.Config.Sources
{
    using TSeconds = Int32;

    using Module.Interfaces.Elements;
    public class ContractAbsenceSource : ISourceValues, ICloneable
    {
        public TSeconds[] HoursInPeriod { get; set; }

        public ContractAbsenceSource()
        {
            HoursInPeriod = new TSeconds[0];
        }

        public ContractAbsenceSource(TSeconds[] hoursInPeriod)
        {
            HoursInPeriod = hoursInPeriod.ToArray();
        }

        public virtual object Clone()
        {
            ContractAbsenceSource cloneSource = (ContractAbsenceSource)this.MemberwiseClone();

            cloneSource.HoursInPeriod = this.HoursInPeriod.ToArray();

            return cloneSource;
        }

    }
}
