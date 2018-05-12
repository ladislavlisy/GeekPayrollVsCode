using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using Legalist.Constants;
    using Module.Interfaces.Elements;
    using Module.Libs;
 
    public class PositionTermSource : ISourceValues, ICloneable
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateStop { get; set; }
        public WorkPositionType PositionType { get; set; }

        public PositionTermSource()
        {
            DateFrom = null;
            DateStop = null;
            PositionType = WorkPositionType.POSITION_EXCLUSIVE;
        }

        public PositionTermSource(DateTime? dateFrom, DateTime? dateStop, WorkPositionType positionType)
        {
            DateFrom = dateFrom;
            DateStop = dateStop;
            PositionType = positionType;
        }

        public virtual object Clone()
        {
            PositionTermSource cloneSource = (PositionTermSource)this.MemberwiseClone();

            cloneSource.DateFrom = this.DateFrom;
            cloneSource.DateStop = this.DateStop;
            cloneSource.PositionType = this.PositionType;

            return cloneSource;
        }

    }
}
