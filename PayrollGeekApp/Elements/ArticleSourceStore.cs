using System;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using System.Linq;

namespace ElementsLib.Elements
{
    using ConfigCode = UInt16;
    using ConfigType = UInt16;
    using ConfigBind = UInt16;
    using TargetHead = UInt16;
    using TargetPart = UInt16;
    using TargetSeed = UInt16;
    using TargetSort = UInt16;
    using ConfigSort = Int32;

    using SourceCase = Module.Interfaces.Matrixus.IArticleConfigProfile;
    using TargetItem = Module.Interfaces.Elements.IArticleTarget;
    using TargetData = Module.Interfaces.Permadom.ArticleData;

    using SortedPair = KeyValuePair<UInt16, Int32>;

    using SourcePair = KeyValuePair<Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>>;
    using SourcePack = ResultMonad.Result<Module.Interfaces.Elements.IArticleSource, string>;

    using Module.Interfaces.Elements;
    using Libs;
    using ResultMonad;
    using Module.Codes;

    public class ArticleSourceStore : IArticleSourceStore
    {
        public static string EXCEPTION_CONFIG_NULL_TEXT = "Config Collection doesn't exist!";

        SourceCase ModelSourceProfile { get; set; }

        #region IENUMERATOR_SOURCE_MODEL
        protected IDictionary<TargetItem, SourcePack> model;

        public IEnumerator<KeyValuePair<TargetItem, SourcePack>> GetEnumerator()
        {
            return model.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return model.GetEnumerator();
        }
        public ICollection<TargetItem> Keys()
        {
            return model.Keys;
        }
        public IEnumerable<TargetItem> GetTargets()
        {
            return model.Keys.ToList();
        }

        public IEnumerable<KeyValuePair<TargetItem, SourcePack>> GetModel()
        {
            return model.ToList();
        }

        #endregion

        public ArticleSourceStore(SourceCase configProfile)
        {
            model = new Dictionary<TargetItem, SourcePack>();

            ModelSourceProfile = configProfile;
        }

        public void LoadSourceData(IEnumerable<TargetData> sourceData)
        {
            List<TargetData> sourceList = sourceData.ToList();

            sourceList.ForEach((s) => StoreGeneralItem(s));
        }

        public void CopyModel(IArticleSourceStore source)
        {
            model = source.GetModel().ToDictionary((kv) => (kv.Key), (kv) => (kv.Value));
        }

        private bool ExistTargetByHeadAndPartAndCode(TargetItem targetItem)
        {
            return (Keys().FirstOrDefault((s) => (s.IsEqualByCodePlusHeadAndPart(targetItem))) != null);
        }

        private IEnumerable<TargetHead> SelectContractCode(ConfigCode contractCode)
        {
            IEnumerable<TargetItem> targetsInit = GetTargets();

            var contractsHead = targetsInit.Where((ch) => (ch.Code() == contractCode)).Select((cv) => (cv.Seed()));

            return contractsHead;
        }
        private IEnumerable<Tuple<TargetHead, TargetPart>> SelectPositionCode(ConfigCode positionCode)
        {
            IEnumerable<TargetItem> targetsInit = GetTargets();

            var positionsPart = targetsInit.Where((ch) => (ch.Code() == positionCode)).Select((cv) => new Tuple<TargetHead, TargetPart>(cv.Head(), cv.Seed()));

            return positionsPart;
        }
        private IEnumerable<TargetItem> GetEvolvedTargets(ConfigCode contractCode, ConfigCode positionCode)
        {
            IEnumerable<IArticleTarget> targetsZero = new List<IArticleTarget>();

            var contractsHead = SelectContractCode(contractCode);

            var positionsPart = SelectPositionCode(positionCode);
            
            IEnumerable<TargetItem> targetsInit = GetTargets();

            IEnumerable<IArticleTarget> targetsCalc = targetsInit.Aggregate(targetsZero, 
                (agr, d) => agr.Concat(ResolveTargets(d, contractsHead, positionsPart)));

            return targetsCalc.Distinct();
        }
        private IEnumerable<TargetItem> ResolveTargets(TargetItem target, IEnumerable<TargetHead> heads, IEnumerable<Tuple<TargetHead, TargetPart>> parts)
        {
            IEnumerable<ConfigCode> successQueue = ModelSourceProfile.GetSuccessQueue(target.Code());

            IEnumerable<IArticleTarget> targetsQueue = successQueue.SelectMany((c) => (CreateTargetsQueue(c, target, heads, parts))).ToList();

            return targetsQueue.Where((c) => (c.Code() != 0));
        }

