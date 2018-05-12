using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Interfaces.Permadom
{
    using ConfigCode = UInt16;
    using ConfigRole = UInt16;
    using ConfigGang = UInt16;
    using ConfigType = UInt16;
    using ConfigBind = UInt16;
    using SymbolName = String;
    using ElementsLib.Legalist.Constants;

    public class ArticleCodeConfigData
    {
        public ConfigCode Code { get; set; }
        public ConfigRole Role { get; set; }
        public ConfigGang Gang { get; set; }
        public ConfigType Type { get; set; }
        public ConfigBind Bind { get; set; }
        public SymbolName Name { get; set; }
        public ConfigCode[] Path { get; set; }
        public TaxingBehaviour TaxingType { get; set; }
        public HealthBehaviour HealthType { get; set; }
        public SocialBehaviour SocialType { get; set; }
        public ArticleCodeConfigData(ConfigCode _code, ConfigRole _role, 
            ConfigGang _gang, ConfigType _type, ConfigBind _bind,
            TaxingBehaviour taxingType, HealthBehaviour healthType, SocialBehaviour socialType,
            SymbolName _name, params ConfigCode[] _path)
        {
            Code = _code;
            Role = _role;
            Gang = _gang;
            Type = _type;
            Bind = _bind;
            Name = _name;
            Path = _path.ToArray();
            TaxingType = taxingType;
            HealthType = healthType;
            SocialType = socialType;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, \"{4}\", {5}", Code.ToString(), 
                Role.ToString(), Gang.ToString(), Type.ToString(), Bind.ToString(), Name, string.Join(", ", Path));
        }
    }
}
