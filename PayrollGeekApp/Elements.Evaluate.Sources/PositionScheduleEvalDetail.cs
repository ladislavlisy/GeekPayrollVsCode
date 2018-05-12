using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Evaluate.Sources
{
    using TDay = Byte;
    using TSeconds = Int32;
    using TargetPart = UInt16;

    using ElementsLib.Legalist.Constants;
    public class PositionScheduleEvalDetail
    {
        public PositionScheduleEvalDetail()
        {
            PositionPart = 0;
            DateFrom = null;
            DayPeriodFrom = 0;
            DateStop = null;
            DayPeriodStop = 0;
            PositionType = WorkPositionType.POSITION_EXCLUSIVE;
            ScheduleWorks = new TSeconds[0];
        }
        public TargetPart PositionPart { get; set; }
        public DateTime? DateFrom { get; set; }
        public TDay DayPeriodFrom { get; set; }
        public DateTime? DateStop { get; set; }
        public TDay DayPeriodStop { get; set; }
        public WorkPositionType PositionType { get; set; }
        public TSeconds[] ScheduleWorks { get; set; }
    }

    internal class ComparePositionTerms : IComparer<PositionScheduleEvalDetail>
    {
        public int CompareDate(DateTime? x, DateTime? y)
        {
            if (x.HasValue && y.HasValue)
            {
                DateTime xv = x.Value;
                DateTime yv = y.Value;

                return xv.CompareTo(yv);
            }
            else if (x.HasValue)
            {
                return 1;
            }
            else if (y.HasValue)
            {
                return -1;
            }
            return 0;
        }
        public int Compare(PositionScheduleEvalDetail x, PositionScheduleEvalDetail y)
        {
            int compareFrom = CompareDate(x.DateFrom, y.DateFrom);
            if (compareFrom == 0)
            {
                int compareStop = CompareDate(x.DateStop, y.DateStop);
                if (compareStop == 0)
                {
                    return x.PositionPart.CompareTo(y.PositionPart);
                }
                return compareStop;
            }
            return compareFrom;
        }
    }
}
