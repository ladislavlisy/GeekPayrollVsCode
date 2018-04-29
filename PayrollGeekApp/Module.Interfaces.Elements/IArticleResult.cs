using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Interfaces.Elements
{
    using ConfigCode = UInt16;
    using ResultCode = UInt16;
    using TDay = Byte;
    using TSeconds = Int32;

    using ElementsLib.Legalist.Constants;
    using MaybeMonad;
    public interface IArticleResult : ICloneable
    {
        ConfigCode Code();
        IArticleResult AddContractFromStop(DateTime? dateFrom, DateTime? dateStop, WorkEmployTerms contractType);
        IArticleResult AddPositionFromStop(DateTime? dateFrom, DateTime? dateStop, WorkPositionType positionType);
        IArticleResult AddMonthFromStop(TDay dayFrom, TDay dayStop);
        IArticleResult AddWorkWeeksFullScheduleValue(TSeconds[] hoursWeek);
        IArticleResult AddWorkWeeksRealScheduleValue(TSeconds[] hoursWeek);
        IArticleResult AddWorkMonthFullScheduleValue(TSeconds[] hoursMonth);
        IArticleResult AddWorkMonthRealScheduleValue(TSeconds[] hoursMonth);
        IArticleResult AddWorkMonthTermScheduleValue(TSeconds[] hoursMonth);
        Maybe<T> ReturnValue<T>(Func<IArticleResultValues, bool> filterFunc) where T : class, IArticleResultValues;
        Maybe<T> ReturnValueForResultCode<T>(ResultCode filterCode) where T : class, IArticleResultValues;
    }
}
