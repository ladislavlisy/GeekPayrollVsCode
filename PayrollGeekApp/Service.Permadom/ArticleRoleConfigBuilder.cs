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
            };
            return configList;
        }

    }
}
