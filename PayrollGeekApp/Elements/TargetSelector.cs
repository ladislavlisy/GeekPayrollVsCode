using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Elements.Libs
{
    using TargetHead = UInt16;
    using TargetPart = UInt16;
    using ConfigCode = UInt16;
    using TargetSeed = UInt16;

    using Module.Interfaces.Elements;

    public static class TargetSelector
    {
        static public TargetSeed GetFirstTargetSeed(IEnumerable<IArticleTarget> targetList, TargetHead codeHead, TargetPart codePart, ConfigCode codeBody)
        {
            IEnumerable<IArticleTarget> selectedTargets = SelectEquals(targetList, codeHead, codePart, codeBody);

            IEnumerable<TargetSeed> oneCodeSeeds = ExtractCodeSeed(selectedTargets);

            return FirstSeedFromList(oneCodeSeeds.OrderBy(x => x).ToArray());
        }

        static public TargetSeed GetSeedToNewTarget(IEnumerable<IArticleTarget> targetList, TargetHead codeHead, TargetPart codePart, ConfigCode codeBody)
        {
            IEnumerable<IArticleTarget> selectedTargets = SelectEquals(targetList, codeHead, codePart, codeBody);

            IEnumerable<TargetSeed> oneCodeSeeds = ExtractCodeSeed(selectedTargets);

            return NewSeqSeedFromList(oneCodeSeeds.OrderBy(x => x).ToArray());
        }

        static private IEnumerable<IArticleTarget> SelectEquals(IEnumerable<IArticleTarget> targetList, TargetHead codeHead, TargetPart codePart, ConfigCode codeBody)
        {
            return targetList.Where(x => (EqualitySelector(x, codeHead, codePart, codeBody))).ToList();
        }

        static private IEnumerable<TargetSeed> ExtractCodeSeed(IEnumerable<IArticleTarget> selectedTargets)
        {
            return selectedTargets.Select(x => x.Seed()).ToList();
        }

        static private TargetSeed FirstSeedFromList(IEnumerable<TargetSeed> selectedSeeds)
        {
            TargetSeed firstSeed = selectedSeeds.DefaultIfEmpty(ArticleTarget.BODY_SEED_FIRST).First();

            return firstSeed;
        }

        static private TargetSeed NewSeqSeedFromList(IEnumerable<TargetSeed> selectedSeeds)
        {
            TargetSeed lastSeed = selectedSeeds.Aggregate(ArticleTarget.BODY_SEED_NULL, (agr, x) => (((x > agr) && (x - agr) > 1) ? agr : x));

            return (TargetSeed)(lastSeed + 1);
        }

        public static bool EqualitySelector(IArticleTarget target, TargetHead codeHead, TargetPart codePart, ConfigCode codeBody)
        {
            return (target.Head() == codeHead && target.Part() == codePart && target.Code() == codeBody);
        }

    }
}
