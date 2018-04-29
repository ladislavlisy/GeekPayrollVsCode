using System;

namespace ElementsLib.Module.Interfaces.Matrixus
{
    using ConfigRole = UInt16;
    using ConfigName = String;
    using ConfigStub = Elements.IArticleSource;

    using Elements;
    public interface IArticleConfigMaster : ICloneable
    {
        ConfigRole Role();
        ConfigName Name();

        ConfigRole[] Path();

        ConfigStub Stub();
        void SetSymbolRole(ConfigRole _role, ConfigName _name, ConfigStub _stub, params ConfigRole[] _path);
    }
}
