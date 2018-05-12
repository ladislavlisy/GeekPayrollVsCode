using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Service.Permadom
{
    using Module.Codes;

    using ConfigCodeEnum = Module.Codes.ArticleCodeCz;
    using ConfigCodeData = UInt16;
    using ConfigCodeName = String;
    using ConfigRoleEnum = Module.Codes.ArticleRoleCz;
    using ConfigRoleData = UInt16;
    using ConfigTypeEnum = Module.Codes.ArticleType;
    using ConfigTypeData = UInt16;
    using ConfigBindEnum = Module.Codes.ArticleBind;
    using ConfigBindData = UInt16;
    using ConfigGangEnum = Module.Codes.ArticleGang;
    using ConfigGangData = UInt16;

    using ConfigItemData = Module.Interfaces.Permadom.ArticleCodeConfigData;
    using ElementsLib.Legalist.Constants;

    public class ArticleCodeConfigBuilder
    {
        public static ConfigItemData CreateConfigItem(ConfigCodeEnum codeEnum, ConfigRoleEnum roleEnum, 
            ConfigGangEnum gangEnum, ConfigTypeEnum typeEnum, ConfigBindEnum bindEnum,
            TaxingBehaviour typeTaxing, HealthBehaviour typeHealth, SocialBehaviour typeSocial,
            params ConfigCodeEnum[] pathEnum)
        {
            ConfigCodeData codeData = (ConfigCodeData)codeEnum;
            ConfigRoleData roleData = (ConfigRoleData)roleEnum;
            ConfigGangData gangData = (ConfigGangData)gangEnum;
            ConfigTypeData typeData = (ConfigTypeData)typeEnum;
            ConfigBindData bindData = (ConfigBindData)bindEnum;
            ConfigCodeData[] codePath = pathEnum.Select((c) => ((ConfigCodeData)c)).ToArray();
            ConfigCodeName codeName = codeEnum.GetSymbol();
            TaxingBehaviour roleTaxing = typeTaxing;
            HealthBehaviour roleHealth = typeHealth;
            SocialBehaviour roleSocial = typeSocial;

            return new ConfigItemData(codeData, roleData, gangData, typeData, bindData, 
                roleTaxing, roleHealth, roleSocial, 
                codeName, codePath);
        }
        #region CONFIG_DATA
        public static IEnumerable<ConfigItemData> GetConfigDataList()
        {
            IList<ConfigItemData> configList = new List<ConfigItemData>()
            {
                CreateConfigItem(ConfigCodeEnum.FACT_UNKNOWN, ConfigRoleEnum.ARTICLE_UNKNOWN, 
                    ConfigGangEnum.EARNINGS_GANG,
                    ConfigTypeEnum.NO_HEAD_PART_TYPE, ConfigBindEnum.ARTICLE_OPT, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING),
#region CONTRACT_ARTICLES
                CreateConfigItem(ConfigCodeEnum.FACT_CONTRACT_TERM, ConfigRoleEnum.ARTICLE_CONTRACT_TERM, 
                    ConfigGangEnum.EARNINGS_GANG, 
                    ConfigTypeEnum.NO_HEAD_PART_TYPE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING),
                CreateConfigItem(ConfigCodeEnum.FACT_CONTRACT_TIMESHEET, ConfigRoleEnum.ARTICLE_CONTRACT_TIMESHEET, 
                    ConfigGangEnum.EARNINGS_GANG, 
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_CONTRACT_TERM, ConfigCodeEnum.FACT_POSITION_TIMESHEET),
                CreateConfigItem(ConfigCodeEnum.FACT_CONTRACT_ATTEND_ITEM, ConfigRoleEnum.ARTICLE_CONTRACT_ATTEND_ITEM, 
                    ConfigGangEnum.EARNINGS_GANG, 
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_OPT, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_CONTRACT_TIMESHEET),
                CreateConfigItem(ConfigCodeEnum.FACT_CONTRACT_ABSENCE, ConfigRoleEnum.ARTICLE_CONTRACT_ABSENCE, 
                    ConfigGangEnum.EARNINGS_GANG, 
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_CONTRACT_TIMESHEET, ConfigCodeEnum.FACT_POSITION_ABSENCE),
                CreateConfigItem(ConfigCodeEnum.FACT_CONTRACT_WORKING, ConfigRoleEnum.ARTICLE_CONTRACT_WORKING, 
                    ConfigGangEnum.EARNINGS_GANG, 
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_CONTRACT_TIMESHEET, ConfigCodeEnum.FACT_POSITION_WORKING),
#endregion
#region POSITION_ARTICLES
                CreateConfigItem(ConfigCodeEnum.FACT_POSITION_TERM, ConfigRoleEnum.ARTICLE_POSITION_TERM, 
                    ConfigGangEnum.EARNINGS_GANG, 
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_CONTRACT_TERM),
                CreateConfigItem(ConfigCodeEnum.FACT_POSITION_SCHEDULE, ConfigRoleEnum.ARTICLE_POSITION_SCHEDULE, 
                    ConfigGangEnum.EARNINGS_GANG, 
                    ConfigTypeEnum.PART_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_POSITION_TERM),
                CreateConfigItem(ConfigCodeEnum.FACT_POSITION_TIMESHEET, ConfigRoleEnum.ARTICLE_POSITION_TIMESHEET, 
                    ConfigGangEnum.EARNINGS_GANG, 
                    ConfigTypeEnum.PART_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_POSITION_SCHEDULE),
                CreateConfigItem(ConfigCodeEnum.FACT_POSITION_ABSENCE, ConfigRoleEnum.ARTICLE_POSITION_ABSENCE, 
                    ConfigGangEnum.EARNINGS_GANG, 
                    ConfigTypeEnum.PART_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_POSITION_TIMESHEET, ConfigCodeEnum.FACT_CONTRACT_ATTEND_ITEM),
                CreateConfigItem(ConfigCodeEnum.FACT_POSITION_WORKING, ConfigRoleEnum.ARTICLE_POSITION_WORKING, 
                    ConfigGangEnum.EARNINGS_GANG, 
                    ConfigTypeEnum.PART_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_POSITION_TIMESHEET, ConfigCodeEnum.FACT_POSITION_ABSENCE),
#endregion
#region SALARY_ARTICLES
                CreateConfigItem(ConfigCodeEnum.FACT_POSITION_MONTHLY_AMOUNT, ConfigRoleEnum.ARTICLE_POSITION_MONTHLY_AMOUNT, 
                    ConfigGangEnum.EARNINGS_GANG, 
                    ConfigTypeEnum.PART_CODE_ARTICLE, ConfigBindEnum.ARTICLE_OPT, 
                    TaxingBehaviour.TAXING_ADVANCE, HealthBehaviour.HEALTH_INCOMES, SocialBehaviour.SOCIAL_INCOMES,
                    ConfigCodeEnum.FACT_POSITION_TIMESHEET, ConfigCodeEnum.FACT_POSITION_WORKING),
#endregion
#region TAXING_AND_INSURACE
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_DECLARATION, ConfigRoleEnum.ARTICLE_TAX_DECLARATION, 
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING),
                CreateConfigItem(ConfigCodeEnum.FACT_INS_DECLARATION_HEALTH, ConfigRoleEnum.ARTICLE_INS_DECLARATION_HEALTH, 
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING),
                CreateConfigItem(ConfigCodeEnum.FACT_INS_DECLARATION_SOCIAL, ConfigRoleEnum.ARTICLE_INS_DECLARATION_SOCIAL, 
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING),
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_INCOMES_GENERAL, ConfigRoleEnum.ARTICLE_TAX_INCOMES_GENERAL, 
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_TAX_DECLARATION),
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_INCOMES_HEALTH, ConfigRoleEnum.ARTICLE_TAX_INCOMES_HEALTH, 
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_TAX_DECLARATION, ConfigCodeEnum.FACT_INS_DECLARATION_HEALTH, ConfigCodeEnum.FACT_INS_INCOMES_HEALTH),
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_INCOMES_SOCIAL, ConfigRoleEnum.ARTICLE_TAX_INCOMES_SOCIAL, 
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_TAX_DECLARATION, ConfigCodeEnum.FACT_INS_DECLARATION_SOCIAL, ConfigCodeEnum.FACT_INS_INCOMES_SOCIAL),
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_INCOMES_ADVANCE, ConfigRoleEnum.ARTICLE_TAX_INCOMES_ADVANCE,
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.NO_HEAD_PART_TYPE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_TAX_INCOMES_GENERAL, ConfigCodeEnum.FACT_TAX_INCOMES_HEALTH, ConfigCodeEnum.FACT_TAX_INCOMES_SOCIAL), 
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_GENERAL, ConfigRoleEnum.ARTICLE_TAX_INCOMES_WITHHOLD_GENERAL,
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.NO_HEAD_PART_TYPE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING, 
                    ConfigCodeEnum.FACT_TAX_INCOMES_ADVANCE, ConfigCodeEnum.FACT_TAX_INCOMES_GENERAL, ConfigCodeEnum.FACT_TAX_INCOMES_HEALTH, ConfigCodeEnum.FACT_TAX_INCOMES_SOCIAL), 
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_LOLEVEL, ConfigRoleEnum.ARTICLE_TAX_INCOMES_WITHHOLD_LOLEVEL,
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.NO_HEAD_PART_TYPE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING, 
                    ConfigCodeEnum.FACT_TAX_INCOMES_ADVANCE, ConfigCodeEnum.FACT_TAX_INCOMES_GENERAL, ConfigCodeEnum.FACT_TAX_INCOMES_HEALTH, ConfigCodeEnum.FACT_TAX_INCOMES_SOCIAL, 
                    ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_GENERAL), 
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_TASKAGR, ConfigRoleEnum.ARTICLE_TAX_INCOMES_WITHHOLD_TASKAGR,
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.NO_HEAD_PART_TYPE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING, 
                    ConfigCodeEnum.FACT_TAX_INCOMES_ADVANCE, ConfigCodeEnum.FACT_TAX_INCOMES_GENERAL, ConfigCodeEnum.FACT_TAX_INCOMES_HEALTH, ConfigCodeEnum.FACT_TAX_INCOMES_SOCIAL, 
                    ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_GENERAL, ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_LOLEVEL), 
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_PARTNER, ConfigRoleEnum.ARTICLE_TAX_INCOMES_WITHHOLD_PARTNER,
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.NO_HEAD_PART_TYPE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING, 
                    ConfigCodeEnum.FACT_TAX_INCOMES_ADVANCE, ConfigCodeEnum.FACT_TAX_INCOMES_GENERAL, ConfigCodeEnum.FACT_TAX_INCOMES_HEALTH, ConfigCodeEnum.FACT_TAX_INCOMES_SOCIAL, 
                    ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_GENERAL, ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_LOLEVEL, ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_TASKAGR), 
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_BASE_ADVANCE, ConfigRoleEnum.ARTICLE_TAX_BASE_ADVANCE,
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.NO_HEAD_PART_TYPE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_TAX_INCOMES_ADVANCE, 
                    ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_GENERAL, 
                    ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_LOLEVEL, 
                    ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_TASKAGR, 
                    ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_PARTNER), 
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_BASE_ADVANCE_HEALTH, ConfigRoleEnum.ARTICLE_TAX_BASE_ADVANCE_HEALTH,
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.NO_HEAD_PART_TYPE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_TAX_INCOMES_HEALTH), 
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_BASE_ADVANCE_SOCIAL, ConfigRoleEnum.ARTICLE_TAX_BASE_ADVANCE_SOCIAL,
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.NO_HEAD_PART_TYPE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_TAX_INCOMES_SOCIAL), 
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_BASE_ADVANCE_PARTIAL, ConfigRoleEnum.ARTICLE_TAX_BASE_ADVANCE_PARTIAL,
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.NO_HEAD_PART_TYPE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_TAX_BASE_ADVANCE, 
                    ConfigCodeEnum.FACT_TAX_BASE_ADVANCE_HEALTH, ConfigCodeEnum.FACT_TAX_BASE_ADVANCE_SOCIAL), 
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_BASE_ADVANCE_ORDINARY, ConfigRoleEnum.ARTICLE_TAX_BASE_ADVANCE_ORDINARY,
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.NO_HEAD_PART_TYPE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_TAX_BASE_ADVANCE_PARTIAL), 
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_BASE_ADVANCE_SOLIDARY, ConfigRoleEnum.ARTICLE_TAX_BASE_ADVANCE_SOLIDARY,
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.NO_HEAD_PART_TYPE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_TAX_INCOMES_ADVANCE), 
                CreateConfigItem(ConfigCodeEnum.FACT_TAX_BASE_WITHHOLD, ConfigRoleEnum.ARTICLE_TAX_BASE_WITHHOLD,
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.NO_HEAD_PART_TYPE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_TAX_INCOMES_ADVANCE, 
                    ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_GENERAL, 
                    ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_LOLEVEL, 
                    ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_TASKAGR, 
                    ConfigCodeEnum.FACT_TAX_INCOMES_WITHHOLD_PARTNER), 
                CreateConfigItem(ConfigCodeEnum.FACT_INS_INCOMES_HEALTH, ConfigRoleEnum.ARTICLE_INS_INCOMES_HEALTH, 
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_INS_DECLARATION_HEALTH),
                CreateConfigItem(ConfigCodeEnum.FACT_INS_INCOMES_SOCIAL, ConfigRoleEnum.ARTICLE_INS_INCOMES_SOCIAL, 
                    ConfigGangEnum.TRANSFER_GANG, 
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ, 
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_INS_DECLARATION_SOCIAL),
                CreateConfigItem(ConfigCodeEnum.FACT_INS_BASE_HEALTH_COMPOUND, ConfigRoleEnum.ARTICLE_INS_BASE_HEALTH_COMPOUND,
                    ConfigGangEnum.TRANSFER_GANG,
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ,
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_INS_INCOMES_HEALTH),
                CreateConfigItem(ConfigCodeEnum.FACT_INS_BASE_HEALTH_EMPLOYER, ConfigRoleEnum.ARTICLE_INS_BASE_HEALTH_EMPLOYER,
                    ConfigGangEnum.TRANSFER_GANG,
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ,
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_INS_INCOMES_HEALTH),
                CreateConfigItem(ConfigCodeEnum.FACT_INS_BASE_HEALTH_EMPLOYEE, ConfigRoleEnum.ARTICLE_INS_BASE_HEALTH_EMPLOYEE,
                    ConfigGangEnum.TRANSFER_GANG,
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ,
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_INS_INCOMES_HEALTH),
                CreateConfigItem(ConfigCodeEnum.FACT_INS_BASE_SOCIAL_COMPOUND, ConfigRoleEnum.ARTICLE_INS_BASE_SOCIAL_COMPOUND,
                    ConfigGangEnum.TRANSFER_GANG,
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ,
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_INS_INCOMES_SOCIAL),
                CreateConfigItem(ConfigCodeEnum.FACT_INS_HEALTH_FINAL, ConfigRoleEnum.ARTICLE_INS_HEALTH_FINAL,
                    ConfigGangEnum.TRANSFER_GANG,
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ,
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_INS_BASE_HEALTH_COMPOUND, ConfigCodeEnum.FACT_INS_BASE_HEALTH_EMPLOYER, ConfigCodeEnum.FACT_INS_BASE_HEALTH_EMPLOYEE),
                CreateConfigItem(ConfigCodeEnum.FACT_INS_SOCIAL_FINAL, ConfigRoleEnum.ARTICLE_INS_SOCIAL_FINAL,
                    ConfigGangEnum.TRANSFER_GANG,
                    ConfigTypeEnum.HEAD_CODE_ARTICLE, ConfigBindEnum.ARTICLE_REQ,
                    TaxingBehaviour.TAXING_NOTHING, HealthBehaviour.HEALTH_NOTHING, SocialBehaviour.SOCIAL_NOTHING,
                    ConfigCodeEnum.FACT_INS_BASE_SOCIAL_COMPOUND),

#endregion
            };
            return configList;
        }
        #endregion
    }
}
