using ElementsLib.Module.Interfaces.Legalist;
using ElementsLib.Module.Items;

namespace ElementsLib.Legalist.Profiles.Employ
{
    public interface IEmployProfilePrototype
    {
        IEmployProfile CreatePeriodProfile(Period period, IEmployGuides guides);
    }
}