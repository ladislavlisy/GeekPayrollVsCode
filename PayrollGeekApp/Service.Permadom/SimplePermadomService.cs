using System;
using System.Collections.Generic;

namespace ElementsLib.Service.Permadom
{
    using ConfigCode = UInt16;
    using ConfigRole = UInt16;
    using ConfigGang = UInt16;
    using ConfigType = UInt16;
    using ConfigBind = UInt16;
    using SymbolName = String;

    using TDay = Byte;
    using TSeconds = Int32;


    using ArticleCodeConfigItem = Module.Interfaces.Permadom.ArticleCodeConfigData;
    using ArticleRoleConfigItem = Module.Interfaces.Permadom.ArticleRoleConfigData;

    using Module.Codes;
    using Module.Interfaces.Permadom;
    using ElementsLib.Elements.Config.Sources;
    using ElementsLib.Legalist.Constants;

    public class SimplePermadomService : IPermadomService
    {
        #region LOAD_DATA
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
                new ArticleData() {
                    Head = 1, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_CONTRACT_ATTEND_ITEM,
                    Tags = new ContractAttendItemSource(TestModule.AbsenceFrom, TestModule.AbsenceStop, TestModule.AbsenceDaysParam, TestModule.AbsenceDaysHours),
                },
                new ArticleData() {
                    Head = 1, Part = 1, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_POSITION_MONTHLY_AMOUNT,
                    Tags = new MonthlyAmountSource(TestModule.BasicSalaryPeriod),
                },
                new ArticleData() {
                    Head = 1, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_TAX_DECLARATION,
                    Tags = new TaxDeclarationSource(TestModule.TaxStatementType, TestModule.TaxingPartyType, TestModule.TaxDeclaracyType, TestModule.TaxResidencyType,
                        TestModule.BasicSalaryAnnual, TestModule.BasicSalaryAnnual),
                },
                new ArticleData() {
                    Head = 1, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_INS_DECLARATION_HEALTH,
                    Tags = new InsDeclarationHealthSource(TestModule.HealthStatementType, TestModule.HealthPartyType, TestModule.BasicSalaryAnnual),
                },
                new ArticleData() {
                    Head = 1, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_INS_DECLARATION_SOCIAL,
                    Tags = new InsDeclarationSocialSource(TestModule.SocialStatementType, TestModule.SocialPartyType, TestModule.BasicSalaryAnnual),
                },
                new ArticleData() {
                    Head = 1, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_INS_INCOMES_HEALTH,
                    Tags = null,
                },
                new ArticleData() {
                    Head = 1, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_INS_INCOMES_SOCIAL,
                    Tags = null,
                },
                new ArticleData() {
                    Head = 1, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_TAX_INCOMES_GENERAL,
                    Tags = null,
                },
                new ArticleData() {
                    Head = 1, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_TAX_INCOMES_HEALTH,
                    Tags = null,
                },
                new ArticleData() {
                    Head = 1, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_TAX_INCOMES_SOCIAL,
                    Tags = null,
                },
                new ArticleData() {
                    Head = 0, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_TAX_BASE_ADVANCE,
                    Tags = null,
                },
                new ArticleData() {
                    Head = 0, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_TAX_INCOMES_WITHHOLD_GENERAL,
                    Tags = null,
                },
                new ArticleData() {
                    Head = 0, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_TAX_INCOMES_WITHHOLD_LOLEVEL,
                    Tags = null,
                },
                new ArticleData() {
                    Head = 0, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_TAX_INCOMES_WITHHOLD_TASKAGR,
                    Tags = null,
                },
                new ArticleData() {
                    Head = 0, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_TAX_INCOMES_WITHHOLD_PARTNER,
                    Tags = null,
                },
                new ArticleData() {
                    Head = 0, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_TAX_BASE_WITHHOLD,
                    Tags = null,
                },
                new ArticleData() {
                    Head = 0, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_TAX_BASE_ADVANCE_HEALTH,
                    Tags = null,
                },
                new ArticleData() {
                    Head = 0, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_TAX_BASE_ADVANCE_SOCIAL,
                    Tags = null,
                },
                new ArticleData() {
                    Head = 0, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_TAX_BASE_ADVANCE_PARTIAL,
                    Tags = null,
                },
                new ArticleData() {
                    Head = 0, Part = 0, Seed = 1, Code = (UInt16)ArticleCodeCz.FACT_TAX_BASE_ADVANCE_SOLIDARY,
                    Tags = null,
                },
                //FACT_POSITION_TIMESHEET,
                //FACT_POSITION_WORKING,
                //FACT_POSITION_ABSENCE,
                //FACT_CONTRACT_TIMESHEET,
                //FACT_CONTRACT_WORKING,
            };
        }
        #endregion
        public IEnumerable<ArticleCodeConfigItem> GetArticleCodeData()
        {
            const ConfigGang EARNINGS_GANG = 1;
            const ConfigGang TRANSFER_GANG = 2;
            //const ConfigGang GROSSNET_GANG = 3;
            //const ConfigGang DEDUCTED_GANG = 4;
            //const ConfigGang PAYMENTS_GANG = 5;

            const ConfigType NO_HEAD_PART_TYPE = 0;
            const ConfigType HEAD_CODE_ARTICLE = 1;
            const ConfigType PART_CODE_ARTICLE = 2;

            const ConfigBind ARTICLE_OPT = 0;
            const ConfigBind ARTICLE_REQ = 1;

            const TaxingBehaviour TAXING_ADVANCE = TaxingBehaviour.TAXING_ADVANCE;
            const TaxingBehaviour TAXING_NOTHING = TaxingBehaviour.TAXING_NOTHING;
            const HealthBehaviour HEALTH_NOTHING = HealthBehaviour.HEALTH_NOTHING;
            const HealthBehaviour HEALTH_INCOMES = HealthBehaviour.HEALTH_INCOMES;
            const SocialBehaviour SOCIAL_NOTHING = SocialBehaviour.SOCIAL_NOTHING;
            const SocialBehaviour SOCIAL_INCOMES = SocialBehaviour.SOCIAL_INCOMES;

            return new List<ArticleCodeConfigItem>()
            {
                new ArticleCodeConfigData(0, 0, EARNINGS_GANG, NO_HEAD_PART_TYPE, ARTICLE_OPT,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_UNKNOWN"),
                new ArticleCodeConfigData(1, 1, EARNINGS_GANG, NO_HEAD_PART_TYPE, ARTICLE_REQ,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_CONTRACT_TERM"),
                new ArticleCodeConfigData(2, 2, EARNINGS_GANG, HEAD_CODE_ARTICLE, ARTICLE_REQ,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_CONTRACT_TIMESHEET", 1, 8),
                new ArticleCodeConfigData(5, 5, EARNINGS_GANG, HEAD_CODE_ARTICLE, ARTICLE_OPT,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_CONTRACT_ATTEND_ITEM", 2),
                new ArticleCodeConfigData(4, 4, EARNINGS_GANG, HEAD_CODE_ARTICLE, ARTICLE_REQ,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_CONTRACT_ABSENCE", 2, 10),
                new ArticleCodeConfigData(3, 3, EARNINGS_GANG, HEAD_CODE_ARTICLE, ARTICLE_REQ,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_CONTRACT_WORKING", 2, 9),
                new ArticleCodeConfigData(6, 6, EARNINGS_GANG, HEAD_CODE_ARTICLE, ARTICLE_REQ,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_POSITION_TERM", 1),
                new ArticleCodeConfigData(7, 7, EARNINGS_GANG, PART_CODE_ARTICLE, ARTICLE_REQ,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_POSITION_SCHEDULE", 6),
                new ArticleCodeConfigData(8, 8, EARNINGS_GANG, PART_CODE_ARTICLE, ARTICLE_REQ,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_POSITION_TIMESHEET", 7),
                new ArticleCodeConfigData(10, 10, EARNINGS_GANG, PART_CODE_ARTICLE, ARTICLE_REQ,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_POSITION_ABSENCE", 8, 5),
                new ArticleCodeConfigData(9, 9, EARNINGS_GANG, PART_CODE_ARTICLE, ARTICLE_REQ,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_POSITION_WORKING", 8, 10),
                new ArticleCodeConfigData(10000, 1000, EARNINGS_GANG, PART_CODE_ARTICLE, ARTICLE_OPT,
                    TAXING_ADVANCE, HEALTH_INCOMES, SOCIAL_INCOMES, "FACT_POSITION_MONTHLY_AMOUNT", 8, 9),
                new ArticleCodeConfigData(10001, 1001, TRANSFER_GANG, HEAD_CODE_ARTICLE, ARTICLE_REQ,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_TAX_DECLARATION"),
                new ArticleCodeConfigData(10002, 1002, TRANSFER_GANG, HEAD_CODE_ARTICLE, ARTICLE_REQ,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_INS_HEALTH_DECLARATION"),
                new ArticleCodeConfigData(10003, 1003, TRANSFER_GANG, HEAD_CODE_ARTICLE, ARTICLE_REQ,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_INS_SOCIAL_DECLARATION"),
                new ArticleCodeConfigData(10004, 1004, TRANSFER_GANG, HEAD_CODE_ARTICLE, ARTICLE_REQ,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_TAX_INCOMES_GENERAL", 10001),
                new ArticleCodeConfigData(10005, 1005, TRANSFER_GANG, HEAD_CODE_ARTICLE, ARTICLE_REQ, 
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_TAX_INCOMES_HEALTH", 10001, 10002, 10012),
                new ArticleCodeConfigData(10006, 1006, TRANSFER_GANG, HEAD_CODE_ARTICLE, ARTICLE_REQ, 
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_TAX_INCOMES_SOCIAL", 10001, 10003, 10013),
                new ArticleCodeConfigData(10012, 1012, TRANSFER_GANG, HEAD_CODE_ARTICLE, ARTICLE_REQ,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_INS_INCOMES_HEALTH", 10002),
                new ArticleCodeConfigData(10013, 1013, TRANSFER_GANG, HEAD_CODE_ARTICLE, ARTICLE_REQ, 
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_INS_INCOMES_SOCIAL", 10003),
                new ArticleCodeConfigData(10007, 1007, TRANSFER_GANG, NO_HEAD_PART_TYPE, ARTICLE_REQ,
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_TAX_INCOMES_ADVANCE", 10004, 10005, 10006),
                new ArticleCodeConfigData(10008, 1008, TRANSFER_GANG, NO_HEAD_PART_TYPE, ARTICLE_REQ, 
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_TAX_INCOMES_WITHHOLD_GENERAL", 10007, 10004, 10005, 10006),
                new ArticleCodeConfigData(10009, 1009, TRANSFER_GANG, NO_HEAD_PART_TYPE, ARTICLE_REQ, 
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_TAX_INCOMES_WITHHOLD_LOLEVEL", 10007, 10004, 10005, 10006, 10008),
                new ArticleCodeConfigData(10010, 1010, TRANSFER_GANG, NO_HEAD_PART_TYPE, ARTICLE_REQ, 
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_TAX_INCOMES_WITHHOLD_TASKAGR", 10007, 10004, 10005, 10006, 10008, 10009),
                new ArticleCodeConfigData(10011, 1011, TRANSFER_GANG, NO_HEAD_PART_TYPE, ARTICLE_REQ, 
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_TAX_INCOMES_WITHHOLD_PARTNER", 10007, 10004, 10005, 10006, 10008, 10009, 10010),
                new ArticleCodeConfigData(10014, 1014, TRANSFER_GANG, NO_HEAD_PART_TYPE, ARTICLE_REQ, 
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_TAX_BASE_ADVANCE", 10007, 10008, 10009, 10010, 10011),
                new ArticleCodeConfigData(10015, 1015, TRANSFER_GANG, NO_HEAD_PART_TYPE, ARTICLE_REQ, 
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_TAX_BASE_ADVANCE_HEALTH", 10005),
                new ArticleCodeConfigData(10016, 1016, TRANSFER_GANG, NO_HEAD_PART_TYPE, ARTICLE_REQ, 
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_TAX_BASE_ADVANCE_SOCIAL", 10006),
                new ArticleCodeConfigData(10017, 1017, TRANSFER_GANG, NO_HEAD_PART_TYPE, ARTICLE_REQ, 
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_TAX_BASE_ADVANCE_PARTIAL", 10014, 10015, 10016),
                new ArticleCodeConfigData(10019, 1019, TRANSFER_GANG, NO_HEAD_PART_TYPE, ARTICLE_REQ, 
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_TAX_BASE_ADVANCE_SOLIDARY", 10007),
                new ArticleCodeConfigData(10023, 1023, TRANSFER_GANG, NO_HEAD_PART_TYPE, ARTICLE_REQ, 
                    TAXING_NOTHING, HEALTH_NOTHING, SOCIAL_NOTHING, "FACT_TAX_BASE_WITHHOLD", 10007, 10008, 10009, 10010, 10011),
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
                new ArticleRoleConfigData(1000, "ARTICLE_POSITION_MONTHLY_AMOUNT"),
                new ArticleRoleConfigData(1001, "ARTICLE_TAX_DECLARATION"),
                new ArticleRoleConfigData(1002, "ARTICLE_INS_DECLARATION_HEALTH"),
                new ArticleRoleConfigData(1003, "ARTICLE_INS_DECLARATION_SOCIAL"),
                new ArticleRoleConfigData(1004, "ARTICLE_TAX_INCOMES_GENERAL"),
                new ArticleRoleConfigData(1005, "ARTICLE_TAX_INCOMES_HEALTH"),
                new ArticleRoleConfigData(1006, "ARTICLE_TAX_INCOMES_SOCIAL"),
                new ArticleRoleConfigData(1012, "ARTICLE_INS_INCOMES_HEALTH"),
                new ArticleRoleConfigData(1013, "ARTICLE_INS_INCOMES_SOCIAL"),
                new ArticleRoleConfigData(1007, "ARTICLE_TAX_INCOMES_ADVANCE"),
                new ArticleRoleConfigData(1008, "ARTICLE_TAX_INCOMES_WITHHOLD_GENERAL"),
                new ArticleRoleConfigData(1009, "ARTICLE_TAX_INCOMES_WITHHOLD_LOLEVEL"),
                new ArticleRoleConfigData(1010, "ARTICLE_TAX_INCOMES_WITHHOLD_TASKAGR"),
                new ArticleRoleConfigData(1011, "ARTICLE_TAX_INCOMES_WITHHOLD_PARTNER"),
                new ArticleRoleConfigData(1014, "ARTICLE_TAX_BASE_ADVANCE"),
                new ArticleRoleConfigData(1015, "ARTICLE_TAX_BASE_ADVANCE_HEALTH"),
                new ArticleRoleConfigData(1016, "ARTICLE_TAX_BASE_ADVANCE_SOCIAL"),
                new ArticleRoleConfigData(1017, "ARTICLE_TAX_BASE_ADVANCE_PARTIAL"),
                new ArticleRoleConfigData(1019, "ARTICLE_TAX_BASE_ADVANCE_SOLIDARY"),
                new ArticleRoleConfigData(1023, "ARTICLE_TAX_BASE_WITHHOLD"),
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
        public static TDay AbsenceFrom = 8;
        public static TDay AbsenceStop = 14;
        public static WorkDayPieceType[] AbsenceDaysParam = new WorkDayPieceType[7] {
            WorkDayPieceType.WORKDAY_FULL,
            WorkDayPieceType.WORKDAY_FULL,
            WorkDayPieceType.WORKDAY_FULL,
            WorkDayPieceType.WORKDAY_HALF,
            WorkDayPieceType.WORKDAY_HALF,
            WorkDayPieceType.WORKDAY_NONE,
            WorkDayPieceType.WORKDAY_NONE };
        public static TSeconds[] AbsenceDaysHours = new TSeconds[7] {0, 0, 0, 0, 0, 0, 0 };
        public static decimal BasicSalaryPeriod = 15000m;
        public static decimal BasicSalaryAnnual = 1500000m;

        public static Byte TaxStatementType = 1;
        public static WorkTaxingTerms TaxingPartyType = WorkTaxingTerms.TAXING_TERM_EMPLOYMENT_POLICY;
        public static Byte TaxDeclaracyType = 1;
        public static Byte TaxResidencyType = 0;

        public static Byte HealthStatementType = 1;
        public static WorkHealthTerms HealthPartyType = WorkHealthTerms.HEALTH_TERM_EMPLOYMENT;
        public static Byte HealthForeignerType = 0;

        public static Byte SocialStatementType = 1;
        public static WorkSocialTerms SocialPartyType = WorkSocialTerms.SOCIAL_TERM_EMPLOYMENT;
        public static Byte SocialForeignerType = 0;

        #endregion
    }
}
