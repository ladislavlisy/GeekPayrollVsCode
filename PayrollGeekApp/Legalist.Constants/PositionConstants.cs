using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Constants
{
    using CONSTANTS_CODE = UInt16;

    public enum WorkPositionType : CONSTANTS_CODE
    {
        POSITION_EXCLUSIVE = 0,
        POSITION_SECONDARY = 1,
        POSITION_CONTINUAL = 2,
        POSITION_AGREEMENT = 3,
    };
    public enum WorkScheduleType : CONSTANTS_CODE
    {
        SCHEDULE_NORMALY_WEEK = 0,
        SCHEDULE_SPECIAL_WEEK = 1,
        SCHEDULE_SPECIAL_TURN = 2,
        SCHEDULE_NONEDAY_WORK = 9,
    };
    public enum WorkDayPieceType : CONSTANTS_CODE
    {
        WORKDAY_NONE = 0,
        WORKDAY_FULL = 1,
        WORKDAY_HALF = 2,
        WORKDAY_TIME = 3,
    };
}
