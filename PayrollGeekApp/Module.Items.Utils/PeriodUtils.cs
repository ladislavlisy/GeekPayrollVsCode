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
    using Legalist.Constants;

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
            for (TDay day = from; day <= stop; day++)
            {
                int index = day - 1;
                result[index] = template[index];
            }
            return result;
        }
        public static TSeconds[] ScheduleFromTimesheet(TSeconds[] timesheet, WorkDayPieceType[] pieceForDays, TSeconds[] hoursForDays, TDay from, TDay stop)
        {
            TSeconds[] result = EmptyMonthSchedule();
            for (TDay day = from; day <= stop; day++)
            {
                int indexMonth = day - 1;
                int indexDays = day - from;
                WorkDayPieceType pieceForDay = pieceForDays[indexDays];
                TSeconds hoursForDay = hoursForDays[indexDays];
                TSeconds fullHoursTimesheet = timesheet[indexMonth];
                TSeconds halfHoursTimesheet = fullHoursTimesheet/2;

                switch (pieceForDay)
                {
                    case WorkDayPieceType.WORKDAY_NONE:
                        result[indexMonth] = 0;
                        break;
                    case WorkDayPieceType.WORKDAY_FULL:
                        result[indexMonth] = (fullHoursTimesheet);
                        break;
                    case WorkDayPieceType.WORKDAY_HALF:
                        result[indexMonth] = (halfHoursTimesheet);
                        break;
                    case WorkDayPieceType.WORKDAY_TIME:
                        result[indexMonth] = hoursForDay;
                        break;
                    default:
                        result[indexMonth] = 0;
                        break;
                }
            }
            return result;
        }
        public static TSeconds[] ScheduleBaseSubtract(TSeconds[] template, TSeconds[] subtract, TDay from, TDay stop)
        {
            TSeconds[] result = EmptyMonthSchedule();
            for (TDay day = from; day <= stop; day++)
            {
                int index = day - 1;
                result[index] += template[index];
                result[index] -= subtract[index];
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

        public static TSeconds TotalWeekHours(TSeconds[] template)
        {
            TSeconds result = template.Take(7).Aggregate(0, (agr, x) => (agr + x));

            return result;
        }
        public static TSeconds TotalMonthHours(TSeconds[] template)
        {
            TSeconds result = template.Take(31).Aggregate(0, (agr, x) => (agr + x));

            return result;
        }
    }
}
