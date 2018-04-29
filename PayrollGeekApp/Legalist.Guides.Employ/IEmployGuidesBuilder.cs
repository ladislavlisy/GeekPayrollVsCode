using System;

namespace ElementsLib.Legalist.Guides.Employ
{
    using BundleVersion = UInt16;
    using TSeconds = Int32;
    using THours = Int32;
    using TDays = Int16;

    using Module.Interfaces.Legalist;
    using Module.Items;

    public interface IEmployGuidesBuilder
    {
        BundleVersion BuilderVersion();
        IEmployGuides BuildPeriodGuides(Period period);
        TDays WeeklyWorkingDays(Period period);
        THours DailyWorkingHours(Period period);
    }
}