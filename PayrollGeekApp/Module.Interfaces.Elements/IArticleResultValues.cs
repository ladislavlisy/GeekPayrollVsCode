using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Interfaces.Elements
{
    using ResultCode = UInt16;
    public interface IArticleResultValues
    {
        string Description();
        string ToResultExport(string targetSymbol);
        bool IsResultCodeValue(ResultCode code);
        bool IsContractFromStopValue();
        bool IsPositionFromStopValue();
        bool IsMonthFromStopValue();
        bool IsFullWeeksValue();
        bool IsRealWeeksValue();
        bool IsFullMonthValue();
        bool IsRealMonthValue();
        bool IsTermMonthValue();
        bool IsMonthAttendanceValue();
        bool IsPaymentMoneyValue();
        bool IsDeclarationTaxingValue();
        bool IsDeclarationHealthValue();
        bool IsDeclarationSocialValue();
        bool IsIncomeTaxableValue();
        bool IsTransferIncomeValue();
        bool IsInsuranceBasisValue();
        bool IsTaxingBasisValue();
    }
}
