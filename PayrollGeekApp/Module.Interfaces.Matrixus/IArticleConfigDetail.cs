using System;

namespace ElementsLib.Module.Interfaces.Matrixus
{
    using ConfigCode = UInt16;
    using ConfigGang = UInt16;
    using ConfigRole = UInt16;
    using ConfigName = String;
    using ConfigType = UInt16;
    using ConfigBind = UInt16;
    using ConfigStub = Module.Interfaces.Elements.IArticleSource;

    public interface IArticleConfigDetail : IArticleConfigFeatures, ICloneable
    {
        ConfigName Name();
        ConfigCode[] Path();
        ConfigStub DetailStub();
        void SetSymbolRole(ConfigRole _role, ConfigStub _stub);
    }
}
