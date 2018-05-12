using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Matrixus.Config
{
    using ConfigCode = UInt16;
    using ConfigBase = Module.Interfaces.Matrixus.IArticleConfigFeatures;
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

    public class ArticleConfigMaster : IArticleConfigMaster
    {
        protected ConfigRole InternalRole { get; set; }
        protected ConfigName InternalName { get; set; }
        protected IList<ConfigRole> InternalPath { get; set; }
        protected ConfigStub InternalStub { get; set; }

        public ArticleConfigMaster(ConfigRole _role, ConfigName _name, ConfigStub _stub, params ConfigRole[] _path)
        {
            InternalRole = _role;

            InternalName = _name;

            InternalPath = _path.ToList();

            InternalStub = CloneUtils<ConfigStub>.CloneOrNull(_stub);
        }

        public ConfigRole Role()
        {
            return InternalRole;
        }
        public ConfigName Name()
        {
            return InternalName;
        }
        public ConfigRole[] Path()
        {
            return InternalPath.ToArray();
        }
        public ConfigStub CloneMasterStub(ConfigCode _code, ConfigRole _role, ConfigGang _gang, 
            ConfigType _type, ConfigBind _bind, 
            TaxingBehaviour _taxing, HealthBehaviour _health, SocialBehaviour _social)
        {
            ConfigStub returnStub = CloneUtils<ConfigStub>.CloneOrNull(InternalStub);

            returnStub.SetSourceConfig(_code, _role, _gang, _type, _bind, _taxing, _health, _social);

            return returnStub;
        }

        public void SetSymbolRole(ConfigRole _role, ConfigName _name, ConfigStub _stub, params ConfigRole[] _path)
        {
            InternalRole = _role;

            InternalName = _name;

            InternalPath = _path.ToList();

            InternalStub = CloneUtils<ConfigStub>.CloneOrNull(_stub);
        }
        public virtual object Clone()
        {
            ArticleConfigMaster cloneMaster = (ArticleConfigMaster)this.MemberwiseClone();
            cloneMaster.InternalRole = this.InternalRole;
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
