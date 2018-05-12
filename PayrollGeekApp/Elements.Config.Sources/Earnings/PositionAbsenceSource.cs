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

    public class PositionAbsenceSource : ISourceValues, ICloneable
    {
        public TSeconds[] HoursInPeriod { get; set; }

        public PositionAbsenceSource()
        {
            HoursInPeriod = new TSeconds[0];
        }

        public PositionAbsenceSource(TSeconds[] hoursInPeriod)
        {
            HoursInPeriod = hoursInPeriod.ToArray();
        }

        public virtual object Clone()
        {
            PositionAbsenceSource cloneSource = (PositionAbsenceSource)this.MemberwiseClone();

            cloneSource.HoursInPeriod = this.HoursInPeriod.ToArray();

            return cloneSource;
        }

    }
}
