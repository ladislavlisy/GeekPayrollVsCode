using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Items.Utils
{
    public static class FormatUtils
    {
        public static string HoursFormat(int seconds)
        {
            int secondsAbs = Math.Abs(seconds);
            int secondsSgn = Math.Sign(seconds);
            int secondsHrs = secondsAbs / 3600;
            int secondsMin = 0;
            int secondsMod = (secondsAbs % 3600);

            if (secondsMod != 0)
            {
                secondsMin = 360000 / secondsMod; 
            }

            if (secondsSgn == -1)
            {
                return string.Format("-{0},{1}", secondsHrs.ToString(), secondsMin.ToString().PadLeft(2, '0'));
            }
            else if (secondsSgn == 1)
            {
                return string.Format("{0},{1}", secondsHrs.ToString(), secondsMin.ToString().PadLeft(2, '0'));
            }
            return string.Format("{0},{1}", secondsHrs.ToString(), secondsMin.ToString().PadLeft(2, '0'));
        }
    }
}
