using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Versions.Employ
{
    using TSeconds = Int32;
    using THours = Int32;
    using TDays = Int16;
    using TAnmount = Decimal;
    using TDay = Byte;
    using TDate = DateTime;

    using Module.Interfaces.Legalist;
    using Module.Items;
    using Operations;
    using Rounding;

    public class EmployGuidingProfile : IEmployProfile
    {
        protected Period InternalPeriod { get; set; }
        protected IEmployGuides InternalGuides { get; set; }

        public EmployGuidingProfile(Period period, IEmployGuides guides)
        {
            this.InternalPeriod = period;

            this.InternalGuides = guides;
        }

        public IEmployGuides Guides()
        {
            return InternalGuides;
        }
        public TDay DateFromInPeriod(Period period, TDate? dateFrom)
        {
            return OperationsPeriod.DateFromInPeriod(period, dateFrom);
        }
        public TDay DateStopInPeriod(Period period, TDate? dateStop)
        {
            return OperationsPeriod.DateEndsInPeriod(period, dateStop);
        }
        public TDay DayEndsOrdinal(Period period, TDate? dateEnds)
        {
            return OperationsPeriod.DateEndsInPeriod(period, dateEnds);
        }

        public TSeconds[] TimesheetWeekSchedule(Period period, TSeconds secondsWeekly, TDays workdaysWeekly)
        {
            return OperationsPeriod.WeekSchedule(period, secondsWeekly, workdaysWeekly);
        }

        public TSeconds[] TimesheetFullSchedule(Period period, TSeconds[] weekSchedule)
        {
            return OperationsPeriod.MonthSchedule(period, weekSchedule);
        }

        public TSeconds[] TimesheetWorkSchedule(Period period, TSeconds[] monthSchedule, TDay dayFrom, TDay dayEnds)
        {
            return OperationsPeriod.TimesheetSchedule(period, monthSchedule,dayFrom, dayEnds);
        }

        public TSeconds[] TimesheetWorkAbsences(Period period, TSeconds[] absenceHours, TDay dayFrom, TDay dayEnds)
        {
            return OperationsPeriod.TimesheetAbsence(period, absenceHours, dayFrom, dayEnds);
        }

        public TSeconds TotalHoursForSalary(Period period, TSeconds fulltimeHour, TSeconds workingHours, TSeconds absenceHours)
        {
            return (TSeconds)0;
        }

        public TAnmount SalaryAmountScheduleFull(Period period, TAnmount amountMonthly)
        {
            return TAnmount.Zero;
        }

        public TAnmount SalaryAmountScheduleWork(Period period, TAnmount amountMonthly,
            TSeconds fulltimeHour, TSeconds workingsHours)
        {
            decimal coeffSalary = 1.0m;

            decimal salaryValue = RoundingPay.MonthlyAmountWithWorkingHours(amountMonthly, coeffSalary, fulltimeHour, workingsHours);

            return salaryValue;
        }
    }
}
