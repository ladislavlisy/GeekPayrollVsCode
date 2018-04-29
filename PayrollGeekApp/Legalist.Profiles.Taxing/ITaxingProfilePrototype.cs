using ElementsLib.Module.Interfaces.Legalist;
using ElementsLib.Module.Items;

namespace ElementsLib.Legalist.Profiles.Taxing
{
    public interface ITaxingProfilePrototype
    {
        ITaxingProfile CreatePeriodProfile(Period period, ITaxingGuides guides);
    }
}