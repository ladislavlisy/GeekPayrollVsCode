using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Results
{
    using ConfigCode = UInt16;
    using ResultCode = UInt16;

    using TDay = Byte;
    using TSeconds = Int32;

    using Module.Interfaces.Elements;
    using Module.Libs;
    using MaybeMonad;
    using Module.Codes;
    using Legalist.Constants;

    public class ArticleGeneralResult : IArticleResult
    {
        public ArticleGeneralResult(ConfigCode code)
        {
            InternalCode = code;
            ResultValues = new ResultValuesStore();
        }
        public IArticleResult AddWorkWeeksFullScheduleValue(TSeconds[] hoursWeek)
        {
            IArticleResultValues value = new WorkWeekResultValue((ResultCode)ArticleResultCode.RESULT_VALUE_FULL_WEEKS_HOURS, hoursWeek);

            ResultValues = ResultValues.Concat(value);

            return this;
        }
        public IArticleResult AddWorkWeeksRealScheduleValue(TSeconds[] hoursWeek)
        {
            IArticleResultValues value = new WorkWeekResultValue((ResultCode)ArticleResultCode.RESULT_VALUE_REAL_WEEKS_HOURS, hoursWeek);

            ResultValues = ResultValues.Concat(value);

            return this;
        }
        
        public IArticleResult AddContractFromStop(DateTime? dateFrom, DateTime? dateStop, WorkEmployTerms contractType)
        {
            IArticleResultValues value = new TermFromStopContractValue(dateFrom, dateStop, contractType);

            ResultValues = ResultValues.Concat(value);

            return this;
        }
        public IArticleResult AddPositionFromStop(DateTime? dateFrom, DateTime? dateStop, WorkPositionType positionType)
        {
            IArticleResultValues value = new TermFromStopPositionValue(dateFrom, dateStop, positionType);

            ResultValues = ResultValues.Concat(value);

            return this;
        }
        public IArticleResult AddMonthFromStop(TDay dayFrom, TDay dayStop)
        {
            IArticleResultValues value = new MonthFromStopResultValue(dayFrom, dayStop);

            ResultValues = ResultValues.Concat(value);

            return this;
        }
        public IArticleResult AddWorkMonthFullScheduleValue(TSeconds[] hoursMonth)
        {
            IArticleResultValues value = new WorkMonthResultValue((ResultCode)ArticleResultCode.RESULT_VALUE_FULL_MONTH_HOURS, hoursMonth);

            ResultValues = ResultValues.Concat(value);

            return this;
        }
        public IArticleResult AddWorkMonthRealScheduleValue(TSeconds[] hoursMonth)
        {
            IArticleResultValues value = new WorkMonthResultValue((ResultCode)ArticleResultCode.RESULT_VALUE_REAL_MONTH_HOURS, hoursMonth);

            ResultValues = ResultValues.Concat(value);

            return this;
        }
        public IArticleResult AddWorkMonthTermScheduleValue(TSeconds[] hoursMonth)
        {
            IArticleResultValues value = new WorkMonthResultValue((ResultCode)ArticleResultCode.RESULT_VALUE_TERM_MONTH_HOURS, hoursMonth);

            ResultValues = ResultValues.Concat(value);

            return this;
        }

        protected ConfigCode InternalCode { get; set; }
        protected ResultValuesStore ResultValues { get; set; }

        public ConfigCode Code()
        {
            return InternalCode;
        }
        public object Clone()
        {
            ArticleGeneralResult cloneResult = (ArticleGeneralResult)this.MemberwiseClone();
            cloneResult.InternalCode = InternalCode;
            cloneResult.ResultValues = CloneUtils<ResultValuesStore>.CloneOrNull(ResultValues);

            return cloneResult;
        }

        public override string ToString()
        {
            string articleCode = ArticleCodeAdapter.GetSymbol(InternalCode);

            string articleDesc = string.Join("\r\n", ResultValues.Select((v) => (v.Description())));

            return string.Format("{0}\r\n{1}", articleCode, articleDesc);
        }
        public Maybe<T> ReturnValue<T>(Func<IArticleResultValues, bool> filterFunc) where T : class, IArticleResultValues
        {
            IArticleResultValues generalvalue = ResultValues.SingleOrDefault(filterFunc);
            T value = generalvalue as T;
            if (value == null)
            {
                return Maybe<T>.Nothing;
            }
            return Maybe.From<T>(value);
        }
        public Maybe<T> ReturnValueForResultCode<T>(ResultCode filterCode) where T : class, IArticleResultValues
        {
            return ReturnValue<T>((x) => (x.IsResultCodeValue(filterCode)));
        }
        public Maybe<TermFromStopContractValue> ReturnContractTermFromStopValue()
        {
            return ReturnValue<TermFromStopContractValue>((v) => (v.IsContractFromStopValue()));
        }
        public Maybe<TermFromStopPositionValue> ReturnPositionTermFromStopValue()
        {
            return ReturnValue<TermFromStopPositionValue>((v) => (v.IsPositionFromStopValue()));
        }
        public Maybe<MonthFromStopResultValue> ReturnMonthFromStopValue()
        {
            return ReturnValue<MonthFromStopResultValue>((v) => (v.IsMonthFromStopValue()));
        }
        public Maybe<WorkMonthResultValue> ReturnFullMonthValue()
        {
            return ReturnValue<WorkMonthResultValue>((v) => (v.IsFullMonthValue()));
        }
        public Maybe<WorkMonthResultValue> ReturnRealMonthValue()
        {
            return ReturnValue<WorkMonthResultValue>((v) => (v.IsRealMonthValue()));
        }
        public Maybe<WorkMonthResultValue> ReturnTermMonthValue()
        {
            return ReturnValue<WorkMonthResultValue>((v) => (v.IsTermMonthValue()));
        }
    }
}
