
using System;

namespace ElementsLib.Module.Interfaces.Legalist
{
    using TSeconds = Int32;
    using THours = Int32;
    using TDays = Int16;
    using TAnmount = Decimal;
    using TDay = Byte;
    using TDate = DateTime;

    using Items;

    public interface IEmployProfile
    {
        IEmployGuides Guides();

        TDay DateFromInPeriod(Period period, TDate? dateFrom);
        TDay DateStopInPeriod(Period period, TDate? dateStop);

        TDay DayEndsOrdinal(Period period, TDate? dateEnds);

        TSeconds[] TimesheetWeekSchedule(Period period, TSeconds secondsWeekly, TDays workdaysWeekly);

        TSeconds[] TimesheetFullSchedule(Period period, TSeconds[] weekSchedule);

        TSeconds[] TimesheetWorkSchedule(Period period, TSeconds[] monthSchedule, TDay dayFrom, TDay dayEnds);

        TSeconds[] TimesheetWorkAbsences(Period period, TSeconds[] absenceHours, TDay dayFrom, TDay dayEnds);

        TSeconds TotalHoursForSalary(Period period, TSeconds fulltimeHour, TSeconds workingHours, TSeconds absenceHours);

        TAnmount SalaryAmountScheduleFull(Period period, TAnmount amountMonthly);

        TAnmount SalaryAmountScheduleWork(Period period, TAnmount amountMonthly, 
            TSeconds fulltimeHour, TSeconds workingHours);
    }
}