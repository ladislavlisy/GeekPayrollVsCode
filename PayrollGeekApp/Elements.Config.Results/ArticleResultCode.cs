using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Results
{
    using EnumCode = UInt16;
    public enum ArticleResultCode : EnumCode
    {
        RESULT_VALUE_FROM_STOP_CONTRACT = 1,
        RESULT_VALUE_FROM_STOP_POSITION,
        RESULT_VALUE_MONTH_FROM_STOP,
        RESULT_VALUE_FULL_WEEKS_HOURS,
        RESULT_VALUE_REAL_WEEKS_HOURS,
        RESULT_VALUE_FULL_MONTH_HOURS,
        RESULT_VALUE_REAL_MONTH_HOURS,
        RESULT_VALUE_TERM_MONTH_HOURS
    }
    public static class ArticleCzCodeExtensions
    {
        public static string GetSymbol(this ArticleResultCode article)
        {
            return article.ToString();
        }
    }
}
