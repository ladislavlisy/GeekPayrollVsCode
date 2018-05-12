using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Interfaces.Matrixus
{
    using ConfigCode = UInt16;
    using ConfigGang = UInt16;
    using ConfigRole = UInt16;
    using ConfigType = UInt16;
    using ConfigBind = UInt16;
    using ElementsLib.Legalist.Constants;

    public interface IArticleConfigFeatures : IArticleBaseFeatures, ICloneable
    {
        void SetSymbolData(ConfigCode _code, ConfigRole _role, ConfigGang _gang, 
            ConfigType _type, ConfigBind _bind,
            TaxingBehaviour _taxing, HealthBehaviour _health, SocialBehaviour _social);
        void SetSymbolCode(ConfigCode _code);
        void SetSymbolRole(ConfigRole _role);
    }
}
