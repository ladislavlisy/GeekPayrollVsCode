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

    using ConfigItemData = Module.Interfaces.Permadom.ArticleCodeConfigData;
    public class ArticleCodeConfigBuilder
    {
        public static ConfigItemData CreateConfigItem(ConfigCodeEnum codeEnum, ConfigRoleEnum roleEnum, ConfigTypeEnum typeEnum, ConfigBindEnum bindEnum, params ConfigCodeEnum[] pathEnum)
        {
            ConfigCodeData codeData = (ConfigCodeData)codeEnum;
            ConfigRoleData roleData = (ConfigRoleData)roleEnum;
            ConfigTypeData typeData = (ConfigTypeData)typeEnum;
            ConfigBindData bindData = (ConfigBindData)bindEnum;
            ConfigCodeData[] codePath = pathEnum.Select((c) => ((ConfigCodeData)c)).ToArray();
            ConfigCodeName codeName = codeEnum.GetSymbol();

            return new ConfigItemData(codeData, roleData, typeData, bindData, codeName, codePath);
        }
        public static IEnumerable<ConfigItemData> GetConfigDataList()
        {
            IList<ConfigItemData> configList = new List<ConfigItemData>()
            {
                CreateConfigItem(ConfigCodeEnum.FACT_UNKNOWN, ConfigRoleEnum.ARTICLE_UNKNOWN, ArticleType.NO_HEAD_PART_TYPE, ArticleBind.ARTICLE_OPT),
                CreateConfigItem(ConfigCodeEnum.FACT_CONTRACT_TERM, ConfigRoleEnum.ARTICLE_CONTRACT_TERM, ArticleType.NO_HEAD_PART_TYPE, ArticleBind.ARTICLE_REQ),
                CreateConfigItem(ConfigCodeEnum.FACT_CONTRACT_TIMESHEET, ConfigRoleEnum.ARTICLE_CONTRACT_TIMESHEET, ArticleType.HEAD_CODE_ARTICLE, ArticleBind.ARTICLE_REQ,
                    ConfigCodeEnum.FACT_CONTRACT_TERM, ConfigCodeEnum.FACT_POSITION_TIMESHEET),
                CreateConfigItem(ConfigCodeEnum.FACT_CONTRACT_WORKING, ConfigRoleEnum.ARTICLE_CONTRACT_WORKING, ArticleType.HEAD_CODE_ARTICLE, ArticleBind.ARTICLE_REQ,
                    ConfigCodeEnum.FACT_CONTRACT_TIMESHEET, ConfigCodeEnum.FACT_POSITION_WORKING),
                CreateConfigItem(ConfigCodeEnum.FACT_CONTRACT_ATTEND_ITEM, ConfigRoleEnum.ARTICLE_CONTRACT_ATTEND_ITEM, ArticleType.HEAD_CODE_ARTICLE, ArticleBind.ARTICLE_OPT,
                    ConfigCodeEnum.FACT_CONTRACT_TIMESHEET),
                CreateConfigItem(ConfigCodeEnum.FACT_CONTRACT_ABSENCE, ConfigRoleEnum.ARTICLE_CONTRACT_ABSENCE, ArticleType.HEAD_CODE_ARTICLE, ArticleBind.ARTICLE_REQ,
                    ConfigCodeEnum.FACT_CONTRACT_TIMESHEET, ConfigCodeEnum.FACT_POSITION_ABSENCE),
                CreateConfigItem(ConfigCodeEnum.FACT_POSITION_TERM, ConfigRoleEnum.ARTICLE_POSITION_TERM, ArticleType.HEAD_CODE_ARTICLE, ArticleBind.ARTICLE_REQ,
                    ConfigCodeEnum.FACT_CONTRACT_TERM),
                CreateConfigItem(ConfigCodeEnum.FACT_POSITION_SCHEDULE, ConfigRoleEnum.ARTICLE_POSITION_SCHEDULE, ArticleType.PART_CODE_ARTICLE, ArticleBind.ARTICLE_REQ,
                    ConfigCodeEnum.FACT_POSITION_TERM),
                CreateConfigItem(ConfigCodeEnum.FACT_POSITION_TIMESHEET, ConfigRoleEnum.ARTICLE_POSITION_TIMESHEET, ArticleType.PART_CODE_ARTICLE, ArticleBind.ARTICLE_REQ,
                    ConfigCodeEnum.FACT_POSITION_SCHEDULE),
                CreateConfigItem(ConfigCodeEnum.FACT_POSITION_WORKING, ConfigRoleEnum.ARTICLE_POSITION_WORKING, ArticleType.PART_CODE_ARTICLE, ArticleBind.ARTICLE_REQ,
                    ConfigCodeEnum.FACT_POSITION_TIMESHEET),
                CreateConfigItem(ConfigCodeEnum.FACT_POSITION_ABSENCE, ConfigRoleEnum.ARTICLE_POSITION_ABSENCE, ArticleType.PART_CODE_ARTICLE, ArticleBind.ARTICLE_REQ,
                    ConfigCodeEnum.FACT_POSITION_TIMESHEET, ConfigCodeEnum.FACT_CONTRACT_ATTEND_ITEM),
            };
            return configList;
        }
    }
}
