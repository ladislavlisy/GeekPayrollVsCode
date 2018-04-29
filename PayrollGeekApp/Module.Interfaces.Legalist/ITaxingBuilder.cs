namespace ElementsLib.Legalist
{
    using Module.Interfaces.Legalist;
    using Module.Items;
    public interface ITaxingBuilder
    {
        ITaxingProfile BuildPeriodProfile(Period period);
    }
}