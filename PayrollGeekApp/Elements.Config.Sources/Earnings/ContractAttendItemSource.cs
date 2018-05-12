using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using TDay = Byte;
    using TSeconds = Int32;

    using Legalist.Constants;
    using Module.Interfaces.Elements;
    using Module.Json;
    using Module.Libs;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class ContractAttendItemSource : ISourceValues, ICloneable
    {
        public TDay DayFrom { get; set; }
        public TDay DayStop { get; set; }
        public WorkDayPieceType[] PieceInDays { get; set; }
        public TSeconds[] HoursInDays { get; set; }

        public ContractAttendItemSource()
        {
            DayFrom = 0;
            DayStop = 0;
            PieceInDays = new WorkDayPieceType[0];
            HoursInDays = new TSeconds[0];
        }

        public ContractAttendItemSource(Byte dayFrom, Byte dayStop, WorkDayPieceType[] pieceInDays, TSeconds[] hoursInDays)
        {
            DayFrom = dayFrom;
            DayStop = dayStop;
            PieceInDays = pieceInDays.ToArray();
            HoursInDays = hoursInDays.ToArray();
        }

        public virtual object Clone()
        {
            ContractAttendItemSource cloneSource = (ContractAttendItemSource)this.MemberwiseClone();

            cloneSource.DayFrom = this.DayFrom;
            cloneSource.DayStop = this.DayStop;
            cloneSource.PieceInDays = this.PieceInDays.ToArray();
            cloneSource.HoursInDays = this.HoursInDays.ToArray();

            return cloneSource;
        }
    }
}
