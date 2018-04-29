using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ElementsLib.Service.Calculus
{
    using ConfigCode = UInt16;
    using TargetItem = Module.Interfaces.Elements.IArticleTarget;

    using SourcePair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>>;
    using SourcePack = ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>;

    using ResultPair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>>;
    using ResultPack = ResultMonad.Result<Module.Interfaces.Elements.IArticleResult, string>;

    using SortedPair = KeyValuePair<UInt16, Int32>;

    using Module.Libs;
    using Module.Items;
    using Module.Interfaces;
    using Module.Interfaces.Elements;
    using Module.Interfaces.Matrixus;
    using Module.Interfaces.Legalist;
    using ElementsLib.Elements;

    public class CalculusService : ICalculusService
    {
        private readonly Func<IArticleSource, TargetItem, Period, IPeriodProfile, IEnumerable<ResultPair>, IEnumerable<ResultPack>> _evaluateResultsFunc = (s, t, p, f, r) => s.EvaluateResults(t, p, f, r);

        IArticleConfigProfile ConfigProfile { get; set; }

        IArticleSourceStore SourcesStream { get; set; }
        IArticleResultStore ResultsStream { get; set; }

        IEnumerable<SourcePair> EvaluationPath { get; set; }
        IEnumerable<ResultPair> EvaluationCase { get; set; }

        protected Assembly ModuleAssembly { get; set; }

        protected ConfigCode ContractCode { get; set; }
        protected ConfigCode PositionCode { get; set; }


        public CalculusService(IArticleConfigProfile configProfile)
        {
            this.ConfigProfile = configProfile;
        }

        public void Initialize()
        {
            SourcesStream = new ArticleSourceStore(ConfigProfile);

            ResultsStream = new ArticleResultStore();

            EvaluationPath = new List<SourcePair>();
        }

        public void EvaluateStore(IArticleSourceStore source, Period evalPeriod, IPeriodProfile evalProfile)
        {
            SourcesStream.CopyModel(source);

            SourcesStream.EvolveStream(ContractCode, PositionCode);

            EvaluationPath = SourcesStream.GetEvaluationPath();
            /*
            // payrollData.ModelList - Evaluate => Results 
            */
            EvaluationCase = EvaluateStream(EvaluationPath, evalPeriod, evalProfile);
        }

        public IEnumerable<ResultPair> EvaluateStream(IEnumerable<SourcePair> sourceStream, Period evalPeriod, IPeriodProfile evalProfile)
        {
            IEnumerable<ResultPair> initResults = new List<ResultPair>();

            IEnumerable<ResultPair> dropResults = sourceStream.Aggregate(initResults,
                (agr, s) => (agr.Merge(EvaluateSourceItem(s, evalPeriod, evalProfile, agr)))).ToList(); 

            return dropResults;
        }

        private IEnumerable<ResultPair> EvaluateSourceItem(SourcePair sourceItem, Period evalPeriod, IPeriodProfile evalProfile, IEnumerable<ResultPair> evalResults)
        {
            TargetItem targetInResult = sourceItem.Key;
            SourcePack sourceInResult = sourceItem.Value;

            IEnumerable<ResultPack> resultList = sourceInResult.OnSuccessToResultSetEvaluate(targetInResult, evalPeriod, evalProfile, evalResults, _evaluateResultsFunc);

            return resultList.Select((r) => (new ResultPair(sourceItem.Key, r))).ToList();
        }

        public List<SourcePair> GetEvaluationPath()
        {
            return EvaluationPath.ToList();
        }
        public List<ResultPair> GetEvaluationCase()
        {
            return EvaluationCase.ToList();
        }
    }
}
