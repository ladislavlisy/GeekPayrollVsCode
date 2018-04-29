using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Config
{
    using BundleVersion = UInt16;
    internal static class TaxingPropertiesDefault
    {
        public const BundleVersion VERSION_MIN = 2000;

        public const Int32 ALLOWANCE_PAYER = 0;
        public const Int32 ALLOWANCE_DISAB_1ST = 0;
        public const Int32 ALLOWANCE_DISAB_2ND = 0;
        public const Int32 ALLOWANCE_DISAB_3RD = 0;
        public const Int32 ALLOWANCE_STUDY = 0;
        public const Int32 ALLOWANCE_CHILD_1ST = 0;
        public const Int32 ALLOWANCE_CHILD_2ND = 0;
        public const Int32 ALLOWANCE_CHILD_3RD = 0;
        public const decimal FACTOR_ADVANCES = 0m;
        public const decimal FACTOR_WITHHOLD = 0m;
        public const decimal FACTOR_SOLIDARY = 0m;
        public const Int32 MIN_VALID_AMOUNT_OF_TAXBONUS = 0;
        public const Int32 MAX_VALID_AMOUNT_OF_TAXBONUS = 0;
        public const Int32 MIN_VALID_INCOME_OF_TAXBONUS = 0;
        public const Int32 MAX_VALID_INCOME_OF_ROUNDING = 0;
        public const Int32 MAX_VALID_INCOME_OF_WITHHOLD = 0;
        public const Int32 MIN_VALID_INCOME_OF_SOLIDARY = 0;
    }
    public static class TaxingPropertiesVersion2011
    {
        public const BundleVersion VERSION_MIN = 2011;

        public const Int32 ALLOWANCE_PAYER = 1970;
        public const Int32 ALLOWANCE_DISAB_1ST = 210;
        public const Int32 ALLOWANCE_DISAB_2ND = 420;
        public const Int32 ALLOWANCE_DISAB_3RD = 1345;
        public const Int32 ALLOWANCE_STUDY = 335;
        public const Int32 ALLOWANCE_CHILD_1ST = 1117;
        public const Int32 ALLOWANCE_CHILD_2ND = 1117;
        public const Int32 ALLOWANCE_CHILD_3RD = 1117;
        public const decimal FACTOR_ADVANCES = 15.0m;
        public const decimal FACTOR_WITHHOLD = 15.0m;
        public const decimal FACTOR_SOLIDARY = 0.0m;
        public const Int32 MIN_VALID_AMOUNT_OF_TAXBONUS = 50;
        public const Int32 MAX_VALID_AMOUNT_OF_TAXBONUS = 5025;
        public const Int32 MIN_VALID_INCOME_OF_TAXBONUS = 8000;
        public const Int32 MAX_VALID_INCOME_OF_ROUNDING = 100;
        public const Int32 MAX_VALID_INCOME_OF_WITHHOLD = 5000;
        public const Int32 MIN_VALID_INCOME_OF_SOLIDARY = 0;
    }

    public static class TaxingPropertiesVersion2012
    {
        public const BundleVersion VERSION_MIN = 2012;

        public const Int32 ALLOWANCE_PAYER = 2070;
        public const Int32 ALLOWANCE_DISAB_1ST = TaxingPropertiesVersion2011.ALLOWANCE_DISAB_1ST;
        public const Int32 ALLOWANCE_DISAB_2ND = TaxingPropertiesVersion2011.ALLOWANCE_DISAB_2ND;
        public const Int32 ALLOWANCE_DISAB_3RD = TaxingPropertiesVersion2011.ALLOWANCE_DISAB_3RD;
        public const Int32 ALLOWANCE_STUDY = TaxingPropertiesVersion2011.ALLOWANCE_STUDY;
        public const Int32 ALLOWANCE_CHILD_1ST = TaxingPropertiesVersion2011.ALLOWANCE_CHILD_1ST;
        public const Int32 ALLOWANCE_CHILD_2ND = TaxingPropertiesVersion2011.ALLOWANCE_CHILD_2ND;
        public const Int32 ALLOWANCE_CHILD_3RD = TaxingPropertiesVersion2011.ALLOWANCE_CHILD_3RD;
        public const decimal FACTOR_ADVANCES = TaxingPropertiesVersion2011.FACTOR_ADVANCES;
        public const decimal FACTOR_WITHHOLD = TaxingPropertiesVersion2011.FACTOR_WITHHOLD;
        public const decimal FACTOR_SOLIDARY = TaxingPropertiesVersion2011.FACTOR_SOLIDARY;
        public const Int32 MIN_VALID_AMOUNT_OF_TAXBONUS = TaxingPropertiesVersion2011.MIN_VALID_AMOUNT_OF_TAXBONUS;
        public const Int32 MAX_VALID_AMOUNT_OF_TAXBONUS = TaxingPropertiesVersion2011.MAX_VALID_AMOUNT_OF_TAXBONUS;
        public const Int32 MIN_VALID_INCOME_OF_TAXBONUS = TaxingPropertiesVersion2011.MIN_VALID_INCOME_OF_TAXBONUS;
        public const Int32 MAX_VALID_INCOME_OF_ROUNDING = TaxingPropertiesVersion2011.MAX_VALID_INCOME_OF_ROUNDING;
        public const Int32 MAX_VALID_INCOME_OF_WITHHOLD = TaxingPropertiesVersion2011.MAX_VALID_INCOME_OF_WITHHOLD;
        public const Int32 MIN_VALID_INCOME_OF_SOLIDARY = TaxingPropertiesVersion2011.MIN_VALID_INCOME_OF_SOLIDARY;
    }

    public static class TaxingPropertiesVersion2013
    {
        public const BundleVersion VERSION_MIN = 2013;

        public const Int32 ALLOWANCE_PAYER = TaxingPropertiesVersion2012.ALLOWANCE_PAYER;
        public const Int32 ALLOWANCE_DISAB_1ST = TaxingPropertiesVersion2012.ALLOWANCE_DISAB_1ST;
        public const Int32 ALLOWANCE_DISAB_2ND = TaxingPropertiesVersion2012.ALLOWANCE_DISAB_2ND;
        public const Int32 ALLOWANCE_DISAB_3RD = TaxingPropertiesVersion2012.ALLOWANCE_DISAB_3RD;
        public const Int32 ALLOWANCE_STUDY = TaxingPropertiesVersion2012.ALLOWANCE_STUDY;
        public const Int32 ALLOWANCE_CHILD_1ST = TaxingPropertiesVersion2012.ALLOWANCE_CHILD_1ST;
        public const Int32 ALLOWANCE_CHILD_2ND = TaxingPropertiesVersion2012.ALLOWANCE_CHILD_2ND;
        public const Int32 ALLOWANCE_CHILD_3RD = TaxingPropertiesVersion2012.ALLOWANCE_CHILD_3RD;
        public const decimal FACTOR_ADVANCES = TaxingPropertiesVersion2012.FACTOR_ADVANCES;
        public const decimal FACTOR_WITHHOLD = TaxingPropertiesVersion2012.FACTOR_WITHHOLD;
        public const decimal FACTOR_SOLIDARY = 7.0m;
        public const Int32 MIN_VALID_AMOUNT_OF_TAXBONUS = TaxingPropertiesVersion2012.MIN_VALID_AMOUNT_OF_TAXBONUS;
        public const Int32 MAX_VALID_AMOUNT_OF_TAXBONUS = TaxingPropertiesVersion2012.MAX_VALID_AMOUNT_OF_TAXBONUS;
        public const Int32 MIN_VALID_INCOME_OF_TAXBONUS = TaxingPropertiesVersion2012.MIN_VALID_INCOME_OF_TAXBONUS;
        public const Int32 MAX_VALID_INCOME_OF_ROUNDING = TaxingPropertiesVersion2012.MAX_VALID_INCOME_OF_ROUNDING;
        public const Int32 MAX_VALID_INCOME_OF_WITHHOLD = TaxingPropertiesVersion2012.MAX_VALID_INCOME_OF_WITHHOLD;
        public const Int32 MIN_VALID_INCOME_OF_SOLIDARY = 103536;
    }

    public static class TaxingPropertiesVersion2014
    {
        public const BundleVersion VERSION_MIN = 2014;

        public const Int32 ALLOWANCE_PAYER = TaxingPropertiesVersion2013.ALLOWANCE_PAYER;
        public const Int32 ALLOWANCE_DISAB_1ST = TaxingPropertiesVersion2013.ALLOWANCE_DISAB_1ST;
        public const Int32 ALLOWANCE_DISAB_2ND = TaxingPropertiesVersion2013.ALLOWANCE_DISAB_2ND;
        public const Int32 ALLOWANCE_DISAB_3RD = TaxingPropertiesVersion2013.ALLOWANCE_DISAB_3RD;
        public const Int32 ALLOWANCE_STUDY = TaxingPropertiesVersion2013.ALLOWANCE_STUDY;
        public const Int32 ALLOWANCE_CHILD_1ST = TaxingPropertiesVersion2013.ALLOWANCE_CHILD_1ST;
        public const Int32 ALLOWANCE_CHILD_2ND = TaxingPropertiesVersion2013.ALLOWANCE_CHILD_2ND;
        public const Int32 ALLOWANCE_CHILD_3RD = TaxingPropertiesVersion2013.ALLOWANCE_CHILD_3RD;
        public const decimal FACTOR_ADVANCES = TaxingPropertiesVersion2013.FACTOR_ADVANCES;
        public const decimal FACTOR_WITHHOLD = TaxingPropertiesVersion2013.FACTOR_WITHHOLD;
        public const decimal FACTOR_SOLIDARY = TaxingPropertiesVersion2013.FACTOR_SOLIDARY;
        public const Int32 MIN_VALID_AMOUNT_OF_TAXBONUS = TaxingPropertiesVersion2013.MIN_VALID_AMOUNT_OF_TAXBONUS;
        public const Int32 MAX_VALID_AMOUNT_OF_TAXBONUS = TaxingPropertiesVersion2013.MAX_VALID_AMOUNT_OF_TAXBONUS;
        public const Int32 MIN_VALID_INCOME_OF_TAXBONUS = 8500;
        public const Int32 MAX_VALID_INCOME_OF_ROUNDING = TaxingPropertiesVersion2013.MAX_VALID_INCOME_OF_ROUNDING;
        public const Int32 MAX_VALID_INCOME_OF_WITHHOLD = 10000;
        public const Int32 MIN_VALID_INCOME_OF_SOLIDARY = 103768;
    }

    public static class TaxingPropertiesVersion2015
    {
        public const BundleVersion VERSION_MIN = 2015;

        public const Int32 ALLOWANCE_PAYER = TaxingPropertiesVersion2014.ALLOWANCE_PAYER;
        public const Int32 ALLOWANCE_DISAB_1ST = TaxingPropertiesVersion2014.ALLOWANCE_DISAB_1ST;
        public const Int32 ALLOWANCE_DISAB_2ND = TaxingPropertiesVersion2014.ALLOWANCE_DISAB_2ND;
        public const Int32 ALLOWANCE_DISAB_3RD = TaxingPropertiesVersion2014.ALLOWANCE_DISAB_3RD;
        public const Int32 ALLOWANCE_STUDY = TaxingPropertiesVersion2014.ALLOWANCE_STUDY;
        public const Int32 ALLOWANCE_CHILD_1ST = TaxingPropertiesVersion2014.ALLOWANCE_CHILD_1ST;
        public const Int32 ALLOWANCE_CHILD_2ND = 1317;
        public const Int32 ALLOWANCE_CHILD_3RD = 1417;
        public const decimal FACTOR_ADVANCES = TaxingPropertiesVersion2014.FACTOR_ADVANCES;
        public const decimal FACTOR_WITHHOLD = TaxingPropertiesVersion2014.FACTOR_WITHHOLD;
        public const decimal FACTOR_SOLIDARY = TaxingPropertiesVersion2014.FACTOR_SOLIDARY;
        public const Int32 MIN_VALID_AMOUNT_OF_TAXBONUS = TaxingPropertiesVersion2014.MIN_VALID_AMOUNT_OF_TAXBONUS;
        public const Int32 MAX_VALID_AMOUNT_OF_TAXBONUS = TaxingPropertiesVersion2014.MAX_VALID_AMOUNT_OF_TAXBONUS;
        public const Int32 MIN_VALID_INCOME_OF_TAXBONUS = 9200;
        public const Int32 MAX_VALID_INCOME_OF_ROUNDING = TaxingPropertiesVersion2014.MAX_VALID_INCOME_OF_ROUNDING;
        public const Int32 MAX_VALID_INCOME_OF_WITHHOLD = TaxingPropertiesVersion2014.MAX_VALID_INCOME_OF_WITHHOLD;
        public const Int32 MIN_VALID_INCOME_OF_SOLIDARY = 106444;
    }
    public static class TaxingPropertiesVersion2016
    {
        public const BundleVersion VERSION_MIN = 2016;

        public const Int32 ALLOWANCE_PAYER = TaxingPropertiesVersion2015.ALLOWANCE_PAYER;
        public const Int32 ALLOWANCE_DISAB_1ST = TaxingPropertiesVersion2015.ALLOWANCE_DISAB_1ST;
        public const Int32 ALLOWANCE_DISAB_2ND = TaxingPropertiesVersion2015.ALLOWANCE_DISAB_2ND;
        public const Int32 ALLOWANCE_DISAB_3RD = TaxingPropertiesVersion2015.ALLOWANCE_DISAB_3RD;
        public const Int32 ALLOWANCE_STUDY = TaxingPropertiesVersion2015.ALLOWANCE_STUDY;
        public const Int32 ALLOWANCE_CHILD_1ST = TaxingPropertiesVersion2015.ALLOWANCE_CHILD_1ST;
        public const Int32 ALLOWANCE_CHILD_2ND = 1317;
        public const Int32 ALLOWANCE_CHILD_3RD = 1417;
        public const decimal FACTOR_ADVANCES = TaxingPropertiesVersion2015.FACTOR_ADVANCES;
        public const decimal FACTOR_WITHHOLD = TaxingPropertiesVersion2015.FACTOR_WITHHOLD;
        public const decimal FACTOR_SOLIDARY = TaxingPropertiesVersion2015.FACTOR_SOLIDARY;
        public const Int32 MIN_VALID_AMOUNT_OF_TAXBONUS = TaxingPropertiesVersion2015.MIN_VALID_AMOUNT_OF_TAXBONUS;
        public const Int32 MAX_VALID_AMOUNT_OF_TAXBONUS = TaxingPropertiesVersion2015.MAX_VALID_AMOUNT_OF_TAXBONUS;
        public const Int32 MIN_VALID_INCOME_OF_TAXBONUS = 9200;
        public const Int32 MAX_VALID_INCOME_OF_ROUNDING = TaxingPropertiesVersion2015.MAX_VALID_INCOME_OF_ROUNDING;
        public const Int32 MAX_VALID_INCOME_OF_WITHHOLD = TaxingPropertiesVersion2015.MAX_VALID_INCOME_OF_WITHHOLD;
        public const Int32 MIN_VALID_INCOME_OF_SOLIDARY = 106444;
    }
    public static class TaxingPropertiesVersion2017
    {
        public const BundleVersion VERSION_MIN = 2017;

        public const Int32 ALLOWANCE_PAYER = TaxingPropertiesVersion2016.ALLOWANCE_PAYER;
        public const Int32 ALLOWANCE_DISAB_1ST = TaxingPropertiesVersion2016.ALLOWANCE_DISAB_1ST;
        public const Int32 ALLOWANCE_DISAB_2ND = TaxingPropertiesVersion2016.ALLOWANCE_DISAB_2ND;
        public const Int32 ALLOWANCE_DISAB_3RD = TaxingPropertiesVersion2016.ALLOWANCE_DISAB_3RD;
        public const Int32 ALLOWANCE_STUDY = TaxingPropertiesVersion2016.ALLOWANCE_STUDY;
        public const Int32 ALLOWANCE_CHILD_1ST = TaxingPropertiesVersion2016.ALLOWANCE_CHILD_1ST;
        public const Int32 ALLOWANCE_CHILD_2ND = 1317;
        public const Int32 ALLOWANCE_CHILD_3RD = 1417;
        public const decimal FACTOR_ADVANCES = TaxingPropertiesVersion2016.FACTOR_ADVANCES;
        public const decimal FACTOR_WITHHOLD = TaxingPropertiesVersion2016.FACTOR_WITHHOLD;
        public const decimal FACTOR_SOLIDARY = TaxingPropertiesVersion2016.FACTOR_SOLIDARY;
        public const Int32 MIN_VALID_AMOUNT_OF_TAXBONUS = TaxingPropertiesVersion2016.MIN_VALID_AMOUNT_OF_TAXBONUS;
        public const Int32 MAX_VALID_AMOUNT_OF_TAXBONUS = TaxingPropertiesVersion2016.MAX_VALID_AMOUNT_OF_TAXBONUS;
        public const Int32 MIN_VALID_INCOME_OF_TAXBONUS = 9200;
        public const Int32 MAX_VALID_INCOME_OF_ROUNDING = TaxingPropertiesVersion2016.MAX_VALID_INCOME_OF_ROUNDING;
        public const Int32 MAX_VALID_INCOME_OF_WITHHOLD = TaxingPropertiesVersion2016.MAX_VALID_INCOME_OF_WITHHOLD;
        public const Int32 MIN_VALID_INCOME_OF_SOLIDARY = 106444;
    }
    public static class TaxingPropertiesVersion2018
    {
        public const BundleVersion VERSION_MIN = 2018;

        public const Int32 ALLOWANCE_PAYER = TaxingPropertiesVersion2017.ALLOWANCE_PAYER;
        public const Int32 ALLOWANCE_DISAB_1ST = TaxingPropertiesVersion2017.ALLOWANCE_DISAB_1ST;
        public const Int32 ALLOWANCE_DISAB_2ND = TaxingPropertiesVersion2017.ALLOWANCE_DISAB_2ND;
        public const Int32 ALLOWANCE_DISAB_3RD = TaxingPropertiesVersion2017.ALLOWANCE_DISAB_3RD;
        public const Int32 ALLOWANCE_STUDY = TaxingPropertiesVersion2017.ALLOWANCE_STUDY;
        public const Int32 ALLOWANCE_CHILD_1ST = TaxingPropertiesVersion2017.ALLOWANCE_CHILD_1ST;
        public const Int32 ALLOWANCE_CHILD_2ND = 1317;
        public const Int32 ALLOWANCE_CHILD_3RD = 1417;
        public const decimal FACTOR_ADVANCES = TaxingPropertiesVersion2017.FACTOR_ADVANCES;
        public const decimal FACTOR_WITHHOLD = TaxingPropertiesVersion2017.FACTOR_WITHHOLD;
        public const decimal FACTOR_SOLIDARY = TaxingPropertiesVersion2017.FACTOR_SOLIDARY;
        public const Int32 MIN_VALID_AMOUNT_OF_TAXBONUS = TaxingPropertiesVersion2017.MIN_VALID_AMOUNT_OF_TAXBONUS;
        public const Int32 MAX_VALID_AMOUNT_OF_TAXBONUS = TaxingPropertiesVersion2017.MAX_VALID_AMOUNT_OF_TAXBONUS;
        public const Int32 MIN_VALID_INCOME_OF_TAXBONUS = 9200;
        public const Int32 MAX_VALID_INCOME_OF_ROUNDING = TaxingPropertiesVersion2017.MAX_VALID_INCOME_OF_ROUNDING;
        public const Int32 MAX_VALID_INCOME_OF_WITHHOLD = TaxingPropertiesVersion2017.MAX_VALID_INCOME_OF_WITHHOLD;
        public const Int32 MIN_VALID_INCOME_OF_SOLIDARY = 106444;
    }
}
