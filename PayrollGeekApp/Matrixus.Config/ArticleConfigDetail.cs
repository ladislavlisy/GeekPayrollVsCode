using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Matrixus.Config
{
    using ConfigCode = UInt16;
    using ConfigGang = UInt16;
    using ConfigRole = UInt16;
    using ConfigType = UInt16;
    using ConfigBind = UInt16;
    using ConfigName = String;
    using ConfigStub = Module.Interfaces.Elements.IArticleSource;

    using Module.Interfaces.Matrixus;
    using Module.Interfaces.Elements;
    using Module.Libs;
    using Legalist.Constants;

    public class ArticleConfigDetail : ArticleConfigFeatures, IArticleConfigDetail
    {
        protected ConfigName InternalName { get; set; }
        protected IList<ConfigCode> InternalPath { get; set; }
        protected ConfigStub InternalStub { get; set; }

        public ArticleConfigDetail(ConfigCode _code, ConfigName _name, ConfigGang _gang, 
            ConfigType _type, ConfigBind _bind, 
            TaxingBehaviour _taxing, HealthBehaviour _health, SocialBehaviour _social, params ConfigCode[] _path)
            : base(_code, _gang, _type, _bind, _taxing, _health, _social)
        {
            InternalName = _name;

            InternalPath = _path.ToList();
        }
        public ConfigName Name()
        {
            return InternalName;
        }
        public ConfigCode[] Path()
        {
            return InternalPath.ToArray();
        }
        public ConfigStub DetailStub()
        {
            return InternalStub;
        }

        public void SetSymbolRole(ConfigRole _role, ConfigStub _stub)
        {
            base.SetSymbolRole(_role);

            InternalStub = _stub;
        }
        public override object Clone()
        {
            ArticleConfigDetail cloneMaster = (ArticleConfigDetail)this.MemberwiseClone();
            cloneMaster.InternalCode = this.InternalCode;
            cloneMaster.InternalGang = this.InternalGang;
            cloneMaster.InternalRole = this.InternalRole;
            cloneMaster.InternalType = this.InternalType;
            cloneMaster.InternalBind = this.InternalBind;
            cloneMaster.InternalName = this.InternalName;
            cloneMaster.InternalPath = this.InternalPath.ToList();
            cloneMaster.InternalStub = CloneUtils<ConfigStub>.CloneOrNull(this.InternalStub);

            return cloneMaster;
        }
        public override string ToString()
        {
            return InternalName;
        }
    }
}
