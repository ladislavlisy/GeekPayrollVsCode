using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Constants
{
    using CONSTANTS_CODE = UInt16;

    public enum WorkEmployTerms : CONSTANTS_CODE
    {
        WORKTERM_EMPLOYMENT_1 = 0,
        WORKTERM_CONTRACTER_A = 1,
        WORKTERM_CONTRACTER_T = 2,
        WORKTERM_STATUTORY__Q = 3,
        WORKTERM_UNKNOWN_TYPE = 9
    };
    public enum WorkHealthTerms : CONSTANTS_CODE
    {
        HEALTH_TERM_EMPLOYMENT = 0,
        HEALTH_TERM_AGREE_WORK = 1,
        HEALTH_TERM_AGREE_TASK = 2,
        HEALTH_TERM_OUT_EMPLOY = 3
    };
    public enum WorkSocialTerms : CONSTANTS_CODE
    {
        SOCIAL_TERM_EMPLOYMENT = 0,
        SOCIAL_TERM_SMALL_EMPL = 1,
        SOCIAL_TERM_SHORT_MEET = 2,
        SOCIAL_TERM_SHORT_DENY = 3
    };
}
