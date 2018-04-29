using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElementsLib.Module.Interfaces.Legalist;
using ElementsLib.Module.Items;

namespace ElementsLib.Legalist.Guides.Employ
{
    using BundleVersion = UInt16;
    using TSeconds = Int32;
    using THours = Int32;
    using TDays = Int16;

    using Operations;

    public abstract class EmployGuidesBuilder : GeneralGuides, IEmployGuidesBuilder
    {
        protected readonly TDays __WeeklyWorkingDays;

        protected readonly THours __DailyWorkingHours;

        protected EmployGuidesBuilder(BundleVersion version,
            TDays weeklyWorkingDays, THours dailyWorkingHours) : base(version)
		{
            __WeeklyWorkingDays = weeklyWorkingDays;

            __DailyWorkingHours = dailyWorkingHours;
        }
        public abstract TDays WeeklyWorkingDays(Period period);

        public abstract THours DailyWorkingHours(Period period);

        public BundleVersion BuilderVersion()
        {
            return InternalVersion;
        }
        public IEmployGuides BuildPeriodGuides(Period period)
        {
            return new EmployGuides(period, 
                WeeklyWorkingDays(period),
                DailyWorkingHours(period));
        }
        public virtual object Clone()
        {
            EmployGuidesBuilder other = (EmployGuidesBuilder)this.MemberwiseClone();
            return other;
        }
    }
}
