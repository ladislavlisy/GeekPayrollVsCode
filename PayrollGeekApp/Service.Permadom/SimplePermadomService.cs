using System;
using System.Collections.Generic;

namespace ElementsLib.Service.Permadom
{
    using ConfigBind = UInt16;

    using ArticleCodeConfigItem = Module.Interfaces.Permadom.ArticleCodeConfigData;
    using ArticleRoleConfigItem = Module.Interfaces.Permadom.ArticleRoleConfigData;

    using Module.Codes;
    using Module.Interfaces.Permadom;
    using ElementsLib.Elements.Config.Sources;
    using ElementsLib.Legalist.Constants;

    public class SimplePermadomService : IPermadomService
    {
        public IEnumerable<ArticleData> GetArticleSourceData()
        {
            return new List<ArticleData>()
            {
                new ArticleData() {
                    Head = 0, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_CONTRACT_TERM,
                    Tags = new ContractTermSource(TestModule.DateFrom, TestModule.DateStop, TestModule.EmployeeTerm),
                },
                new ArticleData() {
                    Head = 1, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_POSITION_TERM,
                    Tags = new PositionTermSource(TestModule.DateFrom, TestModule.DateStop, TestModule.PositionTerm),
                },
                new ArticleData() {
                    Head = 1, Part = 1, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_POSITION_SCHEDULE,
                    Tags = new PositionScheduleSource(TestModule.ShiftLiable, TestModule.ShiftActual, TestModule.ScheduleType),
                },
                new ArticleData() {
                    Head = 1, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_CONTRACT_WORKING,
                    Tags = null,
                },
                new ArticleData() {
                    Head = 1, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_CONTRACT_ABSENCE,
                    Tags = null,
                },
                //FACT_POSITION_TIMESHEET,
                //FACT_POSITION_WORKING,
                //FACT_POSITION_ABSENCE,
                //FACT_CONTRACT_TIMESHEET,
                //FACT_CONTRACT_WORKING,
                //FACT_CONTRACT_ABSENCE,
            };
        }
        public IEnumerable<ArticleCodeConfigItem> GetArticleCodeData()
        {
            const ConfigBind OPT = 0;
            const ConfigBind REQ = 1;

            return new List<ArticleCodeConfigItem>()
            {
                new ArticleCodeConfigData(0, 0, 0, OPT, "FACT_UNKNOWN"),
                new ArticleCodeConfigData(1, 1, 0, REQ, "FACT_CONTRACT_TERM"),
                new ArticleCodeConfigData(2, 2, 1, REQ, "FACT_CONTRACT_TIMESHEET", 1, 8),
                new ArticleCodeConfigData(3, 3, 1, REQ, "FACT_CONTRACT_WORKING", 2, 9),
                new ArticleCodeConfigData(5, 5, 1, OPT, "FACT_CONTRACT_ATTEND_ITEM", 2),
                new ArticleCodeConfigData(4, 4, 1, REQ, "FACT_CONTRACT_ABSENCE", 2, 10),
                new ArticleCodeConfigData(6, 6, 1, REQ, "FACT_POSITION_TERM", 1),
                new ArticleCodeConfigData(7, 7, 2, REQ, "FACT_POSITION_SCHEDULE", 6),
                new ArticleCodeConfigData(8, 8, 2, REQ, "FACT_POSITION_TIMESHEET", 7),
                new ArticleCodeConfigData(9, 9, 2, REQ, "FACT_POSITION_WORKING", 8),
                new ArticleCodeConfigData(10, 10, 2, REQ, "FACT_POSITION_ABSENCE", 8, 5),
            };
        }
        public IEnumerable<ArticleRoleConfigItem> GetArticleRoleData()
        {
            return new List<ArticleRoleConfigItem>()
            {
                new ArticleRoleConfigData(0, "ARTICLE_UNKNOWN"),
                new ArticleRoleConfigData(1, "ARTICLE_CONTRACT_TERM"),
                new ArticleRoleConfigData(6, "ARTICLE_POSITION_TERM"),
                new ArticleRoleConfigData(7, "ARTICLE_POSITION_SCHEDULE"),
                new ArticleRoleConfigData(8, "ARTICLE_POSITION_TIMESHEET"),
                new ArticleRoleConfigData(9, "ARTICLE_POSITION_WORKING"),
                new ArticleRoleConfigData(10, "ARTICLE_POSITION_ABSENCE"),
                new ArticleRoleConfigData(2, "ARTICLE_CONTRACT_TIMESHEET"),
                new ArticleRoleConfigData(3, "ARTICLE_CONTRACT_WORKING"),
                new ArticleRoleConfigData(4, "ARTICLE_CONTRACT_ABSENCE"),
                new ArticleRoleConfigData(5, "ARTICLE_CONTRACT_ATTEND_ITEM"),
            };
        }

        public IEnumerable<ArticleCodeConfigItem> CreateArticleCodeDataList()
        {
            return ArticleCodeConfigBuilder.GetConfigDataList();
        }
        public IEnumerable<ArticleRoleConfigItem> CreateArticleRoleDataList()
        {
            return ArticleRoleConfigBuilder.GetConfigDataList();
        }
    }
    static class TestModule
    {
        #region TEST_VALUES

        public static DateTime? DateFrom = new DateTime(2010, 1, 1);
        public static DateTime? DateStop = null;
        public static WorkEmployTerms EmployeeTerm = WorkEmployTerms.WORKTERM_EMPLOYMENT_1;
        public static WorkPositionType PositionTerm = WorkPositionType.POSITION_EXCLUSIVE;
        public static Int32 ShiftLiable = 144000;
        public static Int32 ShiftActual = 144000;
        public static WorkScheduleType ScheduleType = WorkScheduleType.SCHEDULE_NORMALY_WEEK;
        public static WorkDayPieceType FullDayType = WorkDayPieceType.WORKDAY_FULL;
        public static WorkDayPieceType HalfDayType = WorkDayPieceType.WORKDAY_HALF;
        public static WorkDayPieceType NoneDayType = WorkDayPieceType.WORKDAY_NONE;
        public static WorkDayPieceType TimeDayType = WorkDayPieceType.WORKDAY_TIME;

        #endregion
    }
}
