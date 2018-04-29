using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Operations
{
    using PeriodTerm = Byte;
    using PeriodYear = UInt16;
    using PeriodMnth = Byte;
    using TSeconds = Int32;
    using THours = Int32;
    using TDays = Int16;
    using TAnmount = Decimal;
    using TDayOrdinal = Int16;
    using TDay = Byte;
    using TDate = DateTime;

    using Module.Items;

    public class OperationsPeriod
    {
        public const PeriodTerm TERM_BEG_FINISHED = 32;

        public const PeriodTerm TERM_END_FINISHED = 0;

        public const int WEEKSUN_SUNDAY = 0;

        public const int WEEKMON_SUNDAY = 7;

        public const TSeconds TIME_MULTIPLY_SIXTY = 60;

        public const TDays WEEKDAYS_COUNT = 7;

        public static int DayOfWeekMonToSun(int periodDateCwd)
        {
            // DayOfWeek Sunday = 0,
            // Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5, Saturday = 6, 
            if (periodDateCwd == WEEKSUN_SUNDAY)
            {
                return WEEKMON_SUNDAY;
            }
            else
            {
                return periodDateCwd;
            }
        }

        public static TDayOrdinal DaysInMonth(Period period)
        {
            return (TDayOrdinal)DateTime.DaysInMonth(period.Year(), period.Month());
        }

        public static Period Empty()
        {
            return new Period(0, 0);
        }

        public static Period BeginYear(PeriodYear year)
        {
            return new Period(year, 1);
        }

        public static Period EndYear(PeriodYear year)
        {
            return new Period(year, 12);
        }
        public static DateTime BeginOfMonth(Period period)
        {
            return new DateTime(period.Year(), period.Month(), 1);
        }

        public static DateTime EndOfMonth(Period period)
        {
            return new DateTime(period.Year(), period.Month(), DaysInMonth(period));
        }

        public static DateTime DateOfMonth(Period period, TDayOrdinal dayOrdinal)
        {
            TDayOrdinal periodDay = Math.Min(Math.Max((TDayOrdinal)1, dayOrdinal), DaysInMonth(period));

            return new DateTime(period.Year(), period.Month(), periodDay);
        }

        public static int WeekDayOfMonth(Period period, TDayOrdinal dayOrdinal)
        {
            DateTime periodDate = DateOfMonth(period, dayOrdinal);

            int periodDateCwd = (int)periodDate.DayOfWeek;

            return DayOfWeekMonToSun(periodDateCwd);
        }
        public static int WorkingSecondsDaily(THours workingHours)
        {
            Int32 secondsInHour = (TIME_MULTIPLY_SIXTY * TIME_MULTIPLY_SIXTY);

            return (workingHours * secondsInHour);
        }

        public static int WorkingSecondsWeekly(TDays workingDays, THours workingHours)
        {
            Int32 secondsDaily = WorkingSecondsDaily(workingHours);

            return (workingDays * secondsDaily);
        }

        public static TDay DateFromInPeriod(Period period, TDate? dateFrom)
        {
            PeriodTerm dayTermFrom = TERM_BEG_FINISHED;

            DateTime periodDateBeg = new DateTime(period.Year(), period.Month(), 1);

            if (dateFrom != null)
            {
                dayTermFrom = (TDay)dateFrom.Value.Day;
            }

            if (dateFrom == null || dateFrom < periodDateBeg)
            {
                dayTermFrom = 1;
            }
            return dayTermFrom;
        }

        public static TDay DateEndsInPeriod(Period period, DateTime? dateEnds)
        {
            PeriodTerm dayTermEnd = TERM_END_FINISHED;
            TDay daysPeriod = (TDay)DateTime.DaysInMonth(period.Year(), period.Month());

            DateTime periodDateEnd = new DateTime(period.Year(), period.Month(), (int)daysPeriod);

            if (dateEnds != null)
            {
                dayTermEnd = (TDay)dateEnds.Value.Day;
            }

            if (dateEnds == null || dateEnds > periodDateEnd)
            {
                dayTermEnd = (TDay)daysPeriod;
            }
            return dayTermEnd;
        }

        public static TSeconds[] WeekSchedule(Period period, TSeconds secondsWeekly, TDays workdaysWeekly)
        {
            TSeconds secondsDaily = (secondsWeekly / Math.Min(workdaysWeekly, WEEKDAYS_COUNT));

            TSeconds secRemainder = secondsWeekly - (secondsDaily * workdaysWeekly);

            TSeconds[] weekSchedule = Enumerable.Range(1, 7).
                Select((x) => (WeekDaySeconds(x, workdaysWeekly, secondsDaily, secRemainder))).ToArray();

            return weekSchedule;
        }

        private static TSeconds WeekDaySeconds(int dayOrdinal, int daysOfWork, TSeconds secondsDaily, TSeconds secRemainder)
        {
            if (dayOrdinal < daysOfWork)
            {
                return secondsDaily;
            }
            else if (dayOrdinal == daysOfWork)
            {
                return secondsDaily + secRemainder;
            }
            return (0);
        }

        public static TSeconds[] MonthSchedule(Period period, TSeconds[] weekSchedule)
        {
            int periodDaysCount = DaysInMonth(period);

            int periodBeginCwd = WeekDayOfMonth(period, 1);

            TSeconds[] monthSchedule = Enumerable.Range(1, periodDaysCount).
                Select((x) => (SecondsFromWeekSchedule(period, weekSchedule, x, periodBeginCwd))).ToArray();

            return monthSchedule;
        }

        private static TSeconds SecondsFromWeekSchedule(Period period, TSeconds[] weekSchedule, int dayOrdinal, int periodBeginCwd)
        {
            int dayOfWeek = DayOfWeekFromOrdinal(dayOrdinal, periodBeginCwd);

            int indexWeek = (dayOfWeek - 1);

            if (indexWeek < 0 || indexWeek >= weekSchedule.Length)
            {
                return 0;
            }
            return weekSchedule[indexWeek];
        }

        private static TSeconds SecondsFromScheduleSeq(Period period, TSeconds[] timeTable, int dayOrdinal, uint dayFromOrd, uint dayEndsOrd)
        {
            if (dayOrdinal < dayFromOrd || dayOrdinal > dayEndsOrd)
            {
                return 0;
            }

            int indexTable = (dayOrdinal - (Int32)dayFromOrd);

            if (indexTable < 0 || indexTable >= timeTable.Length)
            {
                return 0;
            }

            return timeTable[indexTable];
        }

        private static int DayOfWeekFromOrdinal(int dayOrdinal, int periodBeginCwd)
        {
            // dayOrdinal 1..31
            // periodBeginCwd 1..7
            // dayOfWeek 1..7

            int dayOfWeek = (((dayOrdinal - 1) + (periodBeginCwd - 1)) % 7) + 1;

            return dayOfWeek;
        }

        public static TSeconds[] TimesheetSchedule(Period period, TSeconds[] monthSchedule, TDay dayOrdFrom, TDay dayOrdEnds)
        {
            TSeconds[] timeSheet = monthSchedule.Select((x, i) => (HoursFromCalendar(dayOrdFrom, dayOrdEnds, (TDay)i, x))).ToArray();

            return timeSheet;
        }

        public static TSeconds[] TimesheetAbsence(Period period, TSeconds[] absenceSchedule, TDay dayOrdFrom, TDay dayOrdEnds)
        {
            int periodDaysCount = DaysInMonth(period);

            TSeconds[] monthSchedule = Enumerable.Range(1, periodDaysCount).
                Select((x) => (SecondsFromScheduleSeq(period, absenceSchedule, x, dayOrdFrom, dayOrdEnds))).ToArray();

            return monthSchedule;
        }

        private static int HoursFromCalendar(TDay dayOrdFrom, TDay dayOrdEnds, TDay dayIndex, TSeconds workSeconds)
        {
            TDay dayOrdinal = (TDay)(dayIndex + 1);

            TSeconds workingDay = workSeconds;

            if (dayOrdFrom > dayOrdinal)
            {
                workingDay = 0;
            }
            if (dayOrdEnds < dayOrdinal)
            {
                workingDay = 0;
            }
            return workingDay;
        }

        public static TSeconds TotalTimesheetHours(TSeconds[] monthTimesheet)
        {
            if (monthTimesheet == null)
            {
                return 0;
            }
            TSeconds timesheetHours = monthTimesheet.Aggregate(0, (agr, dh) => (agr + dh));

            return timesheetHours;
        }

    }
}
