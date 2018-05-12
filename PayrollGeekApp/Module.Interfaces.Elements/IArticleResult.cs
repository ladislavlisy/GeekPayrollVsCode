using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Interfaces.Elements
{
    using ConfigCode = UInt16;
    using ConfigBase = Module.Interfaces.Matrixus.IArticleBaseFeatures;
    using ConfigGang = UInt16;
    using ConfigRole = UInt16;
    using ConfigType = UInt16;
    using ConfigBind = UInt16;
    using ResultCode = UInt16;

    using TDay = Byte;
    using TSeconds = Int32;
    using TAmountDec = Decimal;

    using ElementsLib.Legalist.Constants;
    using MaybeMonad;
    using Matrixus;

    public interface IArticleResult : IArticleBaseFeatures, ICloneable
    {
        ConfigBase Config();
        IArticleResult AddContractFromStop(DateTime? dateFrom, DateTime? dateStop, WorkEmployTerms contractType);
        IArticleResult AddPositionFromStop(DateTime? dateFrom, DateTime? dateStop, WorkPositionType positionType);
        IArticleResult AddMonthFromStop(TDay dayFrom, TDay dayStop);
        IArticleResult AddWorkWeeksFullScheduleValue(TSeconds[] hoursWeek);
        IArticleResult AddWorkWeeksRealScheduleValue(TSeconds[] hoursWeek);
        IArticleResult AddWorkMonthFullScheduleValue(TSeconds[] hoursMonth);
        IArticleResult AddWorkMonthRealScheduleValue(TSeconds[] hoursMonth);
        IArticleResult AddWorkMonthTermScheduleValue(TSeconds[] hoursMonth);
        IArticleResult AddMonthAttendanceScheduleValue(TDay dayFrom, TDay dayStop, TSeconds[] hoursMonth);
        IArticleResult AddMoneyPaymentValue(TAmountDec paymentAmount);
        IArticleResult AddMoneyTransferValue(TAmountDec transferAmount);
        IArticleResult AddMoneyTransferIncomeValue(TAmountDec incomeAmount);
        IArticleResult AddMoneyInsuranceBasisValue(TAmountDec basisRawly, TAmountDec basisRound, TAmountDec basisCuter, TAmountDec aboveCuter, TAmountDec basisFinal);
        IArticleResult AddMoneyTaxingBasisValue(TAmountDec basisRawly, TAmountDec basisRound, TAmountDec basisFinal);
        IArticleResult AddTaxPartialBaseValue(TAmountDec partialBase);
        IArticleResult AddTaxSolidaryBaseValue(TAmountDec partialBase);
        IArticleResult AddDeclarationTaxingValue(Byte statement, WorkTaxingTerms summarize, Byte declaracy, Byte residency, TAmountDec healthSum, TAmountDec socialSum);
        IArticleResult AddDeclarationHealthValue(Byte statement, WorkHealthTerms summarize, TAmountDec totalBase, Byte foreigner);
        IArticleResult AddDeclarationSocialValue(Byte statement, WorkSocialTerms summarize, TAmountDec totalBase, Byte foreigner);
        IArticleResult AddIncomeTaxGeneralValue(WorkTaxingTerms summarize, Byte statement, Byte residency, 
            TAmountDec general, TAmountDec lolevel, TAmountDec agrtask, TAmountDec partner, TAmountDec exclude);
        IArticleResult AddIncomeInsHealthValue(WorkHealthTerms summarize, TAmountDec related, TAmountDec exclude);
        IArticleResult AddIncomeInsSocialValue(WorkSocialTerms summarize, TAmountDec related, TAmountDec exclude);
        string DecoratedError(string message);
        string ToResultExport(string targetSymbol);

        Maybe<T> ReturnValue<T>(Func<IArticleResultValues, bool> filterFunc) where T : class, IArticleResultValues;
        Maybe<T> ReturnValueForResultCode<T>(ResultCode filterCode) where T : class, IArticleResultValues;
    }
}
