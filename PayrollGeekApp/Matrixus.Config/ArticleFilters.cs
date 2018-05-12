using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Matrixus.Config
{
    using ResultItem = Module.Interfaces.Elements.IArticleResult;
    public static class ArticleFilters
    {
        public static bool SelectAllFunc(ResultItem result)
        {
            return true;
        }
        public static bool TaxIncomeFunc(ResultItem result)
        {
            return result.IsTaxingIncome();
        }
        public static bool TaxIncomeHealthFunc(ResultItem result)
        {
            return result.IsTaxingIncome() && result.IsHealthIncome();
        }
        public static bool TaxIncomeSocialFunc(ResultItem result)
        {
            return result.IsTaxingIncome() && result.IsSocialIncome();
        }
        public static bool TaxPartnerFunc(ResultItem result)
        {
            return result.IsTaxingPartner();
        }
        public static bool TaxPartnerHealthFunc(ResultItem result)
        {
            return result.IsTaxingPartner() && result.IsHealthIncome();
        }
        public static bool TaxPartnerSocialFunc(ResultItem result)
        {
            return result.IsTaxingPartner() && result.IsSocialIncome();
        }
        public static bool TaxExcludeFunc(ResultItem result)
        {
            return result.IsTaxingExclude();
        }
        public static bool TaxExcludeHealthFunc(ResultItem result)
        {
            return result.IsTaxingExclude() && result.IsHealthIncome();
        }
        public static bool TaxExcludeSocialFunc(ResultItem result)
        {
            return result.IsTaxingExclude() && result.IsSocialIncome();
        }
        public static bool InsIncomeHealthFunc(ResultItem result)
        {
            return result.IsHealthIncome();
        }
        public static bool InsExcludeHealthFunc(ResultItem result)
        {
            return result.IsHealthExclude();
        }
        public static bool InsIncomeSocialFunc(ResultItem result)
        {
            return result.IsSocialIncome();
        }
        public static bool InsExcludeSocialFunc(ResultItem result)
        {
            return result.IsSocialExclude();
        }
    }
}
