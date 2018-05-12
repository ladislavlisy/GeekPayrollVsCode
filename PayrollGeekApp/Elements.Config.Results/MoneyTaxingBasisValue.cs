using System;

namespace ElementsLib.Elements.Config.Results
{
    using TAmountDec = Decimal;
    using ResultCode = UInt16;

    using Module.Libs;
    using System.Linq;
    using Module.Items.Utils;

    public class MoneyTaxingBasisValue : GeneralResultValue
    {
        public TAmountDec BasisRawNumb { get; protected set; }
        public TAmountDec BasisRounded { get; protected set; }
        public TAmountDec BasisFinNumb { get; protected set; }
        public MoneyTaxingBasisValue(ResultCode code, TAmountDec basisRawly, TAmountDec basisRound, TAmountDec basisFinal) : base(code)
        {
            this.BasisRawNumb = basisRawly;
            this.BasisRounded = basisRound;
            this.BasisFinNumb = basisFinal;
        }
        public MoneyTaxingBasisValue(ResultCode code, TAmountDec basisFinal) : base(code)
        {
            this.BasisRawNumb = TAmountDec.Zero;
            this.BasisRounded = TAmountDec.Zero;
            this.BasisFinNumb = basisFinal;
        }
        public override string Description()
        {
            string formatedRawValue = BasisRawNumb.FormatAmount();
            string formatedRndValue = BasisRounded.FormatAmount();
            string formatedFinValue = BasisFinNumb.FormatAmount();

            return string.Format("{0}: Raw Basis for Taxing: {1}, Rounded Basis for Taxing: {1}, Final Basis for Taxing: {1}",
                Code.ToEnum<ArticleResultCode>().GetSymbol(), 
                formatedRawValue, formatedRndValue, formatedFinValue);
        }
        public override string ToResultExport(string targetSymbol)
        {
            string hoursFormated = "";
            string dayesFormated = "";
            string moneyFormated = "";
            string basisFormated = BasisFinNumb.FormatAmount();
            string payeeFormated = "";

            return string.Format("{0}\t{1}\tHours\t{2}\tDays\t{3}\tIncome Amount\t{4}\tBasis Amount\t{5}\tPayment\t{6}",
                targetSymbol, Code.ToEnum<ArticleResultCode>().GetSymbol(),
                hoursFormated, dayesFormated, moneyFormated, basisFormated, payeeFormated);
        }
    }
}