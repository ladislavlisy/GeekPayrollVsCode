using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Matrixus.Config
{
    using ConfigRole = UInt16;
    using ConfigName = String;
    using ConfigStub = Module.Interfaces.Elements.IArticleSource;

    using Module.Interfaces.Matrixus;
    using Module.Interfaces.Elements;
    using Module.Libs;

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
        public ConfigStub Stub()
        {
            return InternalStub;
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
