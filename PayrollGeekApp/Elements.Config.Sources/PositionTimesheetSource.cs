using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using TSeconds = Int32;

    using Module.Interfaces.Elements;
    using Module.Libs;

    public class PositionTimesheetSource : ISourceValues, ICloneable
    {
        public TSeconds[] HoursSchedule { get; set; }
        public TSeconds[] HoursInPeriod { get; set; }

        public PositionTimesheetSource()
        {
            HoursSchedule = new TSeconds[0];
            HoursInPeriod = new TSeconds[0];
        }

        public PositionTimesheetSource(TSeconds[] hoursSchedule, TSeconds[] hoursInPeriod)
        {
            HoursSchedule = hoursSchedule.ToArray();
            HoursInPeriod = hoursInPeriod.ToArray();
        }

        public virtual object Clone()
        {
            PositionTimesheetSource cloneSource = (PositionTimesheetSource)this.MemberwiseClone();

            cloneSource.HoursSchedule = this.HoursSchedule.ToArray();
            cloneSource.HoursInPeriod = this.HoursInPeriod.ToArray();

            return cloneSource;
        }

    }
}
