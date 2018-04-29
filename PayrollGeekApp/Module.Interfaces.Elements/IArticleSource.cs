using System;
using System.Collections.Generic;
using ResultMonad;

namespace ElementsLib.Module.Interfaces.Elements
{
    using ConfigCode = UInt16;
    using TargetItem = IArticleTarget;
    using ResultPack = ResultMonad.Result<IArticleResult, string>;
    using ResultPair = KeyValuePair<IArticleTarget, ResultMonad.Result<IArticleResult, string>>;

    using Items;
    using Legalist;
    public interface IArticleSource : ICloneable
    {
        ConfigCode Code();
        void ImportSourceValues(ISourceValues values);
        void SetSourceCode(ConfigCode code);
        ISourceValues ExportSourceValues();
        ResultMonad.Result<IArticleSource, string> CloneSourceAndSetValues<T>(ConfigCode configCode, ISourceValues values) where T : class, IArticleSource;
        IEnumerable<ResultPack> EvaluateResults(TargetItem evalTarget, Period evalPeriod, IPeriodProfile evalProfile, IEnumerable<ResultPair> evalResults);
    }
}