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

    public static class FindResultUtils
    {
        private static Result<MoneyAmountSum, string> GetIncomeAmount(MoneyTransferIncomeValue resultValue)
        {
            return Result.Ok<MoneyAmountSum, string>(new MoneyAmountSum(resultValue.Payment));
        }
        private static Result<MoneyAmountSum, string> GetTaxBasisAmount(MoneyTaxingBasisValue resultValue)
        {
            return Result.Ok<MoneyAmountSum, string>(new MoneyAmountSum(resultValue.BasisFinNumb));
        }
        private static Result<MoneyAmountSum, string> GetInsBasisAmount(MoneyInsuranceBasisValue resultValue)
        {
            return Result.Ok<MoneyAmountSum, string>(new MoneyAmountSum(resultValue.BasisFinNumb));
        }
        public static Result<MoneyAmountSum, string> FindMoneyTransferIncomeValue(IEnumerable<ResultPair> evalResults, Func<TargetItem, bool> filterTargetFunc)
        {
            return evalResults.FindAndTransformResultValue<ArticleGeneralResult, MoneyTransferIncomeValue, MoneyAmountSum>(filterTargetFunc, 
                ResultFilters.TransferIncomeValue, FindResultUtils.GetIncomeAmount);
        }
        public static Result<MoneyAmountSum, string> FindMoneyTaxingBasisValue(IEnumerable<ResultPair> evalResults, Func<TargetItem, bool> filterTargetFunc)
        {
            return evalResults.FindAndTransformResultValue<ArticleGeneralResult, MoneyTaxingBasisValue, MoneyAmountSum>(filterTargetFunc, 
                ResultFilters.TaxingBasisValue, FindResultUtils.GetTaxBasisAmount);
        }
        public static Result<MoneyAmountSum, string> FindMoneyInsuranceBasisValue(IEnumerable<ResultPair> evalResults, Func<TargetItem, bool> filterTargetFunc)
        {
            return evalResults.FindAndTransformResultValue<ArticleGeneralResult, MoneyInsuranceBasisValue, MoneyAmountSum>(filterTargetFunc, 
                ResultFilters.TaxingBasisValue, FindResultUtils.GetInsBasisAmount);
        }
    }
}
