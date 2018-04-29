using System;

namespace ElementsLib.Module.Codes
{
    using ConfigRoleEnum = ArticleRoleCz;
    using EnumRole = UInt16;

    using Libs;
    using System.Collections.Generic;
    using System.Linq;

    public class ArticleRoleAdapter
    {
        public static ConfigRoleEnum CreateEnum(EnumRole symbolNumb)
        {
            return symbolNumb.ToEnum<ConfigRoleEnum>(GetDefaultsEnum());
        }
        public static ConfigRoleEnum CreateEnum(string symbolName)
        {
            return symbolName.ToEnum<ConfigRoleEnum>(GetDefaultsEnum());
        }
        public static EnumRole CreateCode(EnumRole symbolNumb)
        {
            return (EnumRole)symbolNumb.ToEnum<ConfigRoleEnum>(GetDefaultsEnum());
        }
        public static EnumRole CreateCode(string symbolName)
        {
            return (EnumRole)symbolName.ToEnum<ConfigRoleEnum>(GetDefaultsEnum());
        }
        public static ConfigRoleEnum GetDefaultsEnum()
        {
            return ConfigRoleEnum.ARTICLE_UNKNOWN;
        }
        public static EnumRole GetDefaultsRole()
        {
            return (EnumRole)ConfigRoleEnum.ARTICLE_UNKNOWN;
        }
        public static IEnumerable<EnumRole> GetSelectedRoles()
        {
            IEnumerable<ConfigRoleEnum> symbolList = EnumUtilsExtensions.GetSelectedItems<ConfigRoleEnum>().ToList();

            IEnumerable<EnumRole> configList = symbolList.Select((c) => ((EnumRole)c)).ToList();

            return configList;
        }

        public static IEnumerable<EnumRole> GetAllRoles()
        {
            IEnumerable<ConfigRoleEnum> symbolList = EnumUtilsExtensions.GetAllItems<ConfigRoleEnum>().ToList();

            IEnumerable<EnumRole> configList = symbolList.Select((c) => ((EnumRole)c)).ToList();

            return configList;
        }
        public static IEnumerable<ConfigRoleEnum> GetSelectedEnums()
        {
            IEnumerable<ConfigRoleEnum> configList = EnumUtilsExtensions.GetSelectedItems<ConfigRoleEnum>().ToList();

            return configList;
        }

        public static IEnumerable<ConfigRoleEnum> GetAllEnums()
        {
            IEnumerable<ConfigRoleEnum> configList = EnumUtilsExtensions.GetAllItems<ConfigRoleEnum>().ToList();

            return configList;
        }
        public static string GetSymbol(EnumRole symbolNumb)
        {
            ConfigRoleEnum symbol = CreateEnum(symbolNumb);

            return symbol.GetSymbol();
        }

    }
}
