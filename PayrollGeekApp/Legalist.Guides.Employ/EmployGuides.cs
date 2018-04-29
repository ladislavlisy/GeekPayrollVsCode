using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Employ
{
    using TSeconds = Int32;
    using THours = Int32;
    using TDays = Int16;

    using Module.Interfaces.Legalist;
    using Module.Items;
    using Operations;

    public class EmployGuides : IEmployGuides
    {
        protected Period InternalPeriod { get; set; }

        protected readonly TDays __WeeklyWorkingDays;

        protected readonly THours __DailyWorkingHours;

        public EmployGuides(Period period, TDays weeklyWorkingDays, THours dailyWorkingHours)
        {
            InternalPeriod = period;

            __WeeklyWorkingDays = weeklyWorkingDays;

            __DailyWorkingHours = dailyWorkingHours;
        }

        public TDays WeeklyWorkingDays()
        {
            return __WeeklyWorkingDays;
        }
        public THours DailyWorkingHours()
        {
            return __DailyWorkingHours;
        }
        public TSeconds WeeklyWorkingSeconds()
        {
            return OperationsEmploy.WorkingSecondsWeekly(__WeeklyWorkingDays, __DailyWorkingHours);
        }
        public TSeconds DailyWorkingSeconds()
        {
            return OperationsEmploy.WorkingSecondsDaily(__DailyWorkingHours);
        }
    }
}
