using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Utils
{
    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using ResultItem = Module.Interfaces.Elements.IArticleResult;
    using ValuesItem = Module.Interfaces.Elements.IArticleResultValues;
    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;

    using Module.Interfaces.Elements;
    using Module.Libs;
    using ResultMonad;
    using Config.Results;
    using Module.Items;
    using Matrixus.Config;

    public static class GetSumResultUtils
    {
        private static Result<TaxableIncomeSum, string> GetSumTaxableIncome(TaxableIncomeSum agr, TargetItem resultTarget, IncomeTaxGeneralValue resultValue)
        {
            return Result.Ok<TaxableIncomeSum, string>(agr.Aggregate(resultValue.IncomeGeneral, resultValue.IncomeExclude,
                resultValue.IncomeLolevel, resultValue.IncomeTaskAgr, resultValue.IncomePartner));
        }
        private static Result<MoneyPaymentSum, string> GetSumMoneyPayment(MoneyPaymentSum agr, TargetItem resultTarget, MoneyPaymentValue resultValue)
        {
            return Result.Ok<MoneyPaymentSum, string>(agr.Aggregate(resultValue.Payment));
        }
        public static Result<TaxableIncomeSum, string> GetSumIncomeTaxGeneral(IEnumerable<ResultPair> results, Func<TargetItem, bool> filterTargetFunc, Func<ResultItem, bool> filterValuesFunc)
        {
            TaxableIncomeSum initialResult = new TaxableIncomeSum();

            Result<TaxableIncomeSum, string> summaryResult = results
                .GetResultValuesInAggrAndError<ResultItem, IncomeTaxGeneralValue, TaxableIncomeSum>(
                    initialResult, filterTargetFunc, filterValuesFunc, ResultFilters.IncomeTaxableFunc, 
                    GetSumResultUtils.GetSumTaxableIncome);

            return summaryResult;
        }
        public static Result<MoneyPaymentSum, string> GetSumMoneyPayment(IEnumerable<ResultPair> results, Func<TargetItem, bool> filterTargetFunc, Func<ResultItem, bool> filterValuesFunc)
        {
            MoneyPaymentSum initialResult = new MoneyPaymentSum(decimal.Zero);

            Result<MoneyPaymentSum, string> summaryResult = results
                .GetResultValuesInAggrAndError<ResultItem, MoneyPaymentValue, MoneyPaymentSum>(
                    initialResult, filterTargetFunc, filterValuesFunc, ResultFilters.PaymentMoneyFunc, 
                    GetSumResultUtils.GetSumMoneyPayment);

            return summaryResult;
        }
    }
}
