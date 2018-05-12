using System;

namespace ElementsLib.Module.Interfaces.Matrixus
{
    using ConfigCode = UInt16;
    using ConfigGang = UInt16;
    using ConfigRole = UInt16;
    using ConfigType = UInt16;
    using ConfigBind = UInt16;
    public interface IArticleBaseFeatures
    {
        ConfigCode Code();
        ConfigGang Gang();
        ConfigRole Role();
        ConfigType Type();
        ConfigBind Bind();

        bool IsTaxingIncome();
        bool IsTaxingPartner();
        bool IsTaxingExclude();
        bool IsHealthIncome();
        bool IsHealthExclude();
        bool IsSocialIncome();
        bool IsSocialExclude();
    }
}
