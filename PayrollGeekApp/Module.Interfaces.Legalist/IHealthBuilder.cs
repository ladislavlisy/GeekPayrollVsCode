namespace ElementsLib.Legalist
{
    using Module.Interfaces.Legalist;
    using Module.Items;
    public interface IHealthBuilder
    {
        IHealthProfile BuildPeriodProfile(Period period);
    }
}