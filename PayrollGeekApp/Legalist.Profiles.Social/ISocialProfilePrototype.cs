using ElementsLib.Module.Interfaces.Legalist;
using ElementsLib.Module.Items;

namespace ElementsLib.Legalist.Profiles.Social
{
    public interface ISocialProfilePrototype
    {
        ISocialProfile CreatePeriodProfile(Period period, ISocialGuides guides);
    }
}