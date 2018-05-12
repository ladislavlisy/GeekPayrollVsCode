using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using TSeconds = Int32;

    using Legalist.Constants;
    using Module.Interfaces.Elements;
    using Module.Json;
    using Module.Libs;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class PositionWorkingSource : ISourceValues, ICloneable
    {
        public TSeconds[] HoursInPeriod { get; set; }

        public PositionWorkingSource()
        {
            HoursInPeriod = new TSeconds[0];
        }

        public PositionWorkingSource(TSeconds[] hoursInPeriod)
        {
            HoursInPeriod = hoursInPeriod.ToArray();
        }

        public virtual object Clone()
        {
            PositionWorkingSource cloneSource = (PositionWorkingSource)this.MemberwiseClone();

            cloneSource.HoursInPeriod = this.HoursInPeriod.ToArray();

            return cloneSource;
        }

    }
}
