using System;

namespace ElementsLib.Elements.Config.Results
{
    using TAmountDec = Decimal;
    using ResultCode = UInt16;

    using Module.Libs;
    using System.Linq;
    using Module.Items.Utils;

    public class MoneyTransferValue : GeneralResultValue
    {
        public TAmountDec Payment { get; protected set; }
        public MoneyTransferValue(ResultCode code, TAmountDec payment) : base(code)
        {
            this.Payment = payment;
        }
        public override string Description()
        {
            string formatedValue = Payment.FormatAmount();

            return string.Format("{0}: Transfer: {1}",
                Code.ToEnum<ArticleResultCode>().GetSymbol(), formatedValue);
        }
        public override string ToResultExport(string targetSymbol)
        {
            string hoursFormated = "";
            string dayesFormated = "";
            string moneyFormated = "";
            string basisFormated = "";
            string payeeFormated = Payment.FormatAmount();

            return string.Format("{0}\t{1}\tHours\t{2}\tDays\t{3}\tIncome Amount\t{4}\tBasis Amount\t{5}\tPayment\t{6}",
                targetSymbol, Code.ToEnum<ArticleResultCode>().GetSymbol(),
                hoursFormated, dayesFormated, moneyFormated, basisFormated, payeeFormated);
        }
    }
}