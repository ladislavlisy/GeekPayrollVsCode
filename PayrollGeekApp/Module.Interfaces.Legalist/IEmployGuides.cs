using System;

namespace ElementsLib.Module.Interfaces.Legalist
{
    using TSeconds = Int32;
    using THours = Int32;
    using TDays = Int16;

    public interface IEmployGuides
    {
        TDays WeeklyWorkingDays();
        THours DailyWorkingHours();
        TSeconds DailyWorkingSeconds();
        TSeconds WeeklyWorkingSeconds();
 
    }
}