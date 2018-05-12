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
        RESULT_VALUE_TERM_MONTH_HOURS,
        RESULT_VALUE_ATTN_MONTH_HOURS,
        RESULT_VALUE_PAYMENT_MONEY,
        RESULT_VALUE_DECLARATION_TAXING,
        RESULT_VALUE_DECLARATION_HEALTH,
        RESULT_VALUE_DECLARATION_SOCIAL,
        RESULT_VALUE_INCOME_SUM_TAXING,
        RESULT_VALUE_INCOME_SUM_HEALTH,
        RESULT_VALUE_INCOME_SUM_SOCIAL,
        RESULT_VALUE_TRANSFER_INCOME_MONEY,
        RESULT_VALUE_INSURANCE_BASIS_MONEY,
        RESULT_VALUE_TAXING_BASIS_MONEY,
        RESULT_VALUE_TRANSFER_MONEY,
    }
    public static class ArticleCzCodeExtensions
    {
        public static string GetSymbol(this ArticleResultCode article)
        {
            return article.ToString();
        }
    }
}
