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
        bool IsResultCodeValue(ResultCode code);
        bool IsContractFromStopValue();
        bool IsPositionFromStopValue();
        bool IsMonthFromStopValue();
        bool IsFullWeeksValue();
        bool IsPartWeeksValue();
        bool IsFullMonthValue();
        bool IsRealMonthValue();
        bool IsTermMonthValue();
    }
}
