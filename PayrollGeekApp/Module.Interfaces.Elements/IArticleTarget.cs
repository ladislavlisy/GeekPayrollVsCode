using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Interfaces.Elements
{
    using TargetHead = UInt16;
    using TargetPart = UInt16;
    using ConfigCode = UInt16;
    using TargetSeed = UInt16;
    using TargetSort = UInt16;

    public interface IArticleTarget : IComparable<IArticleTarget>, IEquatable<IArticleTarget>
    {
        TargetHead Head();
        TargetPart Part();
        ConfigCode Code();
        TargetSeed Seed();
        bool IsEqualByHead(IArticleTarget other);
        bool IsEqualByHeadAndPart(IArticleTarget other);
        bool IsEqualByCodePlusHead(IArticleTarget other);
        bool IsEqualByCodePlusHeadAndPart(IArticleTarget other);
        bool IsEqualByCodePlusHead(ConfigCode otherCode, IArticleTarget other);
        bool IsEqualByCodePlusHeadAndPart(ConfigCode otherCode, IArticleTarget other);

        bool IsEqualByCode(ConfigCode otherCode);
        bool IsEqualByHead(TargetHead otherHead);
        bool IsEqualByHeadAndPart(TargetHead otherHead, TargetPart otherPart);
        bool IsEqualByCodePlusSeed(ConfigCode otherCode, TargetSeed otherSeed);
        bool IsEqualByCodePlusHead(ConfigCode otherCode, TargetHead otherHead);
        bool IsEqualByCodePlusHeadAndSeed(ConfigCode otherCode, TargetHead otherHead, TargetSeed otherSeed);
        bool IsEqualByCodePlusHeadAndPart(ConfigCode otherCode, TargetHead otherHead, TargetPart otherPart);
        string ToSymbolString<TENUM>() where TENUM : struct, IComparable;
    }
}
