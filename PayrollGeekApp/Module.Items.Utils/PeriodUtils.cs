using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Items.Utils
{
    using PeriodYear = UInt16;
    using PeriodMnth = Byte;
    using TSeconds = Int32;
    using TDay = Byte;
    public static class PeriodUtils
    {
        public static int DayOfWeekMonToSun(int weekDayCode)
        {
            // DayOfWeek 
            // Sunday = 0
            // Monday = 1 
            // Tuesday = 2
            // Wednesday = 3
            // Thursday = 4
            // Friday = 5
            // Saturday = 6
            if (weekDayCode == Period.WEEKSUN_SUNDAY)
            {
                return Period.WEEKMON_SUNDAY;
            }
            else
            {
                return weekDayCode;
            }
        }

        public static Period PeriodWithYearAndMonth(PeriodYear year, PeriodMnth month)
        {
            return new Period(year, month);
        }

        public static Period EmptyPeriod()
        {
            return new Period(Period.PRESENT);
        }

        public static Period BeginYear(PeriodYear year)
        {
            return new Period(year, 1);
        }

        public static Period EndYear(PeriodYear year)
        {
            return new Period(year, 12);
        }

        public static TSeconds[] EmptyMonthSchedule()
        {
            return new TSeconds[31] { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
        }

        public static TSeconds[] ScheduleFromTemplate(TSeconds[] template, TDay from, TDay stop)
        {
            TSeconds[] result = EmptyMonthSchedule();
            for (TDay day = from; day < stop; day++)
            {
                int index = day - 1;
                result[index] = template[index];
            }
            return result;
        }
        public static TSeconds[] ScheduleFromTemplateStopExc(TSeconds[] agr, TSeconds[] template, TDay from, TDay stop)
        {
            TSeconds[] result = agr;
            if (from < stop)
            {
                result = agr.ToArray();
            }
            for (TDay day = from; day < stop; day++)
            {
                int index = day - 1;
                result[index] = template[index];
            }
            return result;
        }
        public static TSeconds[] ScheduleFromTemplateStopInc(TSeconds[] agr, TSeconds[] template, TDay from, TDay stop)
        {
            TSeconds[] result = agr;
            if (from < stop)
            {
                result = agr.ToArray();
            }
            for (TDay day = from; day <= stop; day++)
            {
                int index = day - 1;
                result[index] = template[index];
            }
            return result;
        }
    }
}
