using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Config
{
    using BundleVersion = UInt16;
    internal static class HealthPropertiesDefault
    {
        public const BundleVersion VERSION_MIN = 2000;

        public const Int32 BASIS_MONTHLY_MINIMUM = 0;

        public const decimal BASIS_ANNUAL_MAXIMUM = 0m;

        public const decimal FACTOR_COMPOUND = 0m;

        public const decimal INCOME_EMPLOY_MARGIN = 0m;

        public const decimal INCOME_AGREEM_MARGIN = 0m;
    }
    public static class HealthPropertiesVersion2011
    {
        public const BundleVersion VERSION_MIN = 2011;

        public const Int32 BASIS_MONTHLY_MINIMUM = 8000;

        public const decimal BASIS_ANNUAL_MAXIMUM = 1781280m;

        public const decimal FACTOR_COMPOUND = 13.5m;

        public const decimal INCOME_EMPLOY_MARGIN = 2000m;

        public const decimal INCOME_AGREEM_MARGIN = 0m;
    }
    public static class HealthPropertiesVersion2012
    {
        public const BundleVersion VERSION_MIN = 2012;

        public const Int32 BASIS_MONTHLY_MINIMUM = HealthPropertiesVersion2011.BASIS_MONTHLY_MINIMUM;

        public const decimal BASIS_ANNUAL_MAXIMUM = 1809864m;

        public const decimal FACTOR_COMPOUND = HealthPropertiesVersion2011.FACTOR_COMPOUND;

        public const decimal INCOME_EMPLOY_MARGIN = 2500m;

        public const decimal INCOME_AGREEM_MARGIN = 10000m;
    }

    public static class HealthPropertiesVersion2013
    {
        public const BundleVersion VERSION_MIN = 2013;

        public const Int32 BASIS_MONTHLY_MINIMUM_UPTO_07 = 8000;

        public const Int32 BASIS_MONTHLY_MINIMUM = 8500;

        public const decimal BASIS_ANNUAL_MAXIMUM = 0m;

        public const decimal FACTOR_COMPOUND = HealthPropertiesVersion2012.FACTOR_COMPOUND;

        public const decimal INCOME_EMPLOY_MARGIN = HealthPropertiesVersion2012.INCOME_EMPLOY_MARGIN;

        public const decimal INCOME_AGREEM_MARGIN = HealthPropertiesVersion2012.INCOME_AGREEM_MARGIN;
    }

    public static class HealthPropertiesVersion2014
    {
        public const BundleVersion VERSION_MIN = 2014;

        public const Int32 BASIS_MONTHLY_MINIMUM = 8500;

        public const decimal BASIS_ANNUAL_MAXIMUM = HealthPropertiesVersion2013.BASIS_ANNUAL_MAXIMUM;

        public const decimal FACTOR_COMPOUND = HealthPropertiesVersion2013.FACTOR_COMPOUND;

        public const decimal INCOME_EMPLOY_MARGIN = HealthPropertiesVersion2013.INCOME_EMPLOY_MARGIN;

        public const decimal INCOME_AGREEM_MARGIN = HealthPropertiesVersion2013.INCOME_AGREEM_MARGIN;
    }

    public static class HealthPropertiesVersion2015
    {
        public const BundleVersion VERSION_MIN = 2015;

        public const Int32 BASIS_MONTHLY_MINIMUM = 9200;

        public const decimal BASIS_ANNUAL_MAXIMUM = HealthPropertiesVersion2014.BASIS_ANNUAL_MAXIMUM;

        public const decimal FACTOR_COMPOUND = HealthPropertiesVersion2014.FACTOR_COMPOUND;

        public const decimal INCOME_EMPLOY_MARGIN = HealthPropertiesVersion2014.INCOME_EMPLOY_MARGIN;

        public const decimal INCOME_AGREEM_MARGIN = HealthPropertiesVersion2014.INCOME_AGREEM_MARGIN;
    }
    public static class HealthPropertiesVersion2016
    {
        public const BundleVersion VERSION_MIN = 2016;

        public const Int32 BASIS_MONTHLY_MINIMUM = 9200;

        public const decimal BASIS_ANNUAL_MAXIMUM = HealthPropertiesVersion2015.BASIS_ANNUAL_MAXIMUM;

        public const decimal FACTOR_COMPOUND = HealthPropertiesVersion2015.FACTOR_COMPOUND;

        public const decimal INCOME_EMPLOY_MARGIN = HealthPropertiesVersion2015.INCOME_EMPLOY_MARGIN;

        public const decimal INCOME_AGREEM_MARGIN = HealthPropertiesVersion2015.INCOME_AGREEM_MARGIN;
    }
    public static class HealthPropertiesVersion2017
    {
        public const BundleVersion VERSION_MIN = 2017;

        public const Int32 BASIS_MONTHLY_MINIMUM = 9200;

        public const decimal BASIS_ANNUAL_MAXIMUM = HealthPropertiesVersion2016.BASIS_ANNUAL_MAXIMUM;

        public const decimal FACTOR_COMPOUND = HealthPropertiesVersion2016.FACTOR_COMPOUND;

        public const decimal INCOME_EMPLOY_MARGIN = HealthPropertiesVersion2016.INCOME_EMPLOY_MARGIN;

        public const decimal INCOME_AGREEM_MARGIN = HealthPropertiesVersion2016.INCOME_AGREEM_MARGIN;
    }
    public static class HealthPropertiesVersion2018
    {
        public const BundleVersion VERSION_MIN = 2018;

        public const Int32 BASIS_MONTHLY_MINIMUM = 9200;

        public const decimal BASIS_ANNUAL_MAXIMUM = HealthPropertiesVersion2017.BASIS_ANNUAL_MAXIMUM;

        public const decimal FACTOR_COMPOUND = HealthPropertiesVersion2017.FACTOR_COMPOUND;

        public const decimal INCOME_EMPLOY_MARGIN = HealthPropertiesVersion2017.INCOME_EMPLOY_MARGIN;

        public const decimal INCOME_AGREEM_MARGIN = HealthPropertiesVersion2017.INCOME_AGREEM_MARGIN;
    }
}
