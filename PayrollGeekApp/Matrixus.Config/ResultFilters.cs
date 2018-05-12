using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Matrixus.Config
{
    using ResultItem = Module.Interfaces.Elements.IArticleResultValues;
    public static class ResultFilters
    {
        public static bool PaymentMoneyFunc(ResultItem result)
        {
            return (result.IsPaymentMoneyValue());
        }
        public static bool MonthAttendanceFunc(ResultItem result)
        {
            return (result.IsMonthAttendanceValue());
        }
        public static bool IncomeTaxableFunc(ResultItem result)
        {
            return (result.IsIncomeTaxableValue());
        }
        public static bool TransferIncomeValue(ResultItem result)
        {
            return (result.IsTransferIncomeValue());
        }
        public static bool InsuranceBasisValue(ResultItem result)
        {
            return (result.IsInsuranceBasisValue());
        }
        public static bool TaxingBasisValue(ResultItem result)
        {
            return (result.IsTaxingBasisValue());
        }
    }
}
