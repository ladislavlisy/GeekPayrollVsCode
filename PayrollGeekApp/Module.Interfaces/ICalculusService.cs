using System;
using System.Collections.Generic;

namespace ElementsLib.Module.Interfaces
{
    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using SourcePair = KeyValuePair<Elements.IArticleTarget, ResultMonad.Result<Elements.IArticleSource, string>>;
    using ResultPair = KeyValuePair<Elements.IArticleTarget, ResultMonad.Result<Elements.IArticleResult, string>>;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;

    using Legalist;
    using Items;

    public interface ICalculusService
    {
        void Initialize();
        void EvaluateStore(Elements.IArticleSourceStore source, Period evalPeriod, IPeriodProfile evalProfile);
        List<SourcePair> GetEvaluationPath();
        List<ResultPair> GetEvaluationCase();
    }
}