        private IEnumerable<TargetItem> CreateTargetsQueue(ConfigCode code, TargetItem target, IEnumerable<TargetHead> heads, IEnumerable<Tuple<TargetHead, TargetPart>> parts)
        {
            IEnumerable<ArticleTarget> targetList = new List<ArticleTarget>();

            ConfigType targetType = ModelSourceProfile.GetConfigType(code);
            ConfigBind targetBind = ModelSourceProfile.GetConfigBind(code);

            TargetHead codeHead = 0;
            TargetPart codePart = 0;
            ConfigCode codeBody = code;
            TargetSeed seedBody = 0;

            if (targetBind == (ConfigBind)ArticleBind.ARTICLE_OPT)
            {
                return targetList;
            }
            if (targetType == (ConfigType)ArticleType.NO_HEAD_PART_TYPE)
            {
                targetList = new List<ArticleTarget>() { new ArticleTarget(codeHead, codePart, codeBody, seedBody) };
            }
            if (targetType == (ConfigType)ArticleType.HEAD_CODE_ARTICLE)
            {
                if (target.Head() != 0)
                {
                    codeHead = target.Head();
                    targetList = new List<ArticleTarget>() { new ArticleTarget(codeHead, codePart, codeBody, seedBody) };
                }
                else
                {
                    targetList = heads.Select((ch) => (new ArticleTarget(ch, codePart, codeBody, seedBody))).ToList();
                }
            }
            else if (targetType == (ConfigType)ArticleType.PART_CODE_ARTICLE)
            {
                if (target.Head() != 0 && target.Part() != 0)
                {
                    codeHead = target.Head();
                    codePart = target.Part();
                    targetList = new List<ArticleTarget>() { new ArticleTarget(codeHead, codePart, codeBody, seedBody) };
                }
                else
                {
                    targetList = parts.Select((pp) => (new ArticleTarget(pp.Item1, pp.Item2, codeBody, seedBody))).ToList();
                }
            }

            return targetList;
        }

        public void EvolveStream(ConfigCode contractCode, ConfigCode positionCode)
        {
            IEnumerable<TargetItem> targetsInit = GetEvolvedTargets(contractCode, positionCode);

            AddGeneralItems(targetsInit);
        }

        public IList<SourcePair> GetEvaluationPath()
        {
            IDictionary<ConfigCode, ConfigSort> compareDict = ModelSourceProfile.ArticleRanks();

            IEnumerable<TargetItem> targets = Keys();

            IEnumerable<TargetItem> sortedTargets = targets.OrderBy((x) => (x), new CompareEvaluationTargets(compareDict)).ToList();

            return sortedTargets.Select((s) => (model.SingleOrDefault((kv) => (kv.Key.CompareTo(s) == 0)))).ToList();
        }

        public void AddGeneralItems(IEnumerable<TargetItem> targets)
        {
            var keysToAdd = targets.Where((s) => (ExistTargetByHeadAndPartAndCode(s)==false)).ToList();

            keysToAdd.ForEach((a) => AddGeneralItem(a.Head(), a.Part(), a.Code(), a.Seed(), null));
        }

        public ConfigCode GetHeadConfigCode()
        {
            return Module.Codes.ArticleCodeAdapter.GetContractCode();
        }

        public TargetItem AddMainHead(ISourceValues tagsBody)
        {
            TargetHead HEAD_CODE = ArticleTarget.HEAD_CODE_NULL;
            TargetPart PART_CODE = ArticleTarget.PART_CODE_NULL;
            ConfigCode BODY_CODE = GetHeadConfigCode();

            return AddGeneralItem(HEAD_CODE, PART_CODE, BODY_CODE, ArticleTarget.BODY_SEED_NULL, tagsBody);
        }

        public ConfigCode GetPartConfigCode()
        {
            return Module.Codes.ArticleCodeAdapter.GetPositionCode();
        }

        public TargetItem AddMainPart(TargetHead codeHead, ISourceValues tagsBody)
        {
            TargetPart PART_CODE = ArticleTarget.PART_CODE_NULL;
            ConfigCode BODY_CODE = GetPartConfigCode();

            return AddGeneralItem(codeHead, PART_CODE, BODY_CODE, ArticleTarget.BODY_SEED_NULL, tagsBody);
        }

