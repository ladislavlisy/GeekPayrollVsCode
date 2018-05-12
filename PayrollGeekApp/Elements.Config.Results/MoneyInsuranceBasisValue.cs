using System;

namespace ElementsLib.Elements.Config.Results
{
    using TAmountDec = Decimal;
    using ResultCode = UInt16;

    using Module.Libs;
    using System.Linq;
    using Module.Items.Utils;

    public class MoneyInsuranceBasisValue : GeneralResultValue
    {
        public TAmountDec BasisRawNumb { get; protected set; }
        public TAmountDec BasisRounded { get; protected set; }
        public TAmountDec BasisCutdown { get; protected set; }
        public TAmountDec AboveCutdown { get; protected set; }
        public TAmountDec BasisFinNumb { get; protected set; }
        public MoneyInsuranceBasisValue(ResultCode code, TAmountDec basisRawly, TAmountDec basisRound, TAmountDec basisCuter, TAmountDec aboveCuter, TAmountDec basisFinal) : base(code)
        {
            this.BasisRawNumb = basisRawly;
            this.BasisRounded = basisRound;
            this.BasisCutdown = basisCuter;
            this.AboveCutdown = aboveCuter;
            this.BasisFinNumb = basisFinal;
        }
        public override string Description()
        {
            string formatedRawValue = BasisRawNumb.FormatAmount();
            string formatedRndValue = BasisRounded.FormatAmount();
            string formatedCutValue = BasisCutdown.FormatAmount();
            string formatedCutAbove = AboveCutdown.FormatAmount();
            string formatedFinValue = BasisFinNumb.FormatAmount();

            return string.Format("{0}: Raw Basis for Insurance: {1}, Rounded Basis for Insurance: {2}, CutDown Basis for Insurance: {3}, Above CutDown for Insurance: {4}, Final Basis for Insurance: {5}",
                Code.ToEnum<ArticleResultCode>().GetSymbol(),
                formatedRawValue, formatedRndValue, formatedCutValue, formatedCutAbove, formatedFinValue);
        }
        public override string ToResultExport(string targetSymbol)
        {
            string hoursFormated = "";
            string dayesFormated = "";
            string moneyFormated = BasisRawNumb.FormatAmount();
            string basisFormated = BasisFinNumb.FormatAmount();
            string payeeFormated = "";

            return string.Format("{0}\t{1}\tHours\t{2}\tDays\t{3}\tIncome Amount\t{4}\tBasis Amount\t{5}\tPayment\t{6}",
                targetSymbol, Code.ToEnum<ArticleResultCode>().GetSymbol(),
                hoursFormated, dayesFormated, moneyFormated, basisFormated, payeeFormated);
        }
    }
}