using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Config
{
    using BundleVersion = UInt16;
    internal static class SocialPropertiesDefault
    {
        public const BundleVersion VERSION_MIN = 2000;

        public const Int32 BASIS_MONTHLY_MINIMUM = 0;

        public const Int32 BASIS_ANNUAL_MAXIMUM = 0;

        public const decimal FACTOR_EMPLOYER = 0m;

        public const decimal FACTOR_EMPLOYER_HIGHER = 0m;

        public const decimal FACTOR_EMPLOYEE = 0m;

        public const decimal FACTOR_EMPLOYEE_GARANT = 0m;

        public const decimal FACTOR_REDUCE_GARANT = 0m;

        public const decimal INCOME_EMPLOY_MARGIN = 0m;

        public const decimal INCOME_AGREEM_MARGIN = 0m;
    }
    public static class SocialPropertiesVersion2011
    {
        public const BundleVersion VERSION_MIN = 2011;

        public const Int32 BASIS_MONTHLY_MINIMUM = 0;

        public const Int32 BASIS_ANNUAL_MAXIMUM = 1781280;

        public const decimal FACTOR_EMPLOYER = 25.0m;

        public const decimal FACTOR_EMPLOYER_HIGHER = 26.0m;

        public const decimal FACTOR_EMPLOYEE = 6.5m;

        public const decimal FACTOR_EMPLOYEE_GARANT = 0.0m;

        public const decimal FACTOR_REDUCE_GARANT = 0.0m;

        public const decimal INCOME_EMPLOY_MARGIN = 2000m;

        public const decimal INCOME_AGREEM_MARGIN = 0m;
    }

    public static class SocialPropertiesVersion2012
    {
        public const BundleVersion VERSION_MIN = 2012;

        public const Int32 BASIS_MONTHLY_MINIMUM = SocialPropertiesVersion2011.BASIS_MONTHLY_MINIMUM;

        public const Int32 BASIS_ANNUAL_MAXIMUM = 1206576;

        public const decimal FACTOR_EMPLOYER = SocialPropertiesVersion2011.FACTOR_EMPLOYER;

        public const decimal FACTOR_EMPLOYER_HIGHER = SocialPropertiesVersion2011.FACTOR_EMPLOYER_HIGHER;

        public const decimal FACTOR_EMPLOYEE = SocialPropertiesVersion2011.FACTOR_EMPLOYEE;

        public const decimal FACTOR_EMPLOYEE_GARANT = SocialPropertiesVersion2011.FACTOR_EMPLOYEE_GARANT;

        public const decimal FACTOR_REDUCE_GARANT = SocialPropertiesVersion2011.FACTOR_REDUCE_GARANT;

        public const decimal INCOME_EMPLOY_MARGIN = 2500m;

        public const decimal INCOME_AGREEM_MARGIN = 10000m;
    }

    public static class SocialPropertiesVersion2013
    {
        public const BundleVersion VERSION_MIN = 2013;

        public const Int32 BASIS_MONTHLY_MINIMUM = SocialPropertiesVersion2012.BASIS_MONTHLY_MINIMUM;

        public const Int32 BASIS_ANNUAL_MAXIMUM = 1242432;

        public const decimal FACTOR_EMPLOYER = SocialPropertiesVersion2012.FACTOR_EMPLOYER;

        public const decimal FACTOR_EMPLOYER_HIGHER = SocialPropertiesVersion2012.FACTOR_EMPLOYER_HIGHER;

        public const decimal FACTOR_EMPLOYEE = SocialPropertiesVersion2012.FACTOR_EMPLOYEE;

        public const decimal FACTOR_EMPLOYEE_GARANT = 5.0m;

        public const decimal FACTOR_REDUCE_GARANT = 3.0m;

        public const decimal INCOME_EMPLOY_MARGIN = SocialPropertiesVersion2012.INCOME_EMPLOY_MARGIN;

        public const decimal INCOME_AGREEM_MARGIN = SocialPropertiesVersion2012.INCOME_AGREEM_MARGIN;
    }

    public static class SocialPropertiesVersion2014
    {
        public const BundleVersion VERSION_MIN = 2014;

        public const Int32 BASIS_MONTHLY_MINIMUM = SocialPropertiesVersion2013.BASIS_MONTHLY_MINIMUM;

        public const Int32 BASIS_ANNUAL_MAXIMUM = 1245216;

        public const decimal FACTOR_EMPLOYER = SocialPropertiesVersion2013.FACTOR_EMPLOYER;

        public const decimal FACTOR_EMPLOYER_HIGHER = SocialPropertiesVersion2013.FACTOR_EMPLOYER_HIGHER;

        public const decimal FACTOR_EMPLOYEE = SocialPropertiesVersion2013.FACTOR_EMPLOYEE;

        public const decimal FACTOR_EMPLOYEE_GARANT = SocialPropertiesVersion2013.FACTOR_EMPLOYEE_GARANT;

        public const decimal FACTOR_REDUCE_GARANT = SocialPropertiesVersion2013.FACTOR_REDUCE_GARANT;

        public const decimal INCOME_EMPLOY_MARGIN = SocialPropertiesVersion2013.INCOME_EMPLOY_MARGIN;

        public const decimal INCOME_AGREEM_MARGIN = SocialPropertiesVersion2013.INCOME_AGREEM_MARGIN;
    }

    public static class SocialPropertiesVersion2015
    {
        public const BundleVersion VERSION_MIN = 2015;

        public const Int32 BASIS_MONTHLY_MINIMUM = SocialPropertiesVersion2014.BASIS_MONTHLY_MINIMUM;

        public const Int32 BASIS_ANNUAL_MAXIMUM = 1277328;

        public const decimal FACTOR_EMPLOYER = SocialPropertiesVersion2014.FACTOR_EMPLOYER;

        public const decimal FACTOR_EMPLOYER_HIGHER = 25.0m;

        public const decimal FACTOR_EMPLOYEE = SocialPropertiesVersion2014.FACTOR_EMPLOYEE;

        public const decimal FACTOR_EMPLOYEE_GARANT = SocialPropertiesVersion2014.FACTOR_EMPLOYEE_GARANT;

        public const decimal FACTOR_REDUCE_GARANT = SocialPropertiesVersion2014.FACTOR_REDUCE_GARANT;

        public const decimal INCOME_EMPLOY_MARGIN = SocialPropertiesVersion2014.INCOME_EMPLOY_MARGIN;

        public const decimal INCOME_AGREEM_MARGIN = SocialPropertiesVersion2014.INCOME_AGREEM_MARGIN;
    }
    public static class SocialPropertiesVersion2016
    {
        public const BundleVersion VERSION_MIN = 2016;

        public const Int32 BASIS_MONTHLY_MINIMUM = SocialPropertiesVersion2015.BASIS_MONTHLY_MINIMUM;

        public const Int32 BASIS_ANNUAL_MAXIMUM = 1277328;

        public const decimal FACTOR_EMPLOYER = SocialPropertiesVersion2015.FACTOR_EMPLOYER;

        public const decimal FACTOR_EMPLOYER_HIGHER = 25.0m;

        public const decimal FACTOR_EMPLOYEE = SocialPropertiesVersion2015.FACTOR_EMPLOYEE;

        public const decimal FACTOR_EMPLOYEE_GARANT = SocialPropertiesVersion2015.FACTOR_EMPLOYEE_GARANT;

        public const decimal FACTOR_REDUCE_GARANT = SocialPropertiesVersion2015.FACTOR_REDUCE_GARANT;

        public const decimal INCOME_EMPLOY_MARGIN = SocialPropertiesVersion2015.INCOME_EMPLOY_MARGIN;

        public const decimal INCOME_AGREEM_MARGIN = SocialPropertiesVersion2015.INCOME_AGREEM_MARGIN;
    }
    public static class SocialPropertiesVersion2017
    {
        public const BundleVersion VERSION_MIN = 2017;

        public const Int32 BASIS_MONTHLY_MINIMUM = SocialPropertiesVersion2016.BASIS_MONTHLY_MINIMUM;

        public const Int32 BASIS_ANNUAL_MAXIMUM = 1277328;

        public const decimal FACTOR_EMPLOYER = SocialPropertiesVersion2016.FACTOR_EMPLOYER;

        public const decimal FACTOR_EMPLOYER_HIGHER = 25.0m;

        public const decimal FACTOR_EMPLOYEE = SocialPropertiesVersion2016.FACTOR_EMPLOYEE;

        public const decimal FACTOR_EMPLOYEE_GARANT = SocialPropertiesVersion2016.FACTOR_EMPLOYEE_GARANT;

        public const decimal FACTOR_REDUCE_GARANT = SocialPropertiesVersion2016.FACTOR_REDUCE_GARANT;

        public const decimal INCOME_EMPLOY_MARGIN = SocialPropertiesVersion2016.INCOME_EMPLOY_MARGIN;

        public const decimal INCOME_AGREEM_MARGIN = SocialPropertiesVersion2016.INCOME_AGREEM_MARGIN;
    }
    public static class SocialPropertiesVersion2018
    {
        public const BundleVersion VERSION_MIN = 2018;

        public const Int32 BASIS_MONTHLY_MINIMUM = SocialPropertiesVersion2017.BASIS_MONTHLY_MINIMUM;

        public const Int32 BASIS_ANNUAL_MAXIMUM = 1277328;

        public const decimal FACTOR_EMPLOYER = SocialPropertiesVersion2017.FACTOR_EMPLOYER;

        public const decimal FACTOR_EMPLOYER_HIGHER = 25.0m;

        public const decimal FACTOR_EMPLOYEE = SocialPropertiesVersion2017.FACTOR_EMPLOYEE;

        public const decimal FACTOR_EMPLOYEE_GARANT = SocialPropertiesVersion2017.FACTOR_EMPLOYEE_GARANT;

        public const decimal FACTOR_REDUCE_GARANT = SocialPropertiesVersion2017.FACTOR_REDUCE_GARANT;

        public const decimal INCOME_EMPLOY_MARGIN = SocialPropertiesVersion2017.INCOME_EMPLOY_MARGIN;

        public const decimal INCOME_AGREEM_MARGIN = SocialPropertiesVersion2017.INCOME_AGREEM_MARGIN;
    }
}
