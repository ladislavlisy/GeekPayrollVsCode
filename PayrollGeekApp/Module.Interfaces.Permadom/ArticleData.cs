using System;

namespace ElementsLib.Module.Interfaces.Permadom
{
    using TargetHead = UInt16;
    using TargetPart = UInt16;
    using ConfigCode = UInt16;
    using TargetSeed = UInt16;
    using SourceVals = Elements.ISourceValues;

    using ElementsLib.Module.Interfaces.Elements;

    public class ArticleData
    {
        public TargetHead Head { get; set; }
        public TargetPart Part { get; set; }
        public ConfigCode Code { get; set; }
        public TargetSeed Seed { get; set; }
        public SourceVals Tags { get; set; }

    }

}
