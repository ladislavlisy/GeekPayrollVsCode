using System;

namespace ElementsLib.Elements
{
    using TargetHead = UInt16;
    using TargetPart = UInt16;
    using ConfigCode = UInt16;
    using TargetSeed = UInt16;
    using TargetSort = UInt16;

    using Module.Libs;
    using Module.Interfaces.Elements;

    public class ArticleTarget : IArticleTarget 
    {
        public const TargetHead HEAD_CODE_NULL = 0;
        public const TargetPart PART_CODE_NULL = 0;

        public const TargetSeed BODY_SEED_NULL = 0;
        public const TargetSeed BODY_SEED_FIRST = 1;

        protected TargetHead InternalHead { get; set; }
        protected TargetPart InternalPart { get; set; }
        protected ConfigCode InternalCode { get; set; }
        protected TargetSeed InternalSeed { get; set; }
        protected TargetSort InternalSort { get; set; }

        public TargetHead Head()
        {
            return InternalHead;
        }

        public TargetPart Part()
        {
            return InternalPart;
        }

        public ConfigCode Code()
        {
            return InternalCode;
        }

        public TargetSeed Seed()
        {
            return InternalSeed;
        }

        public ArticleTarget(TargetHead codeHead, TargetPart codePart, ConfigCode codeBody, TargetSeed seedBody)
        {
            this.InternalHead = codeHead;
            this.InternalPart = codePart;
            this.InternalCode = codeBody;
            this.InternalSeed = seedBody;
        }

        public int CompareTo(IArticleTarget other)
        {
            if (IsEqualToSame(other))
            {
                return 0;
            }
            else if (IsGreaterToSame(other))
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        private bool IsGreaterToSame(IArticleTarget other)
        {
            if (this.InternalHead != other.Head())
            {
                return (this.InternalHead > other.Head());
            }
            if (this.InternalPart != other.Part())
            {
                return (this.InternalPart > other.Part());
            }
            if (this.InternalCode != other.Code())
            {
                return (this.InternalCode > other.Code());
            }
            return (this.InternalSeed > other.Seed());
        }

        private bool IsSmallerToSame(IArticleTarget other)
        {
            if (this.InternalHead != other.Head())
            {
                return (this.InternalHead < other.Head());
            }
            if (this.InternalPart != other.Part())
            {
                return (this.InternalPart < other.Part());
            }
            if (this.InternalCode != other.Code())
            {
                return (this.InternalCode < other.Code());
            }
            return (this.InternalSeed < other.Seed());
        }

        private bool IsEqualToSame(IArticleTarget other)
        {
            return (this.InternalHead == other.Head() && this.InternalPart == other.Part() && this.InternalCode == other.Code() && this.InternalSeed == other.Seed());
        }
        public bool IsEqualByHead(IArticleTarget other)
        {
            return (this.InternalHead == other.Head());
        }
        public bool IsEqualByHeadAndPart(IArticleTarget other)
        {
            return (this.InternalHead == other.Head() && this.InternalPart == other.Part());
        }
        public bool IsEqualByCode(ConfigCode otherCode)
        {
            return (this.InternalCode == otherCode);
        }
        public bool IsEqualByHead(TargetHead otherHead)
        {
            return (this.InternalHead == otherHead);
        }
        public bool IsEqualByHeadAndPart(TargetHead otherHead, TargetPart otherPart)
        {
            return (this.InternalHead == otherHead && this.InternalPart == otherPart);
        }

        public bool IsEqualByCodePlusHead(ConfigCode otherCode, IArticleTarget other)
        {
            return IsEqualByCodePlusHead(otherCode, other.Head());
        }
        public bool IsEqualByCodePlusHead(IArticleTarget other)
        {
            return IsEqualByCodePlusHead(other.Code(), other.Head());
        }
        public bool IsEqualByCodePlusHeadAndPart(ConfigCode otherCode, IArticleTarget other)
        {
            return IsEqualByCodePlusHeadAndPart(otherCode, other.Head(), other.Part());
        }
        public bool IsEqualByCodePlusHeadAndPart(IArticleTarget other)
        {
            return IsEqualByCodePlusHeadAndPart(other.Code(), other.Head(), other.Part());
        }
        public bool IsEqualByCodePlusSeed(ConfigCode otherCode, TargetHead otherSeed)
        {
            return (this.InternalCode == otherCode && this.InternalSeed == otherSeed);
        }
        public bool IsEqualByCodePlusHead(ConfigCode otherCode, TargetHead otherHead)
        {
            return (this.InternalCode == otherCode && this.InternalHead == otherHead);
        }
        public bool IsEqualByCodePlusHeadAndSeed(ConfigCode otherCode, TargetHead otherHead, TargetPart otherSeed)
        {
            return (this.InternalCode == otherCode && this.InternalHead == otherHead && this.InternalSeed == otherSeed);
        }
        public bool IsEqualByCodePlusHeadAndPart(ConfigCode otherCode, TargetHead otherHead, TargetPart otherPart)
        {
            return (this.InternalCode == otherCode && this.InternalHead == otherHead && this.InternalPart == otherPart);
        }

        public static bool operator <(ArticleTarget x, ArticleTarget y)
        {
            return x.IsSmallerToSame(y);
        }

        public static bool operator >(ArticleTarget x, ArticleTarget y)
        {
            return x.IsGreaterToSame(y);
        }

        public static bool operator <=(ArticleTarget x, ArticleTarget y)
        {
            if (x.IsEqualToSame(y))
            {
                return true;
            }
            return x.IsSmallerToSame(y);
        }

        public static bool operator >=(ArticleTarget x, ArticleTarget y)
        {                                                                         
            if (x.IsEqualToSame(y))                               
            {
                return true;
            }
            return x.IsGreaterToSame(y);
        }


        public bool Equals(IArticleTarget other)
        {
            return this.IsEqualToSame(other);
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            ArticleTarget other = obj as ArticleTarget;

            return this.IsEqualToSame(other);
        }


        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;

            result += prime * result + (int)this.InternalPart;
            result += prime * result + (int)this.InternalHead;
            result += prime * result + (int)this.InternalCode;
            result += prime * result + (int)this.InternalSeed;

            return result;
        }

        public virtual object Clone()
        {
            ArticleTarget clone = (ArticleTarget)this.MemberwiseClone();
            return clone;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}-{3}", this.InternalHead.ToString(), this.InternalPart.ToString(), this.InternalCode.ToString(), this.InternalSeed.ToString());
        }

        public string ToSymbolString<TENUM>() where TENUM : struct, IComparable
        {
            TENUM codeEnum = this.InternalCode.ToEnum<TENUM>();

            return string.Format("{0}-{1}-{2}-{3}", this.InternalHead.ToString(), this.InternalPart.ToString(), codeEnum.ToString(), this.InternalSeed.ToString());
        }

    }
}
