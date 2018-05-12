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

    using ElementsLib.Module.Interfaces.Matrixus;
    using Legalist.Constants;

    public class ArticleConfigFeatures : IArticleConfigFeatures
    {
        protected ConfigCode InternalCode { get; set; }
        protected ConfigGang InternalGang { get; set; }
        protected ConfigRole InternalRole { get; set; }
        protected ConfigType InternalType { get; set; }
        protected ConfigBind InternalBind { get; set; }
        protected TaxingBehaviour InternalTaxing { get; set; }
        protected HealthBehaviour InternalHealth { get; set; }
        protected SocialBehaviour InternalSocial { get; set; }

        public ArticleConfigFeatures()
        {
            InternalCode = 0;

            InternalGang = 0;

            InternalType = 0;

            InternalBind = 0;
            InternalTaxing = TaxingBehaviour.TAXING_NOTHING;
            InternalHealth = HealthBehaviour.HEALTH_NOTHING;
            InternalSocial = SocialBehaviour.SOCIAL_NOTHING;
        }
        public ArticleConfigFeatures(ConfigCode _code, ConfigGang _gang, 
            ConfigType _type, ConfigBind _bind, 
            TaxingBehaviour _taxing, HealthBehaviour _health, SocialBehaviour _social)
        {
            InternalCode = _code;

            InternalGang = _gang;

            InternalType = _type;

            InternalBind = _bind;

            InternalTaxing = _taxing;
            InternalHealth = _health;
            InternalSocial = _social;
        }
        public ConfigCode Code()
        {
            return InternalCode;
        }
        public ConfigRole Role()
        {
            return InternalRole;
        }
        public ConfigGang Gang()
        {
            return InternalGang;
        }
        public ConfigType Type()
        {
            return InternalType;
        }
        public ConfigBind Bind()
        {
            return InternalBind;
        }

        public bool IsTaxingIncome()
        {
            return (InternalTaxing == TaxingBehaviour.TAXING_ADVANCE || InternalTaxing == TaxingBehaviour.TAXING_WITHHOLD);
        }
        public bool IsTaxingPartner()
        {
            return (InternalTaxing == TaxingBehaviour.TAXING_PARTNER);
        }
        public bool IsTaxingExclude()
        {
            return (InternalTaxing == TaxingBehaviour.TAXING_EXCLUDE);
        }
        public bool IsHealthIncome()
        {
            return (InternalHealth == HealthBehaviour.HEALTH_INCOMES);
        }
        public bool IsHealthExclude()
        {
            return (InternalHealth == HealthBehaviour.HEALTH_EXCLUDE);
        }
        public bool IsSocialIncome()
        {
            return (InternalSocial == SocialBehaviour.SOCIAL_INCOMES);
        }
        public bool IsSocialExclude()
        {
            return (InternalSocial == SocialBehaviour.SOCIAL_EXCLUDE);
        }


        public void SetSymbolData(ConfigCode _code, ConfigRole _role, ConfigGang _gang, 
            ConfigType _type, ConfigBind _bind, 
            TaxingBehaviour _taxing, HealthBehaviour _health, SocialBehaviour _social)
        {
            InternalCode = _code;

            InternalRole = _role;

            InternalGang = _gang;

            InternalType = _type;

            InternalTaxing = _taxing;
            InternalHealth = _health;
            InternalSocial = _social;
        }
        public void SetSymbolCode(ConfigCode _code)
        {
            InternalCode = _code;
        }
        public void SetSymbolRole(ConfigRole _role)
        {
            InternalRole = _role;
        }
        public virtual object Clone()
        {
            ArticleConfigFeatures cloneMaster = (ArticleConfigFeatures)this.MemberwiseClone();
            cloneMaster.InternalCode = this.InternalCode;
            cloneMaster.InternalGang = this.InternalGang;
            cloneMaster.InternalRole = this.InternalRole;
            cloneMaster.InternalType = this.InternalType;
            cloneMaster.InternalBind = this.InternalBind;
            cloneMaster.InternalTaxing = this.InternalTaxing;
            cloneMaster.InternalHealth = this.InternalHealth;
            cloneMaster.InternalSocial = this.InternalSocial;
            return cloneMaster;
        }
    }
}
