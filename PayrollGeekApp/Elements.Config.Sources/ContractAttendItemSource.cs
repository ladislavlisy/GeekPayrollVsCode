using System;
using System.Linq;

namespace ElementsLib.Elements.Config.Sources
{
    using TSeconds = Int32;

    using Legalist.Constants;
    using Module.Interfaces.Elements;
 
    public class ContractAttendItemSource : ISourceValues, ICloneable
    {
        public Byte DayFrom { get; set; }
        public Byte DayStop { get; set; }
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
