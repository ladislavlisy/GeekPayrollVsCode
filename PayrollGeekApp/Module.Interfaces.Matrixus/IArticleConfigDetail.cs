using System;

namespace ElementsLib.Module.Interfaces.Matrixus
{
    using ConfigCode = UInt16;
    using ConfigRole = UInt16;
    using ConfigName = String;
    using ConfigType = UInt16;
    using ConfigBind = UInt16;
    using ConfigStub = Module.Interfaces.Elements.IArticleSource;

    public interface IArticleConfigDetail : ICloneable
    {
        ConfigCode Code();
        ConfigName Name();
        ConfigRole Role();
        ConfigType Type();
        ConfigBind Bind();
        ConfigCode[] Path();
        ConfigStub Stub();
        void SetSymbolCode(ConfigCode _code, ConfigName _name, ConfigType _type, ConfigBind _bind, params ConfigCode[] _path);
        void SetSymbolRole(ConfigRole _role, ConfigStub _stub);
    }
}
