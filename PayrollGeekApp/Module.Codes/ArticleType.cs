using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Codes
{
    using EnumCode = UInt16;
    public enum ArticleType : EnumCode
    {
        NO_HEAD_PART_TYPE = 0,
        HEAD_CODE_ARTICLE = 1,
        PART_CODE_ARTICLE = 2,
    }
}
