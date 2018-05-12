using System;
using System.Linq;
using System.Collections.Generic;

namespace ElementsLib.Matrixus.Config
{
    using ConfigCode = UInt16;
    using ConfigBase = Module.Interfaces.Matrixus.IArticleConfigFeatures;
    using ConfigGang = UInt16;
    using ConfigType = UInt16;
    using ConfigBind = UInt16;
    using ConfigItem = Module.Interfaces.Matrixus.IArticleConfigDetail;
    using ConfigPair = KeyValuePair<UInt16, Module.Interfaces.Matrixus.IArticleConfigDetail>;
    using ConfigData = Module.Interfaces.Permadom.ArticleCodeConfigData;
    using ConfigSort = Int32;

    using SourceItem = Module.Interfaces.Elements.IArticleSource;
    using SourceVals = Module.Interfaces.Elements.ISourceValues;
    using SourceErrs = String;

    using Module.Libs;
    using Module.Codes;
    using Module.Interfaces.Matrixus;
    using Elements.Config;

    public class ArticleDetailCollection : GeneralConfigCollection<ConfigItem, ConfigCode>, IArticleDetailCollection
    {
        public ArticleDetailCollection() : base()
        {
            this.InternalRanks = new Dictionary<ConfigCode, ConfigSort>();

            this.InternalQueue = new Dictionary<ConfigCode, ArticleReferenceSort<ConfigGang, ConfigCode>>();
        }

        protected IDictionary<ConfigCode, ConfigSort> InternalRanks { get; set; }
        protected IDictionary<ConfigCode, ArticleReferenceSort<ConfigGang, ConfigCode>> InternalQueue { get; set; }

