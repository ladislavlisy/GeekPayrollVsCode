using System;

namespace ElementsLib.Legalist.Constants
{
    using CONSTANTS_CODE = UInt16;

    public enum TaxingBehaviour : CONSTANTS_CODE
    {
        TAXING_NOTHING = 0,
        TAXING_EXCLUDE = 1,
        TAXING_ADVANCE = 2,
        TAXING_WITHHOLD = 3,
        TAXING_PARTNER = 4,
    }
    public enum TaxingPartnerIncome : CONSTANTS_CODE
    {
        TAXING_ADVANCE = 0,
        NONSIGNED_WITHHOLD = 1,
        TAXING_WITHHOLD = 2,
    }

    public static class TaxStatement
    {
        public static Byte TAXABLE = 1;
        public static Byte NONTAXABLE = 0;
    }
    public static class TaxDeclaracy
    {
        public static Byte SIGNED = 1;
        public static Byte NONSIGNED = 0;
    }
}
