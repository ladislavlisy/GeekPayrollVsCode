using ElementsLib.Module.Interfaces.Legalist;
using ElementsLib.Module.Items;

namespace ElementsLib.Legalist.Profiles.Health
{
    public interface IHealthProfilePrototype
    {
        IHealthProfile CreatePeriodProfile(Period period, IHealthGuides guides);
    }
}