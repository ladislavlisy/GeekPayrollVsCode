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
    using Module.Json;
    using Module.Libs;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class PositionScheduleSource : ISourceValues, ICloneable
    {
        public TSeconds ShiftLiable { get; set; }
        public TSeconds ShiftActual { get; set; }
        public WorkScheduleType ScheduleType { get; set; }

        public PositionScheduleSource()
        {
            ShiftLiable = 0;
            ShiftActual = 0;
            ScheduleType = WorkScheduleType.SCHEDULE_NORMALY_WEEK;
        }

        public PositionScheduleSource(TSeconds shiftLiable, TSeconds shiftActual, WorkScheduleType scheduleType)
        {
            ShiftLiable = shiftLiable;
            ShiftActual = shiftActual;
            ScheduleType = scheduleType;
        }

        public virtual object Clone()
        {
            PositionScheduleSource cloneSource = (PositionScheduleSource)this.MemberwiseClone();

            cloneSource.ShiftLiable = this.ShiftLiable;
            cloneSource.ShiftActual = this.ShiftActual;
            cloneSource.ScheduleType = this.ScheduleType;

            return cloneSource;
        }

    }
}
