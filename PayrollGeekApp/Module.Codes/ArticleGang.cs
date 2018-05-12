using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Codes
{
    using EnumCode = UInt16;
    public enum ArticleGang : EnumCode
    {
        EARNINGS_GANG = 1,
        TRANSFER_GANG,
        GROSSNET_GANG,
        DEDUCTED_GANG,
        PAYMENTS_GANG,
    }
}
