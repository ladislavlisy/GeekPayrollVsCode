using System;
using System.Text.RegularExpressions;

namespace ElementsLib.Module.Common
{
    using Libs;
    public static class GeneralNamesFactory
    {
        public static string ClassNameFor(string nameclazzPostfix, string nameclazzPattern, string targetName)
        {
            Regex regexObj = new Regex(nameclazzPattern, RegexOptions.Singleline);
            Match matchResult = regexObj.Match(targetName);
            string matchResultName = "";
            if (matchResult.Success)
            {
                GroupCollection regexCol = matchResult.Groups;
                if (regexCol.Count == 2)
                {
                    matchResultName = regexCol[1].Value;
                }
            }
            string className = matchResultName.Underscore().Camelize() + nameclazzPostfix;

            return className;
        }

        public static string FullClassNameFor(string namespacePrefix, string nameclazzPostfix, string nameclazzPattern, string targetName)
        {
            string easyClassName = ClassNameFor(nameclazzPostfix, nameclazzPattern, targetName);

            string fullClassName = string.Join(".", namespacePrefix, easyClassName);

            return fullClassName;
        }
    }
}
