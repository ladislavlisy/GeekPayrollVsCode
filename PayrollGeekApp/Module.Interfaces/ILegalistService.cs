using System;

namespace ElementsLib.Module.Interfaces
{
    using Items;
    using Legalist;

    public interface ILegalistService
    {
        IBundleVersionCollection Profile();
        IPeriodProfile GetPeriodProfile(Period period);
    }
}
