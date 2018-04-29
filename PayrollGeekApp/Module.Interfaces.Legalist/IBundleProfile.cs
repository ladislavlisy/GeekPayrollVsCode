using ElementsLib.Module.Items;

namespace ElementsLib.Module.Interfaces.Legalist
{
    public interface IBundleProfile
    {
        IEmployProfile BuildEmployProfile(Period period);
        IHealthProfile BuildHealthProfile(Period period);
        ISocialProfile BuildSocialProfile(Period period);
        ITaxingProfile BuildTaxingProfile(Period period);
        IPenzixProfile BuildPenzixProfile(Period period);
    }
}