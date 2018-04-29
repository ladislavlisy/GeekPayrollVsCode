using System;
using System.Linq;

namespace ElementsLib.Elements.Config.Results
{
    using TSeconds = Int32;
    using ResultCode = UInt16;

    using Module.Items.Utils;
    using Module.Libs;

    public class WorkWeekResultValue : GeneralResultValue
    {
        public WorkWeekResultValue(ResultCode code, TSeconds[] hoursWeek) : base(code)
        {
            HoursWeek = hoursWeek.ToArray();
        }
        public TSeconds[] HoursWeek { get; protected set; }
        public override string Description()
        {
            string formatedValue = string.Join("; ", HoursWeek.Select((h) => (FormatUtils.HoursFormat(h))));
            return string.Format("{0}: Hours in week: {1}",
                Code.ToEnum<ArticleResultCode>().GetSymbol(),
                formatedValue);
        }

    }
}
