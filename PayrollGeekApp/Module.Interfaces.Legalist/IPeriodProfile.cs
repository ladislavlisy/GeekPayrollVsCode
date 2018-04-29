namespace ElementsLib.Module.Interfaces.Legalist
{
    public interface IPeriodProfile : IPeriodSelect
    {
        IHealthProfile Health();
        ISocialProfile Social();
        ITaxingProfile Taxing();
        IPenzixProfile Penzix();
        IEmployProfile Employ();
    }
}