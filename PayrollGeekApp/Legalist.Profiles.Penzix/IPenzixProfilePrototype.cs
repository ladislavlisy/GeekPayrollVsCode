using ElementsLib.Module.Interfaces.Legalist;
using ElementsLib.Module.Items;

namespace ElementsLib.Legalist.Profiles.Penzix
{
    public interface IPenzixProfilePrototype
    {
        IPenzixProfile CreatePeriodProfile(Period period, IPenzixGuides guides);
    }
}