        public IDictionary<ConfigCode, ConfigSort> Ranks()
        {
            return InternalRanks.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public void LoadConfigData(IArticleMasterCollection masterStore, IEnumerable<ConfigData> configList, IArticleConfigFactory configFactory)
        {
            IEnumerable<ConfigPair> configTypeList = configList.Select((c) => 
                (new ConfigPair(c.Code, 
                    configFactory.CreateDetailItem(masterStore, c.Code, c.Name, 
                        c.Role, c.Gang, c.Type, c.Bind, 
                        c.TaxingType, c.HealthType, c.SocialType, c.Path)))).ToList();

            ConfigureModel(configTypeList);

            ConfigureModelDependency();
        }

        public ConfigItem FindArticleConfig(ConfigCode modelCode)
        {
            ConfigItem configModel = FindConfigByCode(modelCode);

            return configModel;
        }
        public ResultMonad.Result<SourceItem, SourceErrs> FindArticleSource(ConfigCode modelCode)
        {
            ConfigItem configModel = FindConfigByCode(modelCode);

            if (configModel == null)
            {
                return ResultMonad.Result.Fail<SourceItem, SourceErrs>("Config model doesn't exist!"); ;
            }
            if (configModel.DetailStub() == null)
            {
                return ResultMonad.Result.Fail<SourceItem, SourceErrs>("Config source doesn't exist!"); ;
            }
            return ResultMonad.Result.Ok<SourceItem, SourceErrs>(configModel.DetailStub());
        }
        public ResultMonad.Result<SourceItem, SourceErrs> CloneInstanceForCode(ConfigCode configCode, SourceVals sourceVals)
        {
            ResultMonad.Result<SourceItem, SourceErrs> emptyInstance = FindArticleSource(configCode);

            if (emptyInstance.IsFailure)
            {
                return emptyInstance;
            }
            return emptyInstance.Value.CloneSourceAndSetValues<SourceItem>(configCode, sourceVals);
        }

        protected void ConfigureModelDependency()
        {
            IDictionary<ConfigCode, ArticleReferenceSort<ConfigGang, ConfigCode>> resultsZero = new Dictionary<ConfigCode, ArticleReferenceSort<ConfigGang, ConfigCode>>();

            InternalQueue = InternalModels.Aggregate(resultsZero, (agr, c) => agr.Merge(c.Value.Code(), c.Value.Gang(), ResolveDependencyPath(agr, c.Value.Code(), c.Value.Path(), InternalModels)));

            IList<ConfigCode> TempKeys = InternalModels.Keys.ToList();

            IList<ConfigCode> SortKeys = TempKeys.OrderBy((x) => (x), new CompareConfigCode(InternalQueue)).ToList();

            IList<KeyValuePair<ConfigCode, ConfigSort>> SortPair = SortKeys.Select((k, i) => (new KeyValuePair<ConfigCode, Int32>(k, i))).ToList();

            InternalRanks = SortPair.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        protected ConfigCode[] ResolveDependencyPath(IDictionary<ConfigCode, ArticleReferenceSort<ConfigGang, ConfigCode>> resultsHead, ConfigCode articleCode, IEnumerable<ConfigCode> articlePath, IDictionary<ConfigCode, ConfigItem> articleTree)
        {
            IDictionary<ConfigCode, IEnumerable<ConfigCode>> resultsSink = new Dictionary<ConfigCode, IEnumerable<ConfigCode>>();

            ConfigCode[] articleSubs = new ConfigCode[0];

            IDictionary<ConfigCode, IEnumerable<ConfigCode>> articleIter = articlePath.Aggregate(resultsSink, (agr, c) => ResolveDependencyIter(resultsHead, c, articleSubs, agr, articleTree));

            ConfigCode[] resultsList = articleIter.SelectMany((c) => (c.Value.Merge(c.Key))).OrderBy(s => s).Distinct().ToArray();

            return resultsList.ToArray();
        }

        protected IDictionary<ConfigCode, IEnumerable<ConfigCode>> ResolveDependencyIter(IDictionary<ConfigCode, ArticleReferenceSort<ConfigGang, ConfigCode>> resultsHead, ConfigCode resolveCode, IEnumerable<ConfigCode> articlePath, IDictionary<ConfigCode, IEnumerable<ConfigCode>> resultsSink, IDictionary<ConfigCode, ConfigItem> articleTree)
        {
            if (articlePath.Contains(resolveCode))
            {
                // Error - cyclic dependency
                return resultsSink.Merge(resolveCode, new ConfigCode[0]);
            }

            ConfigCode[] articleSubs = articlePath.Merge(resolveCode).ToArray();

            ArticleReferenceSort<ConfigGang, ConfigCode> pathHead;
            bool foundHead = resultsHead.TryGetValue(resolveCode, out pathHead);

            if (foundHead && pathHead != null)
            {
                return resultsSink.Merge(resolveCode, pathHead.Path());
            }

            IEnumerable<ConfigCode> pathSink;
            bool foundSink = resultsSink.TryGetValue(resolveCode, out pathSink);

            if (foundSink && pathSink != null)
            {
                return resultsSink;
            }

            ConfigCode[] successQueue = ResolveSuccessQueue(resolveCode, articleTree);

            IDictionary<ConfigCode, IEnumerable<ConfigCode>> mergedSink = resultsSink.Merge(resolveCode, successQueue);

            IDictionary<ConfigCode, IEnumerable<ConfigCode>> returnSink = successQueue.Aggregate(mergedSink, (agr, c) => ResolveDependencyIter(resultsHead, c, articleSubs, agr, articleTree));

            return returnSink;
        }
        public ConfigType GetConfigType(ConfigCode configCode)
        {
            ConfigItem configItem = InternalModels.FirstOrDefault((c) => (c.Key == configCode)).Value;

            if (configItem == null)
            {
                return (ConfigType)ArticleType.NO_HEAD_PART_TYPE;
            }
            return configItem.Type();
        }
        public ConfigBind GetConfigBind(ConfigCode configCode)
        {
            ConfigItem configItem = InternalModels.FirstOrDefault((c) => (c.Key == configCode)).Value;

            if (configItem == null)
            {
                return (ConfigBind)ArticleBind.ARTICLE_OPT;
            }
            return configItem.Bind();
        }
        public IEnumerable<ConfigCode> GetSuccessQueue(ConfigCode configCode)
        {
            ArticleReferenceSort<ConfigGang, ConfigCode> successConfig = InternalQueue.FirstOrDefault((kvx) => (kvx.Key == configCode)).Value;

            return successConfig.Path().ToList();
        }
        protected ConfigCode[] ResolveSuccessQueue(ConfigCode resolveCode, IDictionary<ConfigCode, ConfigItem> articleTree)
        {
            ConfigItem articleItem;
            bool foundTree = articleTree.TryGetValue(resolveCode, out articleItem);

            if (foundTree == false || articleItem == null)
            {
                // Not Found in Config
                return new ConfigCode[0];
            }

            return articleItem.Path();
        }

        public string Description(IDictionary<ConfigCode, IEnumerable<ConfigCode>> articleSink)
        {
            return articleSink.Aggregate("", (agr, a) => (string.Format("{0}\n{1}{2}\n", agr, ArticleCodeAdapter.CreateEnum(a.Key).GetSymbol(),
                a.Value.Aggregate("", (bgr, b) => (string.Format("{0}\n={1}", bgr, ArticleCodeAdapter.CreateEnum(b).GetSymbol()))))));
        }
        private string DescriptionPath(IEnumerable<ConfigCode> articlePath)
        {
            return articlePath.Aggregate("", (agr, a) => (string.Format("{0}{1}\n", agr, ArticleCodeAdapter.CreateEnum(a).GetSymbol())));
        }
    }

    internal class CompareConfigCode : IComparer<ConfigCode>
    {
        private IDictionary<ConfigCode, ArticleReferenceSort<ConfigGang, ConfigCode>> ModelOrderDict;

        public CompareConfigCode(IDictionary<ConfigCode, ArticleReferenceSort<ConfigGang, ConfigCode>> modelOrderDict)
        {
            this.ModelOrderDict = modelOrderDict;
        }

        public int Compare(ushort x, ushort y)
        {
            if (x == y)
            {
                return 0;
            }

            ConfigGang xResolveGang = 0;
            IEnumerable<ConfigCode> xResolvePath;
            ArticleReferenceSort<ConfigGang, ConfigCode> xResolve;
            bool foundX = ModelOrderDict.TryGetValue(x, out xResolve);

            if (foundX == false || xResolve == null)
            {
                xResolveGang = 0;
                xResolvePath = new ConfigCode[0];
            }
            xResolveGang = xResolve.Gang();
            xResolvePath = xResolve.Path();

            ConfigGang yResolveGang = 0;
            IEnumerable<ConfigCode> yResolvePath;
            ArticleReferenceSort<ConfigGang, ConfigCode> yResolve;
            bool foundY = ModelOrderDict.TryGetValue(y, out yResolve);

            if (foundY == false || yResolve == null)
            {
                yResolveGang = 0;
                yResolvePath = new ConfigCode[0];
            }
            yResolveGang = yResolve.Gang();
            yResolvePath = yResolve.Path();

            bool xDependsOnY = xResolvePath.Contains(y);

            bool yDependsOnX = yResolvePath.Contains(x);

            if (xDependsOnY)
            {
                return 1;
            }

            if (yDependsOnX)
            {
                return -1;
            }

            if (xResolveGang != yResolveGang)
            {
                return xResolveGang.CompareTo(yResolveGang);
            }
            if (xResolvePath.Count() != yResolvePath.Count())
            {
                return xResolvePath.Count().CompareTo(yResolvePath.Count());
            }

            return x.CompareTo(y);
        }
    }
}
