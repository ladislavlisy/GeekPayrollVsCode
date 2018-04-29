using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Config
{
    using BundleVersion = UInt16;
    internal static class EmployPropertiesDefault
    {
        public const BundleVersion VERSION_MIN = 2000;

        public const Int32 DAYS_WORKING_WEEKLY = 5;

        public const Int32 HOURS_WORKING_DAILY = 8;
    }
    public static class EmployPropertiesVersion2011
    {
        public const BundleVersion VERSION_MIN = 2011;

        public const Int32 DAYS_WORKING_WEEKLY = EmployPropertiesDefault.DAYS_WORKING_WEEKLY;

        public const Int32 HOURS_WORKING_DAILY = EmployPropertiesDefault.HOURS_WORKING_DAILY;
    }
    public static class EmployPropertiesVersion2012
    {
        public const BundleVersion VERSION_MIN = 2012;

        public const Int32 DAYS_WORKING_WEEKLY = EmployPropertiesVersion2011.DAYS_WORKING_WEEKLY;

        public const Int32 HOURS_WORKING_DAILY = EmployPropertiesVersion2011.HOURS_WORKING_DAILY;
    }
    public static class EmployPropertiesVersion2013
    {
        public const BundleVersion VERSION_MIN = 2013;

        public const Int32 DAYS_WORKING_WEEKLY = EmployPropertiesVersion2012.DAYS_WORKING_WEEKLY;

        public const Int32 HOURS_WORKING_DAILY = EmployPropertiesVersion2012.HOURS_WORKING_DAILY;
    }
    public static class EmployPropertiesVersion2014
    {
        public const BundleVersion VERSION_MIN = 2014;

        public const Int32 DAYS_WORKING_WEEKLY = EmployPropertiesVersion2013.DAYS_WORKING_WEEKLY;

        public const Int32 HOURS_WORKING_DAILY = EmployPropertiesVersion2013.HOURS_WORKING_DAILY;
    }
    public static class EmployPropertiesVersion2015
    {
        public const BundleVersion VERSION_MIN = 2015;

        public const Int32 DAYS_WORKING_WEEKLY = EmployPropertiesVersion2014.DAYS_WORKING_WEEKLY;

        public const Int32 HOURS_WORKING_DAILY = EmployPropertiesVersion2014.HOURS_WORKING_DAILY;
    }
    public static class EmployPropertiesVersion2016
    {
        public const BundleVersion VERSION_MIN = 2016;

        public const Int32 DAYS_WORKING_WEEKLY = EmployPropertiesVersion2015.DAYS_WORKING_WEEKLY;

        public const Int32 HOURS_WORKING_DAILY = EmployPropertiesVersion2015.HOURS_WORKING_DAILY;
    }
    public static class EmployPropertiesVersion2017
    {
        public const BundleVersion VERSION_MIN = 2017;

        public const Int32 DAYS_WORKING_WEEKLY = EmployPropertiesVersion2016.DAYS_WORKING_WEEKLY;

        public const Int32 HOURS_WORKING_DAILY = EmployPropertiesVersion2016.HOURS_WORKING_DAILY;
    }
    public static class EmployPropertiesVersion2018
    {
        public const BundleVersion VERSION_MIN = 2018;

        public const Int32 DAYS_WORKING_WEEKLY = EmployPropertiesVersion2017.DAYS_WORKING_WEEKLY;

        public const Int32 HOURS_WORKING_DAILY = EmployPropertiesVersion2017.HOURS_WORKING_DAILY;
    }
}
