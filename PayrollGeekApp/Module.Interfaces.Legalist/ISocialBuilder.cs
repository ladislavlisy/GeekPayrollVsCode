namespace ElementsLib.Legalist
{
    using Module.Interfaces.Legalist;
    using Module.Items;
    public interface ISocialBuilder
    {
        ISocialProfile BuildPeriodProfile(Period period);
    }
}