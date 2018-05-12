using System;

namespace ElementsLib.Module.Interfaces.Matrixus
{
    using ConfigCode = UInt16;
    using ConfigBase = Module.Interfaces.Matrixus.IArticleConfigFeatures;
    using ConfigGang = UInt16;
    using ConfigRole = UInt16;
    using ConfigType = UInt16;
    using ConfigBind = UInt16;
    using ConfigName = String;
    using ConfigStub = Elements.IArticleSource;

    using Elements;
    using ElementsLib.Legalist.Constants;

    public interface IArticleConfigMaster : ICloneable
    {
        ConfigRole Role();
        ConfigName Name();

        ConfigRole[] Path();

        ConfigStub CloneMasterStub(ConfigCode _code, ConfigRole _role, ConfigGang _gang, 
            ConfigType _type, ConfigBind _bind,
            TaxingBehaviour _taxing, HealthBehaviour _health, SocialBehaviour _social);
        void SetSymbolRole(ConfigRole _role, ConfigName _name, ConfigStub _stub, params ConfigRole[] _path);
    }
}
