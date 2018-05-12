using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Service.Permadom
{
    using ConfigRoleEnum = Module.Codes.ArticleRoleCz;
    using ConfigRoleData = UInt16;
    using ConfigRoleName = String;

    using ConfigItem = Module.Interfaces.Permadom.ArticleRoleConfigData;

    using Module.Interfaces.Permadom;
    using Module.Codes;


    public class ArticleRoleConfigBuilder
    {
        public static ConfigItem CreateConfigData(ConfigRoleEnum roleEnum, params ConfigRoleEnum[] pathEnum)
        {
            ConfigRoleData roleData = (ConfigRoleData)roleEnum;
            ConfigRoleData[] rolePath = pathEnum.Select((c) => ((ConfigRoleData)c)).ToArray();
            ConfigRoleName roleName = roleEnum.GetSymbol();

            return new ArticleRoleConfigData(roleData, roleName, rolePath);
        }
        #region CONFIG_DATA
        public static IEnumerable<ConfigItem> GetConfigDataList()
        {
            IList<ConfigItem> configList = new List<ConfigItem>()
            {
                CreateConfigData(ConfigRoleEnum.ARTICLE_UNKNOWN),
                CreateConfigData(ConfigRoleEnum.ARTICLE_CONTRACT_TERM),
                CreateConfigData(ConfigRoleEnum.ARTICLE_POSITION_TERM),
                CreateConfigData(ConfigRoleEnum.ARTICLE_POSITION_SCHEDULE),
                CreateConfigData(ConfigRoleEnum.ARTICLE_POSITION_TIMESHEET),
                CreateConfigData(ConfigRoleEnum.ARTICLE_POSITION_WORKING),
                CreateConfigData(ConfigRoleEnum.ARTICLE_POSITION_ABSENCE),
                CreateConfigData(ConfigRoleEnum.ARTICLE_CONTRACT_TIMESHEET),
                CreateConfigData(ConfigRoleEnum.ARTICLE_CONTRACT_WORKING),
                CreateConfigData(ConfigRoleEnum.ARTICLE_CONTRACT_ABSENCE),
                CreateConfigData(ConfigRoleEnum.ARTICLE_CONTRACT_ATTEND_ITEM),
                CreateConfigData(ConfigRoleEnum.ARTICLE_POSITION_MONTHLY_AMOUNT),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_DECLARATION),
                CreateConfigData(ConfigRoleEnum.ARTICLE_INS_DECLARATION_HEALTH),
                CreateConfigData(ConfigRoleEnum.ARTICLE_INS_DECLARATION_SOCIAL),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_INCOMES_GENERAL),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_INCOMES_HEALTH),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_INCOMES_SOCIAL),
                CreateConfigData(ConfigRoleEnum.ARTICLE_INS_INCOMES_HEALTH),
                CreateConfigData(ConfigRoleEnum.ARTICLE_INS_INCOMES_SOCIAL),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_INCOMES_ADVANCE),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_INCOMES_WITHHOLD_GENERAL),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_INCOMES_WITHHOLD_LOLEVEL),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_INCOMES_WITHHOLD_TASKAGR),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_INCOMES_WITHHOLD_PARTNER),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_BASE_ADVANCE),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_BASE_WITHHOLD),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_BASE_ADVANCE_HEALTH),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_BASE_ADVANCE_SOCIAL),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_BASE_ADVANCE_PARTIAL),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_BASE_ADVANCE_ORDINARY),
                CreateConfigData(ConfigRoleEnum.ARTICLE_TAX_BASE_ADVANCE_SOLIDARY),
                CreateConfigData(ConfigRoleEnum.ARTICLE_INS_BASE_HEALTH_COMPOUND),
                CreateConfigData(ConfigRoleEnum.ARTICLE_INS_BASE_HEALTH_EMPLOYER),
                CreateConfigData(ConfigRoleEnum.ARTICLE_INS_BASE_HEALTH_EMPLOYEE),
                CreateConfigData(ConfigRoleEnum.ARTICLE_INS_HEALTH_FINAL),
                CreateConfigData(ConfigRoleEnum.ARTICLE_INS_BASE_SOCIAL_COMPOUND),
                CreateConfigData(ConfigRoleEnum.ARTICLE_INS_SOCIAL_FINAL),
            };
            return configList;
        }
        #endregion
    }
}
