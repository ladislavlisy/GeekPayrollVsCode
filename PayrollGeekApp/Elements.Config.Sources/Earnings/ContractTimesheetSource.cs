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

    public class ContractTimesheetSource : ISourceValues, ICloneable
    {
        public TSeconds[] HoursInPeriod { get; set; }

        public ContractTimesheetSource()
        {
            HoursInPeriod = new TSeconds[0];
        }

        public ContractTimesheetSource(TSeconds[] hoursInPeriod)
        {
            HoursInPeriod = hoursInPeriod.ToArray();
        }

        public virtual object Clone()
        {
            ContractTimesheetSource cloneSource = (ContractTimesheetSource)this.MemberwiseClone();

            cloneSource.HoursInPeriod = this.HoursInPeriod.ToArray();

            return cloneSource;
        }

    }
}