        public TargetItem AddHeadItem(TargetHead codeHead, ConfigCode codeBody, ISourceValues tagsBody)
        {
            TargetPart PART_CODE = ArticleTarget.PART_CODE_NULL;

            return AddGeneralItem(codeHead, PART_CODE, codeBody, ArticleTarget.BODY_SEED_NULL, tagsBody);
        }
        public TargetItem AddPartItem(TargetHead codeHead, TargetPart codePart, ConfigCode codeBody, ISourceValues tagsBody)
        {
            return AddGeneralItem(codeHead, codePart, codeBody, ArticleTarget.BODY_SEED_NULL, tagsBody);
        }
        public TargetItem AddGeneralItem(TargetItem target, ISourceValues tagsBody)
        {
            return AddGeneralItem(target.Head(), target.Part(), target.Code(), target.Seed(), tagsBody);
        }
        public TargetItem AddGeneralItem(TargetHead codeHead, TargetPart codePart, ConfigCode codeBody, TargetSeed seedBody, ISourceValues tagsBody)
        {
            TargetSeed newTargetSeed = TargetSelector.GetSeedToNewTarget(model.Keys, codeHead, codePart, codeBody);

            return StoreGeneralItem(codeHead, codePart, codeBody, newTargetSeed, tagsBody);
        }
        public TargetItem StoreGeneralItem(TargetItem target, ISourceValues tagsBody)
        {
            return StoreGeneralItem(target.Head(), target.Part(), target.Code(), target.Seed(), tagsBody);
        }
        public TargetItem StoreGeneralItem(TargetHead codeHead, TargetPart codePart, ConfigCode codeBody, TargetSeed seedBody, ISourceValues tagsBody)
        {
            ArticleTarget newTarget = new ArticleTarget(codeHead, codePart, codeBody, seedBody);

            SourcePack newSource = GetTemplateSourceForArticle(codeBody, tagsBody);

            model.Add(newTarget, newSource);

            return newTarget;
        }
        public TargetItem StoreGeneralItem(TargetData dataItem)
        {
            ArticleTarget newTarget = new ArticleTarget(dataItem.Head, dataItem.Part, dataItem.Code, dataItem.Seed);

            SourcePack newSource = GetTemplateSourceForArticle(dataItem.Code, dataItem.Tags);

            model.Add(newTarget, newSource);

            return newTarget;
        }
        protected SourcePack GetTemplateSourceForArticle(ConfigCode codeBody, ISourceValues tagsBody)
        {
            if (ModelSourceProfile == null)
            {
                return Result.Fail<IArticleSource, string>(EXCEPTION_CONFIG_NULL_TEXT);
            }
            return ModelSourceProfile.CloneInstanceForCode(codeBody, tagsBody);
        }
    }

    internal class CompareEvaluationTargets : IComparer<TargetItem>
    {
        private IDictionary<ConfigCode, ConfigSort> ModelOrderList;

        public CompareEvaluationTargets(IDictionary<ConfigCode, ConfigSort> modelOrderList)
        {
            this.ModelOrderList = modelOrderList;
        }

        public int Compare(TargetItem x, TargetItem y)
        {
            if (x == y)
            {
                return 0;
            }

            SortedPair xResolve = ModelOrderList.SingleOrDefault((xk) => (xk.Key == x.Code()));

            Int32 xResolverOrder = 0;
            if (xResolve.Key == x.Code())
            {
                xResolverOrder = xResolve.Value;
            }

            SortedPair yResolve = ModelOrderList.SingleOrDefault((yk) => (yk.Key == y.Code()));

            Int32 yResolverOrder = 0;
            if (yResolve.Key == y.Code())
            {
                yResolverOrder = yResolve.Value;
            }

            int compareCode = xResolverOrder.CompareTo(yResolverOrder);
            if (compareCode != 0)
            {
                return compareCode;
            }
            compareCode = x.Head().CompareTo(y.Head());
            if (compareCode != 0)
            {
                return compareCode;
            }
            compareCode = x.Part().CompareTo(y.Part());
            if (compareCode != 0)
            {
                return compareCode;
            }
            compareCode = x.Seed().CompareTo(y.Seed());
            return compareCode;
        }
    }
}
