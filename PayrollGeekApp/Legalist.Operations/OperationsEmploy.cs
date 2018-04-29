using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Operations
{
    using TSeconds = Int32;
    using THours = Int32;
    using TDays = Int16;
    using TDay = Byte;
    using TDate = DateTime;

    using Module.Items;
    public static class OperationsEmploy
    {
        public const Int32 TIME_MULTIPLY_SIXTY = 60;

        public static int WorkingSecondsDaily(THours workingHours)
        {
            TSeconds secondsInHour = (TIME_MULTIPLY_SIXTY * TIME_MULTIPLY_SIXTY);

            return (workingHours * secondsInHour);
        }

        public static int WorkingSecondsWeekly(TDays workingDays, THours workingHours)
        {
            TSeconds secondsDaily = WorkingSecondsDaily(workingHours);

            return (workingDays * secondsDaily);
        }

        public static TDay DateFromInPeriod(Period period, TDate? dateFrom)
        {
            TDay dayTermFrom = Period.TERM_BEG_FINISHED;

            var periodDateBeg = new DateTime((int)period.Year(), (int)period.Month(), 1);

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

        public static TDay DateStopInPeriod(Period period, DateTime? dateStop)
        {
            TDay dayTermStop = Period.TERM_END_FINISHED;
            TDay daysPeriods = (TDay)DateTime.DaysInMonth((int)period.Year(), (int)period.Month());

            var periodDateEnd = new DateTime((int)period.Year(), (int)period.Month(), (int)daysPeriods);

            if (dateStop != null)
            {
                dayTermStop = (TDay)dateStop.Value.Day;
            }

            if (dateStop == null || dateStop > periodDateEnd)
            {
                dayTermStop = daysPeriods;
            }
            return dayTermStop;
        }

        public static Int32[] WeekSchedule(Period period, Int32 secondsWeekly, Int32 workdaysWeekly)
        {
            Int32 secondsDaily = (secondsWeekly / Math.Min(workdaysWeekly, 7));

            Int32 secRemainder = secondsWeekly - (secondsDaily * workdaysWeekly);

            Int32[] weekSchedule = Enumerable.Range(1, 7).
                Select((x) => (WeekDaySeconds(x, workdaysWeekly, secondsDaily, secRemainder))).ToArray();

            return weekSchedule;
        }

        private static Int32 WeekDaySeconds(int dayOrdinal, int daysOfWork, Int32 secondsDaily, Int32 secRemainder)
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

        public static Int32[] MonthSchedule(Period period, Int32[] weekSchedule)
        {
            int daysInMonth = period.DaysInMonth();

            int beginDayCwd = period.WeekDayOfMonth(1);

            Int32[] monthSchedule = Enumerable.Range(1, daysInMonth).
                Select((x) => (SecondsFromWeekSchedule(period, weekSchedule, x, beginDayCwd))).ToArray();

            return monthSchedule;
        }

        private static Int32 SecondsFromWeekSchedule(Period period, Int32[] weekSchedule, int dayOrdinal, int beginDayCwd)
        {
            int dayOfWeek = DayOfWeekFromOrdinal(dayOrdinal, beginDayCwd);

            int indexWeek = (dayOfWeek - 1);

            if (indexWeek < 0 || indexWeek >= weekSchedule.Length)
            {
                return 0;
            }
            return weekSchedule[indexWeek];
        }

        private static Int32 SecondsFromScheduleSeq(Period period, Int32[] timeTable, int dayOrdinal, uint dayFromOrd, uint dayEndsOrd)
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

        private static int DayOfWeekFromOrdinal(int dayOrdinal, int beginDayCwd)
        {
            // dayOrdinal 1..31
            // beginDayCwd 1..7
            // dayOfWeek 1..7

            int dayOfWeek = (((dayOrdinal - 1) + (beginDayCwd - 1)) % 7) + 1;

            return dayOfWeek;
        }

        public static Int32[] TimesheetSchedule(Period period, Int32[] monthSchedule, uint dayOrdFrom, uint dayOrdEnds)
        {
            Int32[] timeSheet = monthSchedule.Select((x, i) => (HoursFromCalendar(dayOrdFrom, dayOrdEnds, i, x))).ToArray();

            return timeSheet;
        }

        public static Int32[] TimesheetAbsence(Period period, Int32[] absenceSchedule, uint dayOrdFrom, uint dayOrdEnds)
        {
            int daysInMonth = period.DaysInMonth();

            Int32[] monthSchedule = Enumerable.Range(1, daysInMonth).
                Select((x) => (SecondsFromScheduleSeq(period, absenceSchedule, x, dayOrdFrom, dayOrdEnds))).ToArray();

            return monthSchedule;
        }

        private static int HoursFromCalendar(uint dayOrdFrom, uint dayOrdEnds, int dayIndex, Int32 workSeconds)
        {
            int dayOrdinal = dayIndex + 1;

            int workingDay = workSeconds;

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

        public static Int32 TotalTimesheetHours(Int32[] monthTimesheet)
        {
            if (monthTimesheet == null)
            {
                return 0;
            }
            Int32 timesheetHours = monthTimesheet.Aggregate(0, (agr, dh) => (agr + dh));

            return timesheetHours;
        }

    }
}
