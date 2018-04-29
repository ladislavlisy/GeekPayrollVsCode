using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Items
{
    using PeriodCode = UInt32;
    using PeriodTerm = Byte;
    using PeriodYear = UInt16;
    using PeriodMnth = Byte;

    using Utils;

    public class Period : IComparable
    {
        public static readonly PeriodCode PRESENT = 0;

        public const PeriodTerm TERM_BEG_FINISHED = 32;

        public const PeriodTerm TERM_END_FINISHED = 0;

        public const int WEEKSUN_SUNDAY = 0;

        public const int WEEKMON_SUNDAY = 7;

        public Period(PeriodCode code)
        {
            this.Code = code;
        }

        public Period(PeriodYear year, PeriodMnth month) : this((PeriodCode)(year * 100 + month))
        {
        }

        public PeriodCode Code { get; private set; }

        public PeriodYear YearUInt()
        {
            return ((PeriodYear)(Code / 100));
        }

        public PeriodMnth MonthByte()
        {
            return (byte)(Code % 100);
        }

        public int Year()
        {
            return (int)(Code / 100);
        }

        public int Month()
        {
            return (int)(Code % 100);
        }

        public int MonthOrder()
        {
            return (Math.Max(0, Year() - 2000) * 12 + Month());
        }

        public int DaysInMonth()
        {
            return DateTime.DaysInMonth(Year(), Month());
        }

        public DateTime BeginOfMonth()
        {
            return new DateTime(Year(), Month(), 1);
        }

        public DateTime EndOfMonth()
        {
            return new DateTime(Year(), Month(), DaysInMonth());
        }

        public DateTime DateOfMonth(int dayOrdinal)
        {
            int periodDay = Math.Min(Math.Max(1, dayOrdinal), DaysInMonth());

            return new DateTime(Year(), Month(), periodDay);
        }

        public int WeekDayOfMonth(int dayOrdinal)
        {
            DateTime periodDate = DateOfMonth(dayOrdinal);

            int periodDateCwd = (int)periodDate.DayOfWeek;

            return PeriodUtils.DayOfWeekMonToSun(periodDateCwd);
        }

        public static bool operator <(Period x, Period y)
        {
            return (x.Code < y.Code);
        }

        public static bool operator >(Period x, Period y)
        {
            return (x.Code > y.Code);
        }

        public static bool operator <=(Period x, Period y)
        {
            return (x.Code <= y.Code);
        }

        public static bool operator >=(Period x, Period y)
        {
            return (x.Code >= y.Code);
        }

        public int CompareTo(object obj)
        {
            Period other = obj as Period;

            return this.Code.CompareTo(other.Code);
        }

        public bool isEqualToPeriod(Period other)
        {
            return (this.Code == other.Code);
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            Period other = obj as Period;

            return this.isEqualToPeriod(other);
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;

            result += prime * result + (int)this.Code;

            return result;
        }

        public string YearToString()
        {
            return this.Year().ToString();
        }

        public override string ToString()
        {
            return this.Code.ToString();
        }

        public virtual object Clone()
        {
            Period otherPeriod = (Period)this.MemberwiseClone();
            return otherPeriod;
        }

    }
}
