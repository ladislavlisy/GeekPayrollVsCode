using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Config
{
    using BundleVersion = UInt16;
    internal static class PenzixPropertiesDefault
    {
        public const BundleVersion VERSION_MIN = 2000;

        public const decimal FACTOR_EMPLOYEE = 0m;
    }
    public static class PenzixPropertiesVersion2011
    {
        public const BundleVersion VERSION_MIN = 2011;

        public const decimal FACTOR_EMPLOYEE = PenzixPropertiesDefault.FACTOR_EMPLOYEE;
    }
    public static class PenzixPropertiesVersion2012
    {
        public const BundleVersion VERSION_MIN = 2012;

        public const decimal FACTOR_EMPLOYEE = PenzixPropertiesVersion2011.FACTOR_EMPLOYEE;
    }
    public static class PenzixPropertiesVersion2013
    {
        public const BundleVersion VERSION_MIN = 2013;

        public const decimal FACTOR_EMPLOYEE = 2.0m;
    }
    public static class PenzixPropertiesVersion2014
    {
        public const BundleVersion VERSION_MIN = 2014;

        public const decimal FACTOR_EMPLOYEE = PenzixPropertiesVersion2013.FACTOR_EMPLOYEE;
    }
    public static class PenzixPropertiesVersion2015
    {
        public const BundleVersion VERSION_MIN = 2015;

        public const decimal FACTOR_EMPLOYEE = PenzixPropertiesVersion2014.FACTOR_EMPLOYEE;
    }
    public static class PenzixPropertiesVersion2016
    {
        public const BundleVersion VERSION_MIN = 2016;

        public const decimal FACTOR_EMPLOYEE = PenzixPropertiesVersion2015.FACTOR_EMPLOYEE;
    }
    public static class PenzixPropertiesVersion2017
    {
        public const BundleVersion VERSION_MIN = 2017;

        public const decimal FACTOR_EMPLOYEE = PenzixPropertiesVersion2016.FACTOR_EMPLOYEE;
    }
    public static class PenzixPropertiesVersion2018
    {
        public const BundleVersion VERSION_MIN = 2018;

        public const decimal FACTOR_EMPLOYEE = PenzixPropertiesVersion2017.FACTOR_EMPLOYEE;
    }
}
