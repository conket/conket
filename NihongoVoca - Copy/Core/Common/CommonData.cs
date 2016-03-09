using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Ivs.Core.Data;

namespace Ivs.Core.Common
{
    public static class CommonData
    {
        public enum Alignment
        {
            // Summary:
            //     Places an object or text at the position specified via the RightToLeft property.
            Default = 0,
            //
            // Summary:
            //     Sets the object/text position relative to its default position within an
            //     object.  The default position of the text is specified via the RightToLeft
            //     property. If the default alignment is left, setting the alignment option
            //     to Near places the text to the left. Convercely, if the default alignment
            //     is right, setting the alignment option to Near places the text to the right.
            Near = 1,
            //
            // Summary:
            //     Centers an object or text within an object.
            Center = 2,
            //
            // Summary:
            //     Sets the object/text position relative to its default position within an
            //     object.  The default position of the text is specified via the RightToLeft
            //     property. If the default alignment is left, setting an alignment option to
            //     Far places the text to the right. Conversely, if the default alignment is
            //     right, setting the alignment option to Far places the text to the left.
            Far = 3,
        }
        public enum InputWebType
        {
            text,
            password,
            date,
            datetime,
            time,
            email,
            number,
            month,
            week,
        }
        public static int NumOfInvoiceRow = 15;
        public struct Language
        {
            public const string English = "en";
            public const string VietNamese = "vi";
            public const string Japanese = "ja";
        }

        public struct LanguageDB
        {
            public const string English = "EN";
            public const string VietNamese = "VN";
            public const string Japanese = "JP";
        }

        public struct CultureInfo
        {
            public const string English = "en-US";
            public const string VietNamese = "vi-VN";
            public const string Japanese = "ja-JP";
        }

        public static string ConvertLanguageDBToCulture(this string language)
        {
            string result = string.Empty;
            switch (language)
            {
                case LanguageDB.English:
                    result = Language.English;
                    break;

                case LanguageDB.Japanese:
                    result = Language.Japanese;
                    break;

                case LanguageDB.VietNamese:
                    result = Language.VietNamese;
                    break;

                default:
                    result = Language.English;
                    break;
            }

            return result;
        }

        public static string ConvertCultureLanguageToDB(this string language)
        {
            string result = string.Empty;
            switch (language)
            {
                case Language.English:
                    result = LanguageDB.English;
                    break;

                case Language.Japanese:
                    result = LanguageDB.Japanese;
                    break;

                case Language.VietNamese:
                    result = LanguageDB.VietNamese;
                    break;

                default:
                    result = LanguageDB.English;
                    break;
            }

            return result;
        }

        public static SortedDictionary<string, string> GetLanguage()
        {
            SortedDictionary<string, string> dictonaryLanguage = new SortedDictionary<string, string>
                {
                    {Language.English, "English"},
                    {Language.VietNamese, "Tiếng Việt"} ,
                    {Language.Japanese, "Japanese"}
                };

            return dictonaryLanguage;
        }

        public enum MessageType
        {
            Ok,
            OkCancel,
            YesNo,
            YesNoCancel
        }

        public struct MessageIcon
        {
            public const string None = "0";
            public const string Error = "99";
            public const string Warning = "98";
            public const string Infomation = "97";
            public const string Question = "96";
            public const string OK = "95";
        }

        public struct DateType
        {
            public const string Work = "1";
        }

        public struct DeliveryType
        {
            public const string External = "1";
            public const string Internal = "2";
        }

        public struct AnnualLeaveType
        {
            public const string Year = "1";
            public const string Month = "2";
        }

        public struct TimeSheetBatchType
        {
            public const string Work = "1";
            public const string SubstituteWork = "2";
            public const string CompensatoryWork = "3";
            public const string WorkOnDayOff = "4";
            public const string PaidLeave = "5";
            public const string UnpaidLeave = "6";
            public const string SpecialPaidHoliday = "7";
            public const string CompensatoryLeave = "8";
            public const string SubstituteLeave = "9";
            public const string Others = "10";
        }

        public struct TimeSheetBatchStatus
        {
            public const string Planned = "1";
            public const string ProcessingOK = "2";
            public const string ProcessingErr = "3";
        }

        //public struct LeaveCategoryBatch
        //{
        //    public const string AnnualLeave = "Leave01";
        //    public const string DayOffWithoutApplication = "Leave16";
        //    public const string DayOffWithApplication = "Leave15";
        //}

        public struct LeaveMask
        {
            public const string UApL = "UApL";
        }

        public struct MasterAllowanceType
        {
            public const string PayByWorkingDay = "1";
            public const string FixedPayment = "2";
        }

        public static string TimeCheckFrom
        {
            get
            {
                string value = "06:00:00";
                try
                {
                    value = System.Configuration.ConfigurationManager.AppSettings["TimeCheckFrom"];
                    if (!string.IsNullOrEmpty(value))
                    {
                        value = "06:00:00";
                    }
                }
                catch (Exception ex)
                {
                    value = "06:00:00";
                }

                return value;
            }
        }

        public static string TimeCheckTo
        {
            get
            {
                string value = "23:59:00";
                try
                {
                    value = System.Configuration.ConfigurationManager.AppSettings["TimeCheckTo"];
                    if (!string.IsNullOrEmpty(value))
                    {
                        value = "23:59:00";
                    }
                }
                catch (Exception ex)
                {
                    value = "23:59:00";
                }

                return value;
            }
        }

        public struct MySqlErrorNumber
        {
            public const int InvalidHost = 2005;
            public const int InvalidDatabase = 1049;
            public const int AccessDenied = 1045;
            public const int LostConnection = 1042;
            public const int DuplicateKey = 1062;
            public const int ForgeignKeyNotExist = 1452;
            public const int ForgeignKeyViolation = 1451;
        }

        /// <summary>
        ///  Present the status of mode when switch from search form to edit form
        /// </summary>
        public enum Mode
        {
            View,
            Edit,
            Copy,
            New

        }

        public struct AllowanceType
        {
            public const string IndustriousAllowance = "4";
        }

        public struct AllowanceID
        {
            public const string Transport = "1";
            public const string Housing = "2";
            public const string PerfectAttendance = "3";
            public const string HardShip = "4";
            public const string Position = "5";
            public const string Shifting = "6";
            public const string Skill = "7";
            public const string Meal = "8";
            public const string LengthService = "9";
            public const string Parking = "10";
            public const string AnualLeave = "11";
        }

        public struct AnualLeaveTransferType
        {
            public const string TotalTransfer = "1";
            public const string OnlyCurrentYear = "2";
            public const string No = "3";
        }

        public enum MessageTypeResult
        {
            None = 0,
            OK = 1,
            Cancel = 2,
            Yes = 3,
            No = 4,
        }

        public struct ATNumber
        {
            public const string FullTime = "0";
            public const string PartTime1 = "1";
            public const string PartTime2 = "2";
            public const string PartTime3 = "3";
            public const string PartTime4 = "4";
            public const string PartTime5 = "5";
        }

        public struct PrintSheetDelivery
        {
            public const int DeliverySheet = 3;
        }

        public struct CommonCalculation
        {
            public const string Attendence = "O";
            public const string Unknown = "U";
            public const string OTWorkingDay = "OT";
            public const string Absence = "X";
            public const string Leave = "L";
            public const string SpecialHoliday = "SH";
            public const string OTCompanyHoliday = "CH";
            public const string SubsidiaryLeave = "SBL";
            public const string OTHoliday = "OH";
            public const string OTWeekend = "OT";
            public const string Error = "E";
            public const string NoSymbol = "";
        }

        public struct OperScope
        {
            public const string HR = "0";
            public const string Office = "1";
            public const string Department = "2";
            public const string Section = "3";
            public const string Team = "4";
            public const string Route = "5";
            public const string Operation = "6";
        }

        public struct DateFormat
        {
            public const string DdMMyyyy = "dd/MM/yyyy";
            public const string MMddyyyy = "MM/dd/yyyy";
            public const string YyyyMMdd = "yyyy/MM/dd";
            public const string Yyyy_MM_dd = "yyyy-MM-dd";
            public const string InternalDateFormat = "yyyy-MM-dd";
            public const string InternalPeriodFormat = "yyyy-MM";
            public const string Yyyy_MM_ddHHmmss = "yyyy-MM-dd HH:mm:ss";
            public const string Yyyy_MM_ddHHmm = "yyyy-MM-dd HH:mm";
            public const string YyyyMMddHHmmss = "yyyyMMddHHmmss";
            public const string YyyyMMddHHmm = "yyyyMMddHHmm";
            public const string DdMMyyyyHHmmss = "dd/MM/yyyy HH:mm:ss";
            public const string DdMMyyyyHHmm = "dd/MM/yyyy HH:mm";
            public const string MMddyyyyHHmmss = "MM/dd/yyyy HH:mm:ss";
            public const string MMddyyyyHHmm = "MM/dd/yyyy HH:mm";
            public const string yyyyMM = "yyyyMM";
            public const string yyyy_MM = "yyyy/MM";
            public const string YyyyMMDD = "yyyyMMDD";
            public const string YyyyMMDD_HHmmss = "yyyyMMdd HH:mm:ss";
            public const string yyyyMMdd = "yyyyMMdd";
            public const string MMyyyy = "MM/yyyy";

            public const string dd_MMM_yyyyhhmm = "dd MMM, yyyy HH:mm";
            public const string dd_MMM_yyyy = "dd MMM, yyyy";
        }

        public struct NumberFormat
        {
            public const string N0 = "N0";
            public const string N1 = "N1";
            public const string N2 = "N2";
            public const string N3 = "N3";
            public const string N4 = "N4";
            public const string N5 = "N5";

            public const string N1_CustomDigit = "###,###,###,##0.#";
            public const string N2_CustomDigit = "###,###,###,##0.##";
            public const string N3_CustomDigit = "###,###,###,##0.###";

        }

        public struct TableName
        {
            public const string MsLevels = "MsLevels";
            public const string MsBusRoutes = "MsBusRoutes";
            public const string MsDepartments = "MsDepartments";
            public const string MsUsers = "MsUsers";
            public const string MsGroupsAssign = "MsGroupsAssign";
            public const string MsUserGroups = "MsUserGroups";
            public const string MsPermissionsAssign = "MsPermissionsAssign";
            public const string MsCompanyInfo = "MsCompanyInfo";
            public const string MsOffices = "MsOffices";
            public const string MsOperations = "MsOperations";
            public const string MsSections = "MsSections";
            public const string MsTeams = "MsTeams";
            public const string MsRoutes = "MsRoutes";
            public const string MsReasons = "MsReasons";
            public const string MsShifts = "MsShifts";
            public const string MsRotateShiftSetting = "MsRotateShiftSetting";
            public const string MsRotateScheduledSetting = "MsRotateScheduledSetting";
            public const string MsFingerMachines = "MsFingerMachines";
            public const string MsCalendars = "MsCalendars";
            public const string MsLeaveCategory = "MsLeaveCategory";
            public const string MsInsRegisterOrgs = "MsInsRegisterOrgs";
            public const string MsHospitals = "MsHospitals";
            public const string MsSkills = "MsSkills";
            public const string MsRanks = "MsRanks";
            public const string MsClasses = "MsClasses";
            public const string MsPITCodes = "MsPITCodes";
            public const string MsCareers = "MsCareers";
            public const string MsPositions = "MsPositions";
            public const string MsAreas = "MsAreas";
            public const string MsProjects = "MsProjects";
            public const string MsAllowances = "MsAllowances";
            public const string MsFormOfWages = "MsFormOfWages";
            public const string MsFormOfWagesDetail = "MsFormOfWagesDetail";
            public const string MsDeductions = "MsDeductions";
            public const string MsSalaryPeriods = "MsSalaryPeriods";
            public const string MsDormitorys = "MsDormitorys";
            public const string MsParameters = "MsParameters";
            public const string HrEmployees = "HrEmployees";
            public const string HrEmpSkills = "HrEmpSkills";
            public const string HrEmpEducationQualifications = "HrEmpEducationQualifications";
            public const string HrEmpExperiences = "HrEmpExperiences";
            public const string HrEmpFamilies = "HrEmpFamilies";
            public const string HrEmpSalary = "HrEmpSalary";
            public const string HrEmpAllowances = "HrEmpAllowances";
            public const string HrLabourContracts = "HrLabourContracts";
            public const string HrTraining = "HrTraining";
            public const string HrDisclipinesAwards = "HrDisclipinesAwards";
            public const string HrOTRegisPostnatal = "HrOTRegisPostnatal";
            public const string HrRotateAssignment = "HrRotateAssignment";
            public const string HrManagerAssignment = "HrManagerAssignment";
            public const string HrAllowanceAssignment = "HrAllowanceAssignment";
            public const string HrFrmOfWagesAssignment = "HrFrmOfWagesAssignment";
            public const string HrAnualLeave = "HrAnualLeave";
            public const string HrLeave = "HrLeave";
            public const string HrTransportRegistration = "HrTransportRegistration";
            public const string AtLeavePlanning = "AtLeavePlanning";
            public const string AtInOut = "AtInOut";
            public const string AtAttendanceErrors = "AtAttendanceErrors";
            public const string AtWorkingScheduling = "AtWorkingScheduling";
            public const string AtTimeSheet = "AtTimeSheet";
            public const string AtTimeSheetHistories = "AtTimeSheetHistories";
            public const string PrTimesheetSummary = "PrTimesheetSummary";
            public const string PrSalary = "PrSalary";
            public const string PrAllowances = "PrAllowances";
            public const string PrDeductions = "PrDeductions";
            public const string PrPeriodLockDetail = "PrPeriodLockDetail";
            public const string AtLately_Early = "AtLately_Early";
            public const string AtPlanning = "AtPlanning";
            public const string BsDormitoryRegistration = "BsDormitoryRegistration";
        }

        public struct TimeFormat
        {
            public const string hhmmss = "hh:mm:ss";
            public const string HHmmss = "HH:mm:ss";
            public const string hhmm = "hh:mm";
            public const string HHmm = "HH:mm";
        }

        public struct CapacityImage
        {
            public const int Size = 301156;
        }

        public struct FileType
        {
            public const string Xls = "xls";
            public const string Xlsx = "xlsx";
            public const string Pdf = "pdf";
            public const string Csv = "csv";
            public const string Txt = "txt";
            public const string Html = "html";
        }

        public struct ApplyType
        {
            public const string JoinedDate = "1";
            public const string StartedDate = "2";
        }

        public struct FileName
        {
            public const string Import_Allowances = "Import_Allowances";
            public const string Import_Deductions = "Import_Deductions";
            public const string Import_SalaryPeriod = "Import_SalaryPeriod";
            public const string Import_RotateScheduleSetting = "Import_RotateScheduleSetting";
            public const string Export_MoneyTransfer = "Export_MoneyTransfer.xls";
            public const string Import_InOut = "Import_InOut";
            public const string Import_Calendar = "Import_Calendar";
            public const string Export_EmployeeCard = "Export_EmployeeCard.xls";
            public const string Export_Contract = "Export_Contract.xls";
            public const string Import_Staff = "Import_Staff.xls";

            public const string COUNTING_SHEET = "COUNTING_SHEET.xls";
            public const string DAILY_INVENTORY = "DAILY_INVENTORY.xls";
            public const string DELIVERY_SHEET_EXTERNAL = "DELIVERY_SHEET_EXTERNAL.xls";
            public const string DELIVERY_SHEET_INTERNAL = "DELIVERY_SHEET_INTERNAL.xls";
            public const string DESTROYED_MINUTES = "DESTROYED_MINUTES.xls";
            public const string MONTHLY_INVENTORY_REPORT = "MONTHLY_INVENTORY_REPORT.xls";
            public const string RECYCLE_MINUTES = "RECYCLE_MINUTES.xls";
            public const string WAREHOUSE_INVENTORY = "WAREHOUSE_INVENTORY.xls";
            public const string WAREHOUSE_INVENTORY_MINUTES = "WAREHOUSE_INVENTORY_MINUTES.xls";

            public const string EXPORT_ALUMINUM_TEMPLATE = "BRT_EXPORT_ALUMINUM_TEMPLATE";
            public const string EXPORT_STOCKAGING_TEMPLATE = "BRT_EXPORT_STOCKAGING_TEMPLATE";
            public const string EXPORT_SWITCH_TEMPLATE = "BRT_EXPORT_SWITCH_TEMPLATE";
            public const string BRT_EXPORT_NGNCRATE_TEMPLATE = "BRT_EXPORT_NGNCRATE_TEMPLATE";
            public const string BRT_EXPORT_INVTRANLIST_TEMPLATE = "BRT_EXPORT_INVTRANLIST_TEMPLATE";

            public const string BRT_EXPORT_ResultDailyDelivery_TEMPLATE = "BRT_EXPORT_ResultDailyDelivery_TEMPLATE";

            public const string BRT_EXPORT_DailyDeliverySchedule_TEMPLATE = "BRT_EXPORT_DailyDeliverySchedule_TEMPLATE";
            public const string BRT_EXPORT_DailyDeliveryScheduleInternal_TEMPLATE = "BRT_EXPORT_DailyDeliveryScheduleInternal_TEMPLATE";

            //BRT_EXPORT_DESTROY_MINUTES_TEMPLATE.xls & BRT_EXPORT_RECYCLE_MINUTES_TEMPLATE.xls
            public const string BRT_EXPORT_DESTROY_MINUTES_TEMPLATE = "BRT_EXPORT_DESTROY_MINUTES_TEMPLATE";
            public const string BRT_EXPORT_RECYCLE_MINUTES_TEMPLATE = "BRT_EXPORT_RECYCLE_MINUTES_TEMPLATE";
        }

        public struct SheetName
        {
            public const string Allowances = "Allowance";
            public const string Deductions = "Deduction";
            public const string SalaryPeriod = "Salary Period";
            public const string RotateShiftSetting = "RotateShiftSetting ";
            public const string InOut = "InOut";
            public const string Calendar = "Calendar";
            public const string Staff = "Staff";
        }

        public struct SystemData
        {
            public const string Yes = "1";
            public const string No = "0";
        }

        public struct SeniorityType
        {
            public const string AnualLeave = "1";
            public const string Seniority = "2";
        }

        public struct CalendarDateType
        {
            public const string WorkDay = "1";
            public const string Holiday = "2";
            public const string Weekend = "3";
            public const string Festival = "4";
        }

        public struct EmployeeStatus
        {
            public const string NormalWorking = "1";
            public const string Probation = "2";
            public const string BeforeMaternity = "3";
            public const string Maternity = "4";
            public const string AfterMaternity = "5";
            public const string Tranning = "6";
            public const string BusinessTrip = "7";
            public const string Terminate = "8";
            public const string HealthCare = "9";
            public const string WorkingAccident = "10";
        }

        public struct EmployeeWorkType
        {
            public const string FullTime = "1";
            public const string PartTime = "2";
        }

        public struct State
        {
            public const string Unchanged = "0";
            public const string Add = "1";
            public const string Update = "2";
            public const string Delete = "3";
        }

        public struct AppState
        {
            public const string Lock = "1";
            public const string Unlock = "2";
        }

        public struct DeductionType
        {
            public const string SocialInsurance = "0";
            public const string HealthInsurance = "1";
            public const string JoblessInsurance = "2";
            public const string PIT = "3";
            public const string UnionDues = "4";
            public const string Other = "5";
        }

        public struct ReasonType
        {
            public const string Leave = "1";
            public const string LatelyIn = "2";
            public const string EarlyOut = "3";
            public const string Termination = "4";
            public const string PrivateOut = "5";
        }

        public struct PeriodStatus
        {
            public const string LockMonthly = "1";
            public const string UnlockMonthly = "0";
            public const string OpenMonthly = "2";
            public const string CloseMonthly = "3";
            public const string OpenPayroll = "4";
            public const string ClosePayroll = "5";
        }

        //public struct PeriodStatusEx
        //{
        //    public const string LockDaily = "1";
        //    public const string UnlockDaily = "0";
        //    public const string OpenMonthly = "2";
        //    public const string CloseMonthly = "3";
        //    public const string OpenPayroll = "4";
        //    public const string ClosePayroll = "5";
        //}

        public enum PeriodAction
        {
            Load,
            Unlock,
            Lock,
            CloseMonthly,
            ClosePayroll,
            OpenMonthly,
            OpenPayroll,
            CreateMonthly,
            CreatePayroll
        }

        public struct PeriodCloseMonthlyTSStatus
        {
            public const string Close = "1";
            public const string Open = "0";
        }

        public struct TimeSheetStatus
        {
            public const string Unknown = "0";
            public const string Planned = "1";
            public const string Processed_OK = "2";
            public const string Processed_Err = "3";
            public const string Locked = "4";
        }

        /// <summary>
        /// 0: Successful
        /// 1: Access denied, login to database fail(invalid username, invalid password)
        /// 2: Invalid host, cannot find server(host) that set in app config file
        /// 3: Invalid database, cannot find database that set in DbConfig file
        /// 4: Lost connection, cannot connect to database because lost connection
        /// 5: Duplicate key: insert Primary Key or Unique Key that already exist in database
        /// 6: Forgeign key not exist: insert a foreign key that not exist in primary key
        /// 7: Foreign Key Violation: Foreign Key Violation (delete primary key that is foreign key in other table)
        /// 8: Data not found: Delete or Update data that not exist in database
        /// 9: Exception occured: other exception
        /// </summary>
        public struct DbReturnCode
        {
            public const int Succeed = 0;
            public const int AccessDenied = 1;
            public const int InvalidHost = 2;
            public const int InvalidDatabase = 3;
            public const int LostConnection = 4;
            public const int DuplicateKey = 5;
            public const int ForgeignKeyNotExist = 6;
            public const int ForeignKeyViolation = 7;
            public const int DataNotFound = 8;
            public const int ExceptionOccured = 9;
            public const int DeadlockFound = 10;
            public const int LockWaitTimeoutExceeded = 11;
            public const int ConcurrencyViolation = 12;
            //public const int StockPeriodTypeNotExist = 36;
        }

        public struct WarehouseReturnCode
        {
            public const int Succeed = 0;
            public const int AccessDenied = 1;
            public const int InvalidHost = 2;
            public const int InvalidDatabase = 3;
            public const int LostConnection = 4;
            public const int DuplicateKey = 5;
            public const int ForgeignKeyNotExist = 6;
            public const int ForeignKeyViolation = 7;
            public const int DataNotFound = 8;
            public const int ExceptionOccured = 9;
            public const int DeadlockFound = 10;
            public const int LockWaitTimeoutExceeded = 11;
            public const int ConcurrencyViolation = 12;
            public const int HavingStock = 13;
            public const int HavingCountsheet = 14;
            //public const int StockPeriodTypeNotExist = 36;
        }

        /// <summary>
        /// 0: Successful
        /// 1: Access denied, login to database fail(invalid username, invalid password)
        /// 2: Invalid host, cannot find server(host) that set in app config file
        /// 3: Invalid database, cannot find database that set in DbConfig file
        /// 4: Lost connection, cannot connect to database because lost connection
        /// 5: Duplicate key: insert Primary Key or Unique Key that already exist in database
        /// 6: Forgeign key not exist: insert a foreign key that not exist in primary key
        /// 7: Foreign Key Violation: Foreign Key Violation (delete primary key that is foreign key in other table)
        /// 8: Data not found: Delete or Update data that not exist in database
        /// 9: Exception occured: other exception
        /// </summary>
        public struct PhysicalReturnCode
        {
            public const int Succeed = 0;
            public const int AccessDenied = 1;
            public const int InvalidHost = 2;
            public const int InvalidDatabase = 3;
            public const int LostConnection = 4;
            public const int DuplicateKey = 5;
            public const int ForgeignKeyNotExist = 6;
            public const int ForeignKeyViolation = 7;
            public const int DataNotFound = 8;
            public const int ExceptionOccured = 9;
            public const int DeadlockFound = 10;
            public const int LockWaitTimeoutExceeded = 11;
            public const int ConcurrencyViolation = 12;
            public const int StockPeriodTypeNotExist = 36;
            public const int CountSheetComplete = 13;
            public const int CountSheetCancel = 14;
        }

        public struct InvoiceReturnCode
        {
            public const int Succeed = 0;
            public const int AccessDenied = 1;
            public const int InvalidHost = 2;
            public const int InvalidDatabase = 3;
            public const int LostConnection = 4;
            public const int DuplicateKey = 5;
            public const int ForgeignKeyNotExist = 6;
            public const int ForeignKeyViolation = 7;
            public const int DataNotFound = 8;
            public const int ExceptionOccured = 9;
            public const int DeadlockFound = 10;
            public const int LockWaitTimeoutExceeded = 11;
            public const int ConcurrencyViolation = 12;
            public const int QuantityNotEnough = 13;
        }

        public struct TSBatchReturnCode
        {
            public const int Succeed = 0;
            public const int ClosedPeriod = 50;
            public const int NotExistedPeriod = 51;
            public const int GreaterThanCurrentPeriod = 52;
        }

        /// <summary>
        /// 0: Successful
        /// 1: Access denied, login to database fail(invalid username, invalid password)
        /// 2: Invalid host, cannot find server(host) that set in app config file
        /// 3: Invalid database, cannot find database that set in DbConfig file
        /// 4: Lost connection, cannot connect to database because lost connection
        /// 5: Duplicate key: insert Primary Key or Unique Key that already exist in database
        /// 6: Forgeign key not exist: insert a foreign key that not exist in primary key
        /// 7: Foreign Key Violation: Foreign Key Violation (delete primary key that is foreign key in other table)
        /// 8: Data not found: Delete or Update data that not exist in database
        /// 9: Exception occured: other exception
        /// </summary>
        public struct StockReturnCode
        {
            public const int Succeed = 0;
            public const int AccessDenied = 1;
            public const int InvalidHost = 2;
            public const int InvalidDatabase = 3;
            public const int LostConnection = 4;
            public const int DuplicateKey = 5;
            public const int ForgeignKeyNotExist = 6;
            public const int ForeignKeyViolation = 7;
            public const int DataNotFound = 8;
            public const int ExceptionOccured = 9;
            public const int DeadlockFound = 10;
            public const int LockWaitTimeoutExceeded = 11;
            public const int ConcurrencyViolation = 12;

            public const int StockOutLack = 13;
            public const int StockOutEnough = 14;
            public const int StockMinusOk = 15;
            public const int StockMinusNg = 16;
            public const int StockWareHouse2NotExist = 17;
            public const int StockItemCodeNull = 18;
            public const int CurrentStockLessThanStockOut = 19;
            public const int StockOutLackOfNextPeriod = 36;
            public const int StockMinusNgOfNextPeriod = 37;
            public const int RelatedDocumentQtyValid = 39;
            public const int RelatedDocumentQtyNotValid = 40;

            public const int RelatedDocumentDateValid = 43;
            public const int RelatedDocumentDateNotValid = 44;

            #region Data Validate

            public const int StockDocumnetNoNull = 20;
            public const int StockShippingDateNoNull = 21;
            public const int StockSupplierNoNull = 22;
            public const int StockArrivingDateNoNull = 23;
            public const int StockCustomerNoNull = 24;
            public const int StockWareHouse2NoNull = 25;
            public const int StockCancelSuccess = 26;
            public const int StockPrintConfirm = 27;
            public const int StockSafetyDangerous = 28;
            public const int StockPeriodOpen = 29;
            public const int StockPeriodClose = 30;
            public const int StockPeriodLock = 36;
            public const int StockPeriodNotExist = 31;
            public const int StockCancelFail = 32;
            public const int StockProductionLineNotExist = 33;
            public const int StockQualityStatusNotExist = 34;
            public const int StockStockTypeNotValid = 35;
            public const int StockSafetyDangerousOfNextPeriod = 38;

            public const int CurrentStockEqualModifyStock = 41;

            public const int StockCancelRelatedFail = 42;
            public const int DocumentCanceled = 45;

            public const int StockCancelExistInvoice = 46;
            public const int StockCancelAging = 47;

            #endregion Data Validate
        }

        /// <summary>
        /// 0: Successful
        /// 1: Access denied, don't have authority to access file
        /// 3: File name is invalid
        /// 4: File format is invalid
        /// 5: Data in file is invalid
        /// 6: System error
        /// </summary>
        public struct IOReturnCode
        {
            public const int Succeed = 0;
            public const int AccessDenied = 1;
            public const int FileNotFound = 2;
            public const int FileNameInValid = 3;
            public const int FileFormatInValid = 4;
            public const int FileTypeInValid = 5;
            public const int DataInValid = 6;
            public const int SystemError = 7;
            public const int CancelImport = 8;
            public const int PrinterInValid = 9;
            public const int RPCServerUnavailable = 10;
            public const int CancelExport = 11;
        }

        public struct TimeSheetReturnCode
        {
            public const int Succeed = 0;
            public const int AccessDenied = -1;
            public const int PreviousPeriodNotClosed = -2;
            public const int ExistAttendanceError = -3;
        }

        public struct ImportReturnCode
        {
            public const int Succeed = 0;
            public const int PathIsEmpty = 1;
            public const int PathNotExist = 2;
            public const int FileFormatInvalid = 3;
            public const int NumFieldInvalid = 4;
            public const int NumRowInvalid = 5;
            public const int DataNotFound = 6;
            public const int SystemError = 7;
            public const int NotYetWorkingSchedule = 8;
        }

        public struct PayrollReturnCode
        {
            public const int Succeed = 0;
            public const int PreviousPayrollNotClosed = 31;
            public const int ExistedAttendanceError = 32;
            public const int IsNotUnlockedTSDaily = 33;
            public const int IsNotLockedTSDaily = 34;
            public const int IsNotOpenedTSMonthly = 35;
            public const int IsNotClosedTSMonthly = 36;
            public const int IsNotOpenedPayroll = 37;
            public const int IsNotClosedPayroll = 38;
            public const int IsUnLockedTSDaily = 39;
            public const int CannotCalculatePayroll = 40;
        }

        public struct PayrollReturnCodeEx
        {
            public const int Succeed = 0;
            public const int DataNotExist = -1;
        }

        public struct NumOfColumnImport
        {
            public const int Hrem00 = 71;
            public const int Msrs00 = 3;
            public const int Mssp00 = 4;
            public const int Msca00 = 3;
            public const int Atio00 = 6;
            public const int Atae00 = 9;
            public const int Atie00 = 8;
            public const int Atot00 = 5;

            //change HREMSalary 5 to 3 column: EmployeeID, apply from, base salary
            public const int HremSalary = 4;

            public const int PrmpAllowance = 3;
            public const int PrmpDeduction = 3;
            public const int WorkingSchedule = 7;
            public const int BusinessTrip = 5;
            public const int LeavePlanning = 7;
            public const int Maternity = 10;
            public const int WorkingScheduleParttime = 15;
            public const int Hrra00 = 10;
            public const int OTSchedule = 5;

            public const int ItemPrice = 6;
            public const int ItemCosting = 2;
        }

        /// <summary>
        ///  Using to set text for caption of button
        /// </summary>
        public struct ButtonCaption
        {
            public const string Next = "MasterEditForm_Next";
            public const string SaveAndNext = "MasterEditForm_SaveAndNext";
            public const string Edit = "MasterEditForm_Edit";
            public const string Reset = "MasterEditForm_Reset";
            public const string Clear = "MasterEditForm_Clear";
        }

        //public struct DayOfWeek
        //{
        //    public const int Sunday = 0;
        //    public const int Monday = 1;
        //    public const int Tuesday = 2;
        //    public const int Wednesday = 3;
        //    public const int Thursday = 4;
        //    public const int Friday = 5;
        //    public const int Saturday = 6;
        //}
        /// <summary>
        ///  Present the result of object validation
        /// </summary>
        public enum IsValid
        {
            Successful,
            Failed
        }

        /// <summary>
        ///  Present the result of checking authority
        /// </summary>
        public enum IsAuthority
        {
            Allow,
            Deny
        }

        /// <summary>
        ///  1: Insert 2:Update 3:Delete
        /// </summary>
        public struct Action
        {
            public const string None = "0";
            public const string Insert = "1";
            public const string Update = "2";
            public const string Delete = "3";
            public const string Import = "4";
            public const string Login = "5";
            public const string Logout = "6";

            public const string Opening = "7";
            public const string Closing = "8";
            public const string Cancel = "9";

            public const string StockShippingForDelivery = "10";
            public const string ReturnDeliveryToSupplier = "11";
            public const string ScrappingFromNG = "12";
            public const string StockShippingAdjustment = "13";
            public const string StockArrivingForPurchaseOrder = "14";
            public const string ReturnFromCustomerShipping = "15";
            public const string StockArrivingAdjustment = "16";
            public const string ShippingFromStorage = "17";
            public const string ArrivingInStorage = "18";
            public const string NGRecycledMaterials = "19";
            public const string TransferForProductionLine = "20";
            public const string TransferPosting = "21";
            public const string PhysicalInventory = "22";
            public const string Adjust = "23";
        }

        /// <summary>
        ///  Set the code of parameters in system
        /// </summary>
        public struct Code
        {
            public const string PeriodStatus = "1000000010";
            public const string Status = "1000000008";
            public const string LeaveType = "1000000064";//1000000002
            public const string AllowanceType = "1000000034";
            public const string ApprovalType = "1000000022";
            public const string Weekday = "1000000012";
            public const string CalendarType = "1000000041";
            public const string ReasonType = "1000000007";
            public const string DeductionType = "1000000042";
            public const string Gender = "1000000020";
            public const string IO = "1000000058";
            public const string TSRoundOffMthod = "1000000011";
            public const string OTRoundOffMthod = "1000000011";
            public const string HrlyRateMethod = "1000000047";
            public const string TransferType = "1000000013";
            public const string CalcMethod = "1000000014";
            public const string Language = "1000000027";
            public const string ContrType = "1000000052";
            public const string Nationality = "1000000001";
            public const string EmployeeStatus = "1000000040";
            public const string EmployeeStatus2 = "1000000060";
            public const string Ethnic = "1000000038";
            public const string Religion = "1000000039";
            public const string MaritalStatus = "1000000053";
            public const string Education = "1000000049";
            public const string Calculateby = "1000000025";
            public const string OTType = "1000000005";
            public const string RNationality = "1000000001";
            public const string WrkType = "1000000004";
            public const string WrkType2 = "1000000061";

            //public const string WorkingType = "1000000003";
            public const string Action = "1000000035";

            public const string TimeRoundOff = "1000000011";
            public const string ERRMessage = "1000000044";
            public const string DisPunishment = "1000000018";
            public const string ObjType = "1000000015";
            public const string Gender2 = "1000000021";
            public const string MethodOfPay = "1000000054";
            public const string FingerType = "1000000062";
            public const string EmpType = "1000000063";
            public const string WorkingType = "1000000064";
            public const string TimeSheetType = "1000000065";
            public const string WorkCategory = "1000000066";
            public const string AnualLeaveType = "1000000067";
            public const string Status_Active = "1000000114";
            public const string LogonState = "1000000115";

            #region Inventory

            public const string ItemType = "1000000101";
            public const string Currency = "1000000102";
            public const string UnitType = "1000000103";
            public const string WarehouseType = "1000000104";
            public const string InventoryPeriodType = "1000000105";
            public const string InventoryPeriodStatus = "1000000106";
            public const string TransactionCategory = "1000000107";
            public const string QualityStatus = "1000000108";
            public const string TransferStatusType = "1000000109";
            public const string PhysicalInventoryStatus = "1000000110";
            public const string StockType = "1000000111";
            public const string DeliverySheetPrintFlg = "1000000112";
            public const string CancelFlag = "1000000113";
            public const string InvoiceStatus = "1000000116";
            public const string SampleShippingFlg = "1000000117";

            #endregion Inventory

            #region Fixed Asset

            public const string FixedAssetReasonType = "1000000200";
            public const string FixedAssetTransactionType = "1000000201";
            public const string FixedAssetStatus = "1000000202";
            public const string FAClosingStatus = "1000000203";

            #endregion
        }

        #region Quanlity Status

        public struct QualityStatus
        {
            public const string OK = "1";
            public const string QI = "2";
            public const string NC = "3";
            public const string NG = "4";
        }

        #endregion Quanlity Status

        #region Quanlity Status

        public struct TransferStatusType
        {
            public const string OK2NG = "1";
            public const string OK2NC = "2";
            public const string OK2QI = "3";
            public const string QI2OK = "4";
            public const string QI2NC = "5";
            public const string QI2NG = "6";
            public const string NC2NG = "7";
            public const string NC2OK = "8";
        }

        #endregion Quanlity Status

        #region OperId

        /// <summary>
        ///  Return OperId in sy_operas table
        /// </summary>
        public struct OperId
        {
            public const string View = "1";
            public const string Add = "2";
            public const string Edit = "3";
            public const string Delete = "4";
            public const string Import = "5";
            public const string Export = "6";
            public const string Print = "7";
            public const string Approve = "13";
            public const string Open = "8";
            public const string Lock = "9";
            public const string Close = "10";
            public const string Backup = "11";
            public const string Restore = "12";
            public const string All = "99";

            public const string StockShippingForDelivery = "21";
            public const string ReturnDeliveryToSupplier = "22";
            public const string ScrappingFromNG = "23";
            public const string StockShippingAdjustment = "24";
            public const string StockArrivingForPurchaseOrder = "25";
            public const string ReturnFromCustomerShipping = "26";
            public const string StockArrivingAdjustment = "27";
            public const string ShippingFromStorage = "28";
            public const string ArrivingInStorage = "29";
            public const string NGRecycledMaterials = "30";
            public const string TransferForProductionLine = "31";
            public const string TransferPosting = "32";
            public const string Shipping_ArrivingInquiry = "33";
            public const string InventoryInquiry = "34";
            public const string StockModify = "35";
            public const string PhysicalInventory = "36";
            public const string ViewPOPrice = "37";
            public const string InputPOPrice = "38";
            public const string ViewSOPrice = "41";
            public const string InputSOPrice = "42";
            public const string CheckInventory = "39";
            public const string Cancel = "40";
            public const string ViewCostPrice = "43";
            public const string InputCostPrice = "44";
        }

        #endregion OperId

        #region FunctionId

        /// <summary>
        ///  Return name of forms in system
        /// </summary>
        public struct FunctionId
        {
            //Function ID of Login Form
            public const string Login = "Login";

            //Function ID of Master Form
            public const string MasterReport = "MasterReport";

            public const string MasterEdit = "MasterEdit";
            public const string MasterSearch = "MasterSearch";

            //Function ID of Trans Form
            public const string TransEdit = "TransEdit";

            public const string TransSearch = "TransSearch";

            //Function ID of Master Chird Form
            public const string Msaa00 = "MSAA00";

            public const string Msaa01 = "MSAA01";
            public const string Msae00 = "MSAE00";
            public const string Msae01 = "MSAE01";
            public const string Msal00 = "MSAL00";
            public const string Msal01 = "MSAL01";
            public const string Msap00 = "MSAP00";
            public const string Msap01 = "MSAP01";
            public const string Msar00 = "MSAR00";
            public const string Msar01 = "MSAR01";
            public const string Msca00 = "MSCA00";
            public const string Msca01 = "MSCA01";
            public const string Mscp00 = "MSCP00";
            public const string Mscr00 = "MSCR00";
            public const string Mscr01 = "MSCR01";
            public const string Msdd00 = "MSDD00";
            public const string Msdd01 = "MSDD01";
            public const string Msde00 = "MSDE00";
            public const string Msde01 = "MSDE01";
            public const string Msfm00 = "MSFM00";
            public const string Msfm01 = "MSFM01";
            public const string Mshp00 = "MSHP00";
            public const string Mshp01 = "MSHP01";
            public const string Msir00 = "MSIR00";
            public const string Msir01 = "MSIR01";
            public const string Msit00 = "MSIT00";
            public const string Msit01 = "MSIT01";
            public const string Msjf00 = "MSJF00";
            public const string Msjf01 = "MSJF01";
            public const string Mslc00 = "MSLC00";
            public const string Mslc01 = "MSLC01";
            public const string Msof00 = "MSOF00";
            public const string Msof01 = "MSOF01";
            public const string Msop00 = "MSOP00";
            public const string Msop01 = "MSOP01";
            public const string Mspa00 = "MSPA00";
            public const string Mspo00 = "MSPO00";
            public const string Mspo01 = "MSPO01";
            public const string Mspr00 = "MSPR00";
            public const string Mspr01 = "MSPR01";
            public const string Mspw00 = "MSPW00";
            public const string Msre00 = "MSRE00";
            public const string Msre01 = "MSRE01";
            public const string Msro00 = "MSRO00";
            public const string Msro01 = "MSRO01";
            public const string Msrs00 = "MSRS00";
            public const string Msrt00 = "MSRT00";
            public const string Msrt01 = "MSRT01";
            public const string Msse00 = "MSSE00";
            public const string Msse01 = "MSSE01";
            public const string Mssh00 = "MSSH00";
            public const string Mssh01 = "MSSH01";
            public const string Mssi00 = "MSSI00";
            public const string Mssi01 = "MSSI01";
            public const string Mssk00 = "MSSK00";
            public const string Mssk01 = "MSSK01";
            public const string Msrk00 = "MSRK00";
            public const string Msrk01 = "MSRK01";
            public const string Mscs00 = "MSCS00";
            public const string Mscs01 = "MSCS01";
            public const string Mssp00 = "MSSP00";
            public const string Mste00 = "MSTE00";
            public const string Mste01 = "MSTE01";
            public const string Msug00 = "MSUG00";
            public const string Msug01 = "MSUG01";
            public const string Msur00 = "MSUR00";
            public const string Msur01 = "MSUR01";
            public const string Mssj00 = "MSSJ00";
            public const string Msbr00 = "MSBR00";
            public const string Msbr01 = "MSBR01";
            public const string Msdo00 = "MSDO00";
            public const string Msdo01 = "MSDO01";
            public const string Mswa00 = "MSWA00";
            public const string Mswa01 = "MSWA01";
            public const string Msle00 = "MSLE00";
            public const string Msle01 = "MSLE01";

            // Function ID of HumanResource Chird Form
            public const string Hrce00 = "HRCE00";

            public const string Hrem00 = "HREM00";
            public const string Hrem01 = "HREM01";
            public const string Hrct00 = "HRCT00";
            public const string Hrct01 = "HRCT01";
            public const string Hrra00 = "HRRA00";
            public const string Hrra01 = "HRRA01";
            public const string Hrtm00 = "HRTM00";
            public const string Hrtm01 = "HRTM01";
            public const string Hrsa00 = "HRSA00";
            public const string Hrsa01 = "HRSA01";
            public const string Hrda00 = "HRDA00";
            public const string Hrda01 = "HRDA01";
            public const string Hris00 = "HRIS00";
            public const string Hris01 = "HRIS01";
            public const string Hrec00 = "HREC00";
            public const string Hrmr00 = "HRMR00";
            public const string Hrmr01 = "HRMR01";
            public const string Hrot00 = "HROT00";
            public const string Hrot01 = "HROT01";
            public const string Hrlp00 = "HRLP00";
            public const string Hrla00 = "HRLA00";
            public const string Hrli00 = "HRLI00";
            public const string Hrma00 = "HRMA00";
            public const string Hraa00 = "HRAA00";
            public const string Hrwa00 = "HRWA00";

            // Function ID of Attendance Chird Form
            public const string Atsa00 = "ATSA00";

            public const string Atsd00 = "ATSD00";
            public const string Atsd01 = "ATSD01";
            public const string Atsd02 = "ATSD02";
            public const string Atsp00 = "ATSP00";
            public const string Atsp01 = "ATSP01";
            public const string Atsp02 = "ATSP02";
            public const string Atls00 = "ATLS00";
            public const string Atls01 = "ATLS01";
            public const string Atbt00 = "ATBT00";
            public const string Atbt01 = "ATBT01";
            public const string Atio00 = "ATIO00";
            public const string Atle00 = "ATLE00";
            public const string Atle01 = "ATLE01";
            public const string Atie00 = "ATIE00";
            public const string Atae00 = "ATAE00";
            public const string Atae01 = "ATAE01";
            public const string Atas00 = "ATAS00";
            public const string Atts00 = "ATTS00";
            public const string Atts01 = "ATTS01";
            public const string Atts02 = "ATTS02";
            public const string Atts03 = "ATTS03";
            public const string Atts04 = "ATTS04";
            public const string Atts05 = "ATTS05";
            public const string Atot00 = "ATOT00";
            public const string Timsheetprocessing = "TIMESHEETPROCESSING";

            // Function ID of Payroll Chird Form
            public const string Prmp00 = "PRMP00";

            public const string Prmp01 = "PRMP01";
            public const string Prts00 = "PRTS00";

            // Function ID of Business Chird Form
            public const string Bstr00 = "BSTR00";

            public const string Bstr01 = "BSTR01";
            public const string Bsdo00 = "BSDO00";
            public const string Bsdo01 = "BSDO01";

            // Function ID of sy_function table
            public const string Syfunctions = "SY_FUNCTIONS";

            public const string SYBK00 = "SYBK00";
            public const string SYBK01 = "SYBK01";
            public const string SYRT00 = "SYRT00";

            // Function ID of import table
            public const string ImportForm = "Import";

            // Function ID of import table
            public const string ReasonForm = "Reason";

            public const string OpenClose_UserStateForm = "OpenClose_UserStateForm";

            public const string WorkTimeInput = "WorkTimeInput";

            // Function ID of processing form
            public const string ProcessingForm = "ProcessingForm";

            // Function ID of Report Chird Form
            public const string RPA000 = "RPA000";

            public const string RPH000 = "RPH000";
            public const string RPP000 = "RPP000";
            public const string Rtito00 = "RTITO00";
            public const string Rpio00 = "RPIO00";
            public const string Rptm00 = "RPTM00";
            public const string Rpba00 = "RPBA00";

            public const string Rppr00 = "RPPR00";
            public const string Rpps00 = "RPPS00";
            public const string Rpsa00 = "RPSA00";
            public const string Rpsu00 = "RPSU00";
            public const string RpsuAll00 = "RPSUALL00";

            public const string TSP = "TSP";

            #region Master

            public const string MS_InventoryPeriods_Search = "MS_Warehouses_Search";
            public const string MS_InventoryPeriods_Edit = "MS_Warehouses_Edit";

            public const string MS_Warehouses_Search = "MS_Warehouses_Search";
            public const string MS_Warehouses_Edit = "MS_Warehouses_Edit";

            public const string MS_Vendor_Search = "MS_Vendor_Search";
            public const string MS_Vendor_Edit = "MS_Vendor_Edit";

            public const string MS_ProductionLine_Search = "MS_ProductionLine_Search";
            public const string MS_ProductionLine_Edit = "MS_ProductionLine_Edit";

            public const string MS_Department_Edit = "MS_Department_Edit";
            public const string MS_Department_Search = "MS_Department_Search";

            public const string MS_Employee_Search = "MS_Employee_Search";
            public const string MS_Employee_Edit = "MS_Employee_Edit";

            public const string MS_UnitConvertion_Search = "MS_UnitConvertion_Search";
            public const string MS_UnitConvertion_Edit = "MS_UnitConvertion_Edit";

            public const string MS_ItemGroup_Search = "MS_ItemGroup_Search";
            public const string MS_ItemGroup_Edit = "MS_ItemGroup_Edit";

            public const string MS_Customer_Search = "MS_Customer_Search";
            public const string MS_Customer_Edit = "MS_Customer_Edit";

            public const string MS_Company_Search = "MS_Company_Search";
            public const string MS_Company_Select = "MS_Company_Select";
            public const string MS_Company_Edit = "MS_Company_Edit";

            public const string MS_Line_Search = "MS_Line_Search";
            public const string MS_Line_Edit = "MS_Line_Edit";

            public const string MS_Unit_Search = "MS_Unit_Search";
            public const string MS_Unit_Edit = "MS_Unit_Edit";

            public const string MS_Factory_Search = "MS_Factory_Search";
            public const string MS_Factory_Edit = "MS_Factory_Edit";

            public const string MS_Supplier_Search = "MS_Supplier_Search";
            public const string MS_Supplier_Edit = "MS_Supplier_Edit";

            public const string MS_Item_Search = "MS_Item_Search";
            public const string MS_Item_Edit = "MS_Item_Edit";

            public const string MS_ErrorReasons_Search = "MS_ErrorReasons_Search";
            public const string MS_ErrorReasons_Edit = "MS_ErrorReasons_Edit";

            public const string MS_CompanyInfo_Edit = "MS_CompanyInfo_Edit";

            public const string MS_WorkRouting_Search = "MS_WorkRouting_Search";
            public const string MS_WorkRouting_Edit = "MS_WorkRouting_Edit";

            public const string MS_Exchrates_Search = "MS_Exchrates_Search";
            public const string MS_Exchrates_Edit = "MS_Exchrates_Edit";

            public const string MS_Capital_Search = "MS_Capital_Search";
            public const string MS_Capital_Edit = "MS_Capital_Edit";

            public const string MS_Acquisitionreason_Search = "MS_Acquisitionreason_Search";
            public const string MS_Acquisitionreason_Edit = "MS_Acquisitionreason_Edit";

            public const string MS_FixedAssetsType_Search = "MS_FixedAssetsType_Search";
            public const string MS_FixedAssetsType_Edit = "MS_FixedAssetsType_Edit";
            #endregion Master

            #region Inventory

            public const string ST_StockOverview_Search = "ST_StockOverview_Search";
            public const string ST_StockOverview_Edit = "ST_StockOverview_Edit";

            public const string ST_SR_StockArivingForPurchaseOrder_Edit = "ST_SR_StockArivingForPurchaseOrder_Edit ";
            public const string ST_SR_ReturnFromCustomerShipping_Edit = "ST_SR_ReturnFromCustomerShipping_Edit";

            public const string ST_SH_StockShippingForDelivery_Search = "ST_SH_StockShippingForDelivery_Search";
            public const string ST_SH_StockShippingForDelivery_Edit = "ST_SH_StockShippingForDelivery_Edit";

            public const string ST_SH_ReturnDeliveryToSupplier_Search = "ST_SH_ReturnDeliveryToSupplier_Search";
            public const string ST_SH_ReturnDeliveryToSupplier_Edit = "ST_SH_ReturnDeliveryToSupplier_Edit";

            public const string ST_SH_StockShippingAdjustment_Search = "ST_SH_StockShippingAdjustment_Search";
            public const string ST_SH_StockShippingAdjustment_Edit = "ST_SH_StockShippingAdjustment_Edit";

            public const string ST_SH_ScrappingFromNG_Search = "ST_SH_ScrappingFromNG_Search";
            public const string ST_SH_ScrappingFromNG_Edit = "ST_SH_ScrappingFromNG_Edit";

            public const string ST_SA_StockArrivingForPurchaseOrder_Search = "ST_SA_StockArrivingForPurchaseOrder_Search";
            public const string ST_SA_StockArrivingForPurchaseOrder_Edit = "ST_SA_StockArrivingForPurchaseOrder_Edit";

            public const string ST_SA_ReturnFromCustomerShipping_Search = "ST_SA_ReturnFromCustomerShipping_Search";
            public const string ST_SA_ReturnFromCustomerShipping_Edit = "ST_SA_ReturnFromCustomerShipping_Edit";

            public const string ST_SA_StockArrivingAdjustment_Search = "ST_SA_StockArrivingAdjustment_Search";
            public const string ST_SA_StockArrivingAdjustment_Edit = "ST_SA_StockArrivingAdjustment_Edit";

            public const string ST_ST_StockTransferShippingFromStorage_Search = "ST_ST_StockTransferShippingFromStorage_Search";
            public const string ST_ST_StockTransferShippingFromStorage_Edit = "ST_ST_StockTransferShippingFromStorage_Edit";

            public const string ST_ST_StockTransferArrivingInStorage_Search = "ST_ST_StockTransferArrivingInStorage_Search";
            public const string ST_ST_StockTransferArrivingInStorage_Edit = "ST_ST_StockTransferArrivingInStorage_Edit";

            public const string ST_ST_NGRecycleMaterial_Search = "ST_ST_NGRecycleMaterial_Search";
            public const string ST_ST_NGRecycleMaterial_Edit = "ST_ST_NGRecycleMaterial_Edit";

            public const string ST_ST_TransferPosting_Search = "ST_ST_TransferPosting_Search";
            public const string ST_ST_TransferPosting_Edit = "ST_ST_TransferPosting_Edit";

            public const string ST_ST_TransferForProductionLine_Search = "ST_ST_TransferForProductionLine_Search";
            public const string ST_ST_TransferForProductionLine_Edit = "ST_ST_TransferForProductionLine_Edit";

            public const string ST_PA_PhysicalAdjustmentInventory_CountSheet = "ST_PA_PhysicalAdjustmentInventory_CountSheet";

            public const string IV_PhysicalInventory_Search = "IV_PhysicalInventory_Search";
            public const string IV_PhysicalIV_CountSheet_AddItem_Edit = "IV_PhysicalIV_CountSheet_AddItem_Edit";
            public const string IV_PhysicalInventory_Edit = "IV_PhysicalInventory_Edit";

            public const string IV_StockModify_Search = "IV_StockModify_Search";
            public const string IV_StockModify_Edit = "IV_StockModify_Edit";

            public const string IV_ReceiptIssueInquiry_Edit = "IV_ReceiptIssueInquiry_Edit";
            public const string IV_ReceiptIssueInquiry_Search = "IV_ReceiptIssueInquiry_Search";

            public const string IV_InventoryInquiry_Search = "IV_InventoryInquiry_Search";
            public const string IV_InventoryInquiry_Edit = "IV_InventoryInquiry_Edit";

            public const string IV_ArrivingIssueInquiry_Search = "IV_ArrivingIssueInquiry_Search";
            public const string IV_ArrivingIssueInquiry_Edit = "IV_ArrivingIssueInquiry_Edit";

            public const string PR_PriceUpdating_Search = "PR_PriceUpdating_Search";
            public const string PR_PriceUpdating_Edit = "PR_PriceUpdating_Edit";

            public const string IV_StockIssueForDeliveryShipping_Edit = "IV_StockIssueForDeliveryShipping_Edit";

            public const string IV_Inventory_Search = "IV_Inventory_Search";
            public const string IV_Inventory_Edit = "IV_Inventory_Edit";

            public const string IV_StockReceive_Search = "IV_StockReceive_Search";

            public const string IV_OpenClose_Edit = "IV_OpenClose_Edit";

            public const string IV_OpenClose_Reopen = "IV_OpenClose_Reopen";

            public const string IV_AdjustmentDate_EditForm = "IV_AdjustmentDate_EditForm";

            #endregion Inventory

            #region Invoice
            public const string INV_Invoice_Search = "INV_Invoice_Search";
            public const string INV_InvoiceSelect_Edit = "INV_InvoiceSelect_Edit";
            public const string INV_Invoice_Edit = "INV_Invoice_Edit";
            #endregion

            #region Anging
            public const string A_CurrentAging_Search = "A_CurrentAging_Search";
            public const string A_CurrentAging_Import = "A_CurrentAging_Import";
            #endregion Anging

            #region Schedule
            public const string SD_MonthlyDelivery_Search = "SD_MonthlyDelivery_Search";
            public const string SD_MonthlyDelivery_Edit = "SD_MonthlyDelivery_Edit";
            #endregion

            #region Cost
            public const string CO_Cost_Export = "CO_Cost_Export";
            public const string CO_Cost_Seach = "CO_Cost_Seach";
            #endregion

            #region System

            public const string SY_Users_Search = "SY_Users_Search";
            public const string SY_Users_Edit = "SY_Users_Edit";

            public const string SY_UserGroups_Search = "SY_UserGroups_Search";
            public const string SY_UserGroups_Edit = "SY_UserGroups_Edit";

            public const string SY_ChangePassword_Search = "SY_ChangePassword_Search";
            public const string SY_ChangePassword_Edit = "SY_ChangePassword_Edit";

            public const string SY_SystemJournals_Search = "SY_SystemJournals_Search";
            public const string SY_SystemJournals_Edit = "SY_SystemJournals_Edit";

            public const string SY_CompanyInfomation_Search = "SY_CompanyInfomation_Search";
            public const string SY_CompanyInfomation_Edit = "SY_CompanyInfomation_Edit";

            public const string SY_Parameters_Search = "SY_Parameters_Search";
            public const string SY_Parameters_Edit = "SY_Parameters_Edit";
            public const string SY_Backup = "SY_Backup";
            public const string SY_Restore = "SY_Restore";

            public const string SYS_SystemJournals = "SYS_SystemJournals";

            public const string SYS_Notifications = "SYS_Notifications";

            //public const string SYS_User_Search = "SYS_User_Search";
            //public const string SYS_User_Edit = "SYS_User_Edit";

            public const string SY_UserState_Search = "SY_UserState_Search";

            #endregion System

            #region Delivery

            public const string DE_MonthlyPlan_Search = "DE_MonthlyPlan_Search";
            public const string DE_DailyDeliverySchedule_Search = "DE_DailyDeliverySchedule_Search";
            public const string DE_MonthlyPlan_Edit = "DE_MonthlyPlan_Edit";
            public const string DE_DailyResult_Export = "DE_DailyResult_Export";

            public const string DE_MonthlyPlanInternal_SearchForm = "DE_MonthlyPlanInternal_SearchForm";
            public const string DE_DailyDeliveryScheduleInternal_Search = "DE_DailyDeliveryScheduleInternal_Search";
            public const string DE_MonthlyPlanInternal_Edit = "DE_MonthlyPlanInternal_Edit";
            public const string DE_DailyResultInternal_Export = "DE_DailyResultInternal_Export";
            #endregion Delivery

            #region Report

            public const string RP_DailyInventory_Search = "RP_DailyInventory_Search";
            public const string RP_MonthlyInventory_Search = "RP_MonthlyInventory_Search";
            public const string RP_WarehouseInventoryMinutes_Search = "RP_WarehouseInventoryMinutes_Search";
            public const string RP_RawMaterialSummary_Search = "RP_RawMaterialSummary_Search";
            public const string RP_MaterialsReportedInMonth_Search = "RP_MaterialsReportedInMonth_Search";
            public const string RP_DestroyedMinutes_Search = "RP_DestroyedMinutes_Search";
            public const string RP_WarehouseVendorInventoryEveryQuarter_Search = "RP_WarehouseVendorInventoryEveryQuarter_Search";
            public const string RP_ArrivingList_Search = "RP_ArrivingList_Search";
            public const string RP_ShippingList_Search = "RP_ShippingList_Search";
            public const string RP_SalesList_Search = "RP_SalesList_Search";
            public const string RP_IncludedWarehouseInventoryMinutes_Search = "RP_IncludedWarehouseInventoryMinutes_Search";
            public const string RP_StockAging_Export = "RP_StockAging_Export";
            public const string RP_NG_NCRate_Export = "RP_NG_NCRate_Export";
            public const string RP_NG_NCRateChart_Search = "RP_NG_NCRateChart_Search";
            public const string RP_NG_NCRateSummary_Search = "RP_NG_NCRateSummary_Search";
            public const string RP_InvTranList_Export = "RP_InvTranList_Export";
            public const string RP_CycleTimeDetail_Search = "RP_CycleTimeDetail_Search";
            public const string RP_SaleForecast_Search = "RP_SaleForecast_Search";

            #endregion Report

            #region Print Sheet Reason Form

            public const string SH_DeliveryPrintConfirmForm_Edit = "SH_DeliveryPrintConfirmForm_Edit";

            #endregion Print Sheet Reason Form

            #region Fixed Assets

            public const string FA_Information_Search = "FA_Information_Search";
            public const string FA_Information_Edit = "FA_Information_Edit";
            public const string FA_Revaluation_Edit = "FA_Revaluation_Edit";

            public const string FA_Decrease_Edit = "FA_Decrease_Edit";
            public const string FA_Increase_Edit = "FA_Increase_Edit";

            public const string FA_Transfer_Edit = "FA_Trasfer_Edit";
            public const string FA_Abandonment_Edit = "FA_Abandonment_Edit";
            public const string FA_MonthlyAdjustment_Edit = "FA_MonthlyAdjustment_Edit";
            public const string FA_DepreciationCalculation_Search = "FA_DepreciationCalculation_Search";
            public const string FA_DepreciationCalculation_Edit = "FA_DepreciationCalculation_Edit";
            public const string FA_DepreciationCalculation_Delete = "FA_DepreciationCalculation_Delete";
            public const string FA_DepreciationCalculation_Add = "FA_DepreciationCalculation_Add";

            public const string FA_DepreciationForecast_Search = "FA_DepreciationForecast_Search";

            #endregion
        }

        #endregion FunctionId

        #region FunctionGroup

        /// <summary>
        ///  Return id of function group in system
        /// </summary>
        public struct FunctionGr
        {
            //ID of Backup function group
            public const string Sybk = "SYBK";

            public const string Syrt = "SYRT";

            //ID of Master function group
            public const string Msae = "MSAE";

            public const string Msal = "MSAL";
            public const string Msap = "MSAP";
            public const string Msar = "MSAR";
            public const string Msca = "MSCA";
            public const string Mscp = "MSCP";
            public const string Mscr = "MSCR";
            public const string Msdd = "MSDD";
            public const string Msde = "MSDE";
            public const string Msfm = "MSFM";
            public const string Mshp = "MSHP";
            public const string Msir = "MSIR";
            public const string Msit = "MSIT";
            public const string Msjf = "MSJF";
            public const string Mslc = "MSLC";
            public const string Msof = "MSOF";
            public const string Msop = "MSOP";
            public const string Mspa = "MSPA";
            public const string Mspo = "MSPO";
            public const string Mspr = "MSPR";
            public const string Mspw = "MSPW";
            public const string Msre = "MSRE";
            public const string Msro = "MSRO";
            public const string Msrs = "MSRS";
            public const string Msrt = "MSRT";
            public const string Msse = "MSSE";
            public const string Mssh = "MSSH";
            public const string Mssi = "MSSI";
            public const string Mssk = "MSSK";
            public const string Msrk = "MSRK";
            public const string Mscs = "MSCS";
            public const string Mssp = "MSSP";
            public const string Mswa = "MSWA";
            public const string Mste = "MSTE";
            public const string Msug = "MSUG";
            public const string Msur = "MSUR";
            public const string Mssj = "MSSJ";
            public const string Msbr = "MSBR";
            public const string Msdo = "MSDO";
            public const string Msle = "MSLE";

            //ID of Human Resource function group
            public const string Hrce = "HRCE";

            public const string Hrem = "HREM";
            public const string Hrct = "HRCT";
            public const string Hrra = "HRRA";
            public const string Hrtm = "HRTM";
            public const string Hrsa = "HRSA";
            public const string Hrda = "HRDA";
            public const string Hris = "HRIS";
            public const string Hrec = "HREC";
            public const string Hrmr = "HRMR";
            public const string Hrot = "HROT";
            public const string Hrlp = "HRLP";
            public const string Hrla = "HRLA";
            public const string Hrli = "HRLI";
            public const string Hrma = "HRMA";
            public const string Hraa = "HRAA";
            public const string Hrwa = "HRWA";

            //ID of Attendance function group
            public const string Atsa = "ATSA";

            public const string Atsd = "ATSD";
            public const string Atsp = "ATSP";
            public const string Atls = "ATLS";
            public const string Atbt = "ATBT";
            public const string Atio = "ATIO";
            public const string Atle = "ATLE";
            public const string Atie = "ATIE";
            public const string Atae = "ATAE";
            public const string Atas = "ATAS";
            public const string Atts = "ATTS";
            public const string Tsp = "TSP";
            public const string Atot = "Atot";

            //ID of Payroll function group
            public const string Prts = "PRTS";

            public const string Prmp = "PRMP";

            //ID of Business function group
            public const string Bstr = "BSTR";

            public const string Bsdo = "BSDO";

            //ID of Report function group
            public const string Rtito = "RTITO";

            public const string Rpio = "RPIO";
            public const string Rptm = "RPTM";
            public const string Rpba = "RPBA";
            public const string Rppr = "RPPR";
            public const string Rpps = "RPPS";
            public const string Rpsa = "RPSA";
            public const string Rpsu = "RPSU";
            public const string RpsuAll = "RPSUAL";

            public const string ProcessType = "ProcessType";

            #region Common

            public const string Reason = "Reason";
            public const string OpenClose_UserState = "OpenClose_UserState";
            public const string ST_StockTransactionMaster = "ST_StockTransactionMaster";

            #endregion Common

            #region Master

            public const string MS_ItemGroups = "MS_ItemGroups";
            public const string MS_Items = "MS_Items";
            public const string MS_WorkRouting = "MS_WorkRouting";
            public const string MS_Items_DisplayName = "MS_Items_DisplayName";
            public const string MS_Suppliers = "MS_Suppliers";
            public const string MS_Vendors = "MS_Vendors";
            public const string MS_Customers = "MS_Customers";
            public const string MS_Companies = "MS_Companies";
            public const string MS_Units = "MS_Units";
            public const string MS_Factories = "MS_Factories";
            public const string MS_Factories_DisplayName = "MS_Factories_DisplayName";
            public const string MS_Departments = "MS_Departments";
            public const string MS_ProductionLines = "MS_ProductionLines";
            public const string MS_Warehouses = "MS_Warehouses";
            public const string MS_InventoryPeriods = "MS_InventoryPeriods";
            public const string MS_Employees = "MS_Employees";
            public const string MS_ErrorReasons = "MS_ErrorReasons";
            public const string MS_UnitConvertion = "MS_UnitConvertion";
            public const string MS_ItemFactory = "MS_ItemFactory";

            //public const string MS_Unit_Search = "MS_Unit_Search";
            public const string MS_Lines = "MS_Lines";

            public const string MS_Currency = "MS_Currency";
            public const string MS_Currency_ExchRate = "MS_Currency_ExchRate";
            public const string MS_DocumentType = "MS_DocumentType";

            public const string SYS_Parameters_SearchForm = "SYS_Parameters_SearchForm";

            public const string MS_CategoryItem = "MS_CategoryItem";
            public const string MS_Exchrates = "MS_Exchrates";

            public const string MS_Capital = "MS_Capital";

            public const string MS_Acquisitionreason = "MS_Acquisitionreason";

            public const string MS_FixedAssetsType = "MS_FixedAssetsType";
            #endregion Master

            #region Price

            public const string PR_PriceUpdating = "PR_PriceUpdating";
            public const string PR_SampleShippingText = "PR_SampleShippingText";
            #endregion Price

            #region Inventory

            public const string ST_StockManagement = "ST_StockManagement";
            public const string ST_MonthlyProcess = "ST_MonthlyProcess";
            public const string ST_MonthlyProcess_PeriodClose = "ST_MonthlyProcess_PeriodClose";

            public const string ST_StockOverview = "ST_StockOverview";
            public const string ST_SH_StockShippingForDelivery = "ST_SH_StockShippingForDelivery";
            public const string ST_SH_StockShippingForDeliveryShipping = "ST_SH_StockShippingForDeliveryShipping";
            public const string ST_SH_StockShippingForDeliverySample = "ST_SH_StockShippingForDeliverySample";
            public const string ST_SH_ReturnDeliveryToSupplier = "ST_SH_ReturnDeliveryToSupplier";
            public const string ST_SH_ScrappingFromNG = "ST_SH_ScrappingFromNG";
            public const string ST_SH_StockShippingAdjustment = "ST_SH_StockShippingAdjustment";
            public const string ST_SA_StockArrivingForPurchaseOrder = "ST_SA_StockArrivingForPurchaseOrder";
            public const string ST_SA_ReturnFromCustomerShipping = "ST_SA_ReturnFromCustomerShipping";
            public const string ST_SA_StockArrivingAdjustment = "ST_SA_StockArrivingAdjustment";
            public const string ST_ST_StockTransferShippingFromStorage = "ST_ST_StockTransferShippingFromStorage";
            public const string ST_ST_StockTransferArrivingInStorage = "ST_ST_StockTransferArrivingInStorage";
            public const string ST_ST_NGRecycleMaterial = "ST_ST_NGRecycleMaterial";
            public const string ST_ST_TransferPosting = "ST_ST_TransferPosting";
            public const string ST_ST_TransferForProductionLine = "ST_ST_TransferForProductionLine";

            public const string ST_PA_PhysicalInventoryAdjustment = "ST_PA_PhysicalAdjustmentInventory_CountSheet";

            public const string IV_PhysicalInventory = "IV_PhysicalInventory";
            public const string IV_PhysicalInventory_CountSheet_AddItem = "IV_PhysicalInventory_CountSheet_AddItem";
            public const string IV_PhysicalInventory_CountSheet = "IV_PhysicalInventory_CountSheet";

            public const string IV_StockModify = "IV_StockModify";
            public const string IV_ReceiptIssueInquiry = "IV_ReceiptIssueInquiry";
            public const string IV_InventoryInquiry = "IV_InventoryInquiry";
            public const string IV_ArrivingIssueInquiry = "IV_ArrivingIssueInquiry";
            public const string IV_OpenClose = "IV_OpenClose";

            //public const string IV_StockReceive_Edit = "IV_StockReceive_Edit";
            public const string s_stocktransactionmaster = "s_stocktransactionmaster";

            public const string t_currentstock = "t_currentstock";
            public const string t_currentwipdetailbyline = "t_currentwipdetailbyline";
            public const string t_stockdetail = "t_stockdetail";
            public const string IV_AdjustmentDateForm = "IV_AdjustmentDateForm";


            #endregion Inventory

            #region  Invoice
            public const string INV_Invoice = "INV_Invoice";
            public const string INV_InvoiceSelect = "INV_InvoiceSelect";
            #endregion

            #region  Invoice

            public const string CO_Cost = "CO_Cost";
            public const string A_Costing_Import = "A_Costing_Import";

            #endregion

            #region Anging

            public const string A_CurrentAging = "A_CurrentAging";
            public const string A_ExportAging = "A_ExportAging";

            #endregion Anging

            #region Schedule

            public const string SD_MonthlyDelivery = "SD_MonthlyDelivery";

            #endregion

            #region System

            public const string SY_Functions = "SY_Functions";
            public const string SY_Users = "SY_Users";
            public const string SY_UserGroups = "SY_UserGroups";
            public const string SY_UserGroupsPermission = "SY_UserGroupsPermission";
            public const string SY_ChangePassword = "SY_ChangePassword";
            public const string SY_SystemJournals = "SY_SystemJournals";
            public const string SY_CompanyInfomation = "SY_CompanyInfomation";
            public const string SY_Parameters = "SY_Parameters";
            public const string SY_Backup = "SY_Backup";
            public const string SY_Restore = "SY_Restore";

            public const string SYS_SystemJournals = "SY_SystemJournals";

            public const string SY_UserState = "SY_UserState";

            public const string SYS_Notifications = "SYS_Notifications";

            #endregion System

            #region Report

            public const string RP_DailyInventory = "RP_DailyInventory";
            public const string RP_MonthlyInventory = "RP_MonthlyInventory";
            public const string RP_WarehouseInventoryMinutes = "RP_WarehouseInventoryMinutes";
            public const string RP_RawMaterialSummary = "RP_RawMaterialSummary";
            public const string RP_MaterialsReportedInMonth = "RP_MaterialsReportedInMonth";
            public const string RP_DestroyedMinutes = "RP_DestroyedMinutes";
            public const string RP_WarehouseVendorInventoryEveryQuarter = "RP_WarehouseVendorInventoryEveryQuarter";
            public const string RP_ArrivingList = "RP_ArrivingList";
            public const string RP_ShippingList = "RP_ShippingList";
            public const string RP_SalesList = "RP_SalesList";
            public const string RP_StockAging = "RP_StockAging";
            public const string RP_NG_NCRate = "RP_NG_NCRate";
            public const string RP_NG_NCRateChart = "RP_NG_NCRateChart";
            public const string RP_NG_NCRateSummary = "RP_NG_NCRateSummary";
            public const string RP_InvTranList = "RP_InvTranList";
            public const string RP_IncludedWarehouseInventoryMinutes = "RP_IncludedWarehouseInventoryMinutes";
            public const string RP_CycleTimeDetail = "RP_CycleTimeDetail";
            public const string RP_SaleForecast = "RP_SaleForecast";

            #endregion Report

            #region Delivery

            public const string DE_MonthlyPlan = "DE_MonthlyPlan";
            public const string DE_DailyDeliverySchedule = "DE_DailyDeliverySchedule";
            public const string DE_DailyPlan = "DE_DailyPlan";
            public const string DE_ResultDaily = "DE_ResultDaily";

            public const string DE_MonthlyPlanInternal = "DE_MonthlyPlanInternal";
            public const string DE_DailyPlanInternal = "DE_DailyPlanInternal";
            public const string DE_ResultDailyInternal = "DE_ResultDailyInternal";

            #endregion Delivery

            #region Custome Select Data

            public const string CurrentPeriod_Document = "CurrentPeriod_Document";

            #endregion Custome Select Data

            #region Print Sheet Reason Form

            public const string SH_DeliveryPrintConfirmForm = "SH_DeliveryPrintConfirmForm";

            #endregion Print Sheet Reason Form

            #region Fixed Assets
            public const string FA_Information = "FA_Information";
            public const string FA_Revaluation = "FA_Revaluation";
            public const string FA_Decrease = "FA_Decrease";
            public const string FA_Increase = "FA_Increase";
            public const string FA_Transfer = "FA_Transfer";
            public const string FA_Abandonment = "FA_Abandonment";
            public const string t_transaction = "t_transaction";
            public const string FA_MonthlyAdjustment = "FA_MonthlyAdjustment";
            public const string FA_DepreciationCalculation = "FA_DepreciationCalculation";
            public const string FA_DepreciationForecast = "FA_DepreciationForecast";

            #endregion


        }

        #endregion FunctionGroup

        #region TimesheetType

        //public struct TimesheetType
        //{
        //    public const string Blank = "0";
        //    public const string Working = "1";
        //    public const string WorkOnADayOff = "4";
        //    public const string Leave = "2";
        //}

        public struct TimesheetType
        {
            public const string Blank = "0";
            public const string Work = "1";
            public const string SubstituteWork = "2"; //w
            public const string CompensatoryWork = "3"; //w
            public const string WorkOnADayOff = "4";
            public const string PaidLeave = "5";
            public const string UnpaidLeave = "6";
            public const string SpecialPaidHoliday = "7";
            public const string CompensatoryLeave = "8"; //l
            public const string SubstituteLeave = "9"; //l
            public const string Others = "10";
            public const string DayOff = "11"; //l
        }

        public struct TimesheetTypeSummary
        {
            public const string Work = "1"; //w
            public const string SubstituteWork = "2"; //w
            public const string CompensatoryWork = "3"; //w
            public const string WorkOnDayOff = "4"; //w
            public const string PaidLeave = "5"; //l
            public const string UnpaidLeave = "6"; //l
            public const string SpecialPaidHoliday = "7"; //l
            public const string CompensatoryLeave = "8"; //l
            public const string SubstituteLeave = "9"; //l
            public const string Others = "10"; //l
            public const string DayOff = "11"; //l
        }

        #endregion TimesheetType

        #region TimesheetType

        public struct AttendanceType
        {
            public const string Work = "1";
            public const string Leave = "2";
            public const string Half = "3";
        }

        #endregion TimesheetType

        #region Leave Category

        public struct LeaveType
        {
            public const string Annual = "1";
            public const string Maternity = "2";
            public const string LeaveWithoutPay = "3";
            public const string BusinessTrip = "4";
            public const string Subsidiary = "5";
            public const string Other = "6";
        }

        public struct LeaveTypeForTimeSheet
        {
            public const string PaidLeave = "5";
            public const string UnpaidLeave = "6";
            public const string SpecialPaidHoliday = "7";
            public const string Compensatoryleave = "8";
            public const string Substituteleave = "9";
            public const string Others = "10";
        }

        public struct LeaveCategory
        {
            public const string Annual = "Leave01";
            public const string Maternity = "Leave10";
            public const string BusinessTrip = "Leave11";
            public const string DayOffWithApplication = "Leave15";
            public const string DayOffWithoutApplication = "Leave16";

            //====Showa====
            public const string Pregnancy = "Leave17";

            //=============
        }

        public struct LeaveProcessing
        {
            public const string Year = "1";
            public const string Month = "2";
        }

        #endregion Leave Category

        public struct DisclipinesAwards
        {
            public const string DisInside = "1";
            public const string DisOutside = "0";
            public const string DisPunishment = "1";
            public const string DisAward = "0";
        }

        #region Status

        public struct Status
        {
            public const string Enable = "1";
            public const string Disable = "0";
        }

        #endregion Status

        /// <summary>
        ///  Code of Weekdays
        /// </summary>
        public struct Weekdays
        {
            public const string Sun = "0";
            public const string Mon = "1";
            public const string Tue = "2";
            public const string Wed = "3";
            public const string Thu = "4";
            public const string Fri = "5";
            public const string Sat = "6";
        }

        /// <summary>
        ///  Master Types (Parent tables)
        /// </summary>
        public enum MasterType
        {
            MsUsers = 0,
            MsUserGroups = 1,
            MsOffices = 2,
            MsDepartments = 3,
            MsSections = 4,
            MsTeams = 5,
            MsRoutes = 6,
            MsOperations = 7,
            MsReasons = 8,
            MsShifts = 9,
            MsRotateShiftSetting = 10,
            MsFingerMachines = 11,
            MsLeaveCategory = 12,
            MsInsRegisterOrgs = 13,
            MsHospitals = 14,
            MsSkills = 15,
            MsCareers = 16,
            MsPositions = 17,
            MsAreas = 18,
            MsProjects = 19,
            MsAllowances = 20,
            MsDeductions = 21,
            MsSalaryPeriods = 22,
            MsRanks = 23,
            MsClasses = 24,
            MsFormOfWages = 25,
            MsDormitories = 26,
            MsLevels = 27,
            MsBusRoute = 28,
        }

        /// <summary>
        ///  Child tables
        /// </summary>
        public enum DeActiveResult
        {
            Fail = -2,
            OK = -1,
            MsGroupsAssign = 0,
            AtTimeSheetHistories = 1,
            MsPermissionsAssign = 2,
            MsDepartments = 3,
            MsFingerMachines = 4,
            HrEmployees = 5,
            HrRotateAssignment = 6,
            HrManagerAssignment = 7,
            MsSections = 8,
            MsTeams = 9,
            MsShifts = 10,
            MsRoutes = 11,
            MsOperations = 12,
            AtLeavePlanning = 13,
            MsRotateScheduledSetting = 14,
            AtWorkingScheduling = 15,
            AtInOut = 16,
            AtAttendanceErrors = 17,
            HrTraining = 18,
            MsPositions = 19,
            MsProjects = 20,
            HrEmpAllowances = 21,
            PrAllowances = 22,
            PrDeductions = 23,
            MsFormOfWages = 24,
            MsBusRoute = 25,
            MsDormitories = 26
        }

        public struct PayrollAssignmentGender
        {
            public const string Male = "1";
            public const string Female = "2";
            public const string All = "3";
        }

        public struct PayrollAssignmentEmployeeType
        {
            public const string Staff = "1";
            public const string Worker = "2";
            public const string All = "3";
        }

        public struct PayrollAssignmentWorkType
        {
            public const string FullTime = "1";
            public const string PartTime = "2";
            public const string Contract = "3";
            public const string All = "4";
        }

        public struct FormOfWagePaidOT
        {
            public const string PaidOt = "1";
            public const string UnPaidOt = "0";
        }

        public struct FormOfWageDetailDayNight
        {
            public const string Day = "0";
            public const string Night = "1";
        }

        public struct FormOfWageDetailDayType
        {
            public const string WorkingDay = "1";
            public const string CompanyHoliday = "2";
            public const string Weekend = "3";
            public const string PublicHoliday = "4";
        }

        public struct FormOfWageType
        {
            public const string Monthly = "1";
            public const string DailyByFixDays = "2";
            public const string DailyBySalaryPeriod = "3";
            public const string HourlybyFixHours = "4";
            public const string HourlyBySalaryPeriod = "5";
            public const string Hourly = "6";
            public const string HourlyByAccumulation = "7";
        }

        public struct IsInsured
        {
            public const string Insured = "1";
            public const string UnInsured = "0";
        }

        public struct PitDeduction
        {
            public const string Deduction = "1";
            public const string NonDeduction = "0";
        }

        public struct UDMember
        {
            public const string JoinUD = "1";
            public const string NotJoinUD = "0";
        }

        public struct Nationality
        {
            public const string Vietnamese = "1";
            public const string Japanese = "2";
            public const string Other = "3";
        }

        public static readonly List<String> DateColumnName = new List<String>()
        {
            "DateofBirth",
            "Date",
            "ToDate",
            "FromDate",
            "IDIssDate",
            "MoveInDate",
            "StartedDate",
            "JoinedDate",
            "StopWrkDate",
            "SIDate",
            "HIRegDate",
            "HIFrmDate",
            "HIToDate",
            "RPPIssDate",
            "RExpDate",
            "RVisaIssDate",
            "RVisaFrmDate",
            "RVisaUntil",
            "QAchieDate",
            "EStartDate",
            "EEndDate",
            "ReBirthDay",
            "ContrDate",
            "ContrStartDate",
            "ContrExpDate",
            "ContrProbStartDate",
            "ContrProbEndDate",
            "AIssuedDate",
            "BDateOfBirth",
            "BIssuedDate",
            "TStartDate",
            "TEndDate",
            "DisDate",
            "MovingDay",
            "StartDate",
            "EndDate",
            "EstDueDate",
            "BirthDate",
            "ChildBenStartDate",
            "ChildBenEndDate",
            "ATDate",
            "ScheduleDate",
            "TimeSheetDate"
          };

        public static readonly List<String> TimeColumnName = new List<String>()
        {
            "TimeIn",
            "TimeOut",
            "BreakTimeFrom1",
            "BreakTimeTo1",
            "BreakTimeFrom2",
            "BreakTimeTo2",
            "BreakTimeFrom3",
            "BreakTimeTo3",
            "TotalWrkHr",
            "ShiftTime",
            "WorkTimeMaxofDay",
            "TimeInRoundOff",
            "TimeOutRoundOff",
            "WorkingTimeRoundOff",
            "DailyOTRoundOff",
            "TSRoundOff",
            "OTRoundOff",
            "EarlyOutHours",
            "LatelyInHours",
            "NightTimeFrom",
            "NightTimeTo",
            "WorkingTime",
            "BreakTime",
            "OTBeforeShift",
            "OTBeforeShiftUpdated",
            "OTAfterShift",
            "OTAfterShiftUpdated",
            "LateNight",
            "LateNightUpdated",
            "OTLateNight",
            "OTLateNightUpdated",
            "OTWeekend",
            "OTWeekendUpdated",
            "LateNightWk",
            "LateNightWkUpdated",
            "OTCompHoliday",
            "OTCompHolidayUpdated",
            "LateNightCompH",
            "LateNightCompHUpdated",
            "OTPublicHoliday",
            "OTPublicHolidayUpdated",
            "LateNightPubH",
            "LateNightPubHUpdated",
            "LatelyIn",
            "LatelyInUpdated",
            "EarlyOut",
            "EarlyOutUpdated",
            "PrivateOut",
            "PrivateOutUpdated",
            "WorkingHours",
            "BeforeOTHours",
            "AfterOTHours",
            "LateNightHours",
            "OTLateNightHours",
            "OTWeekendHours",
            "LateNightWedHours",
            "OTCompHoHours",
            "LateNightCompHoHours",
            "OTPubHoHours",
            "LateNightPubHoHours",
            "LatelyInHours",
            "EarlyOutHours",
            "PrivateOutHours",
            "TotalHours"
        };

        public const int DaysInYear = 365;

        /// <summary>
        ///  Return string have value is empty
        /// </summary>
        public static string StringEmpty = string.Empty;
        public const string CommonAll = "All";
        public const string CommonCode = "Code";
        public const string CommonKey = "Key";
        public const string CommonLangId = "LangId";
        public const string CommonValue = "Value";
        public const string GroupPanelText = "GroupPanelText";

        public const string WordWrapCharacter = "\r\n";

        /// <summary>
        ///  Code of ContrType
        /// </summary>
        public struct ContrType
        {
            public const string ProbationContract = "0";
            public const string LimitedContract = "1";
            public const string SeasonalContract = "2";
            public const string PermanentContract = "3";
        }

        public struct BackupFormat
        {
            public const string MySql = "sql";
            public const string MSSQL = "bak";
            public const string Oracle = "dmp";
        }

        public struct ServicesBatch
        {
            public const string BackupScheduling = "BackupScheduling";
        }

        /// <summary>
        ///  Code of ObjType
        /// </summary>
        public struct ObjType
        {
            public const string Offices = "1";
            public const string Departments = "2";
            public const string Sections = "3";
            public const string Teams = "4";
            public const string Routes = "5";
            public const string Areas = "6";
            public const string Projects = "7";
            public const string Operations = "8";
        }

        public struct ObjTypeAllowance
        {
            public const string Offices = "1";
            public const string Departments = "2";
            public const string Sections = "3";
            public const string Teams = "4";
            public const string Routes = "5";

            //public const string Areas = "6";
            //public const string Projects = "7";
            public const string Operations = "6";

            public const string Levels = "7";
            public const string Positions = "8";
            public const string Shifts = "9";
        }

        public struct AllowanceCodes
        {
            public const string Transportation = "AL001";
            public const string Housing = "AL002";
            public const string PerfectAttendance = "AL003";
            public const string Hardship = "AL004";
            public const string Position = "AL005";

            //public const string Areas = "6";
            //public const string Projects = "7";
            public const string Shifting = "AL006";

            public const string Skill = "AL007";
            public const string Service = "AL009";
        }

        /// <summary>
        ///  Code of PlanningType
        /// </summary>
        public struct PlanningType
        {
            public const string Work = "1";
            public const string Leave = "2";
        }

        /// <summary>
        ///  Code of WorkCategory
        /// </summary>
        public struct WorkCategory
        {
            public const string Normal = "1";
            public const string LatelyIn = "2";
            public const string EarlyOut = "3";
            public const string LatelyInAndEarlyOut = "4";
            public const string OT = "5";
            public const string LateNightOT = "6";
        }

        /// <summary>
        ///  Code of FMType
        /// </summary>
        public struct FMType
        {
            public const string Looker = "1";
            public const string Attandance = "2";
        }

        /// <summary>
        ///  Code of FMType
        /// </summary>
        public enum CalculateTimeSheetBy
        {
            Plan = 1,
            User = 2,
            AutoTS = 3,
            AttendanceSheet = 4,
            AttendanceError = 5,
            ManualTS = 6
        }

        public struct Environment
        {
            /// <summary>
            /// Default environment is english "en-US"
            /// </summary>
            public const string English = "en-US";

            public const string Vietnamese = "en-US";
        }

        public struct ProcessFileType
        {
            public const string UpLoad = "1";
            public const string DownLoad = "2";
        }

        public static int GetProgress(int increaseNumber, int totalNumber, int partOfNumber, int totalParOfNumber)
        {
            //Progress number of begin
            int numOfBegin = (100 * (partOfNumber - 1) / totalParOfNumber);
            return numOfBegin + ((increaseNumber * 100 / totalNumber) / totalParOfNumber);
        }

        public static bool WriteProgress(string logPath, params string[] messages)
        {
            return WriteFile(logPath, false, messages);
        }

        public static bool ReadProgress(string logPath, ref string result)
        {
            bool isOK = false;
            if (File.Exists(logPath))
            {
                using (FileStream fileStream = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    if (fileStream != null)
                    {
                        using (StreamReader tr = new StreamReader(fileStream))
                        {
                            result = tr.ReadToEnd();
                            tr.Close();
                            isOK = true;
                        }
                        fileStream.Close();
                    }
                }
            }
            return isOK;
        }

        public static bool DeleteFile(string logPath)
        {
            try
            {
                if (System.IO.File.Exists(logPath))
                {
                    File.Delete(logPath);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get fullname of path
        /// </summary>
        /// <param name="folderPath">Folder name</param>
        /// <param name="fileName">File name</param>
        /// <param name="extention">Extention of file</param>
        public static string GetFullPath(string folderPath, string fileName, string extention)
        {
            return folderPath + @"\" + fileName + (extention.Split('.').Length > 1 ? extention : "." + extention);
        }

        /// <summary>
        /// Write some message to LogFile
        /// </summary>
        /// <param name="logPath">Path of logfile</param>
        /// <param name="messages">Messages to write log</param>
        public static bool WriteFile(string logPath, params string[] messages)
        {
            return WriteFile(logPath, true, messages);
        }

        public static bool WriteFile(string logPath, bool isAppend, params string[] messages)
        {
            bool isOK = false;
            using (FileStream fileStream = new FileStream(logPath, isAppend ? FileMode.Append : FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                if (fileStream != null)
                {
                    using (StreamWriter sw = new StreamWriter(fileStream))
                    {
                        foreach (string message in messages)
                        {
                            sw.WriteLine(message);
                        }
                        sw.Close();
                        isOK = true;
                    }
                    fileStream.Close();
                }
            }
            return isOK;
        }

        public enum CommandNumber
        {
            WriteTemp = 222,
            Backup = 223,
            Restore = 224,
            TimeSheet = 225,
        }

        public struct ServiceTask
        {
            public const string ImportWorkingScheduling = "ImportWorkingScheduling";
            public const string ImportLeavePlanning = "ImportLeavePlanning";
            public const string ImportBusinessTripPlanning = "ImportBusinessTripPlanning";
            public const string ImportMaternityLeavePlanning = "ImportMaternityLeavePlanning";
            public const string InsertWorkingScheduling = "InsertWorkingScheduling";
            public const string InsertLeavePlanning = "InsertLeavePlanning";
            public const string InsertBusinessTripPlanning = "InsertBusinessTripPlanning";
            public const string InsertMaternityLeavePlanning = "InsertMaternityLeavePlanning";
            public const string DeleteWorkingScheduling = "DeleteWorkingScheduling";
            public const string DeleteLeavePlanning = "DeleteLeavePlanning";
            public const string DeleteBusinessTripPlanning = "DeleteBusinessTripPlanning";
            public const string DeleteMaternityLeavePlanning = "DeleteMaternityLeavePlanning";
            public const string RegenerateTS = "RegenerateTimesheet";
            public const string LockTimeSheetMonthly = "LockTimeSheetMonthly";
            public const string UnLockTimeSheetMonthly = "UnLockTimeSheetMonthly";
            public const string CreateTimeSheetMonthly = "CreateTimeSheetMonthly";
            public const string CloseTimeSheetMonthly = "CloseTimeSheetMonthly";
            public const string OpenTimeSheetMonthly = "OpenTimeSheetMonthly";
            public const string CreatePayroll = "CreatePayroll";
            public const string ClosePayroll = "ClosePayroll";
            public const string OpenPayroll = "OpenPayroll";
            public const string UpdateWorkingScheduling = "UpdateWorkingScheduling";
            public const string UpdateLeavePlanning = "UpdateLeavePlanning";
            public const string UpdateMaternityLeavePlanning = "UpdateMaternityLeavePlanning";
            public const string UpdateBusinessTripPlanning = "UpdateBusinessTripPlanning";
            public const string ProcessAnnualLeave = "ProcessAnnualLeave";

            public const string MonthlyProcessing = "MonthlyProcessing";
        }

        public struct ServiceName
        {
            public const string BackupRestore = "BackupScheduling";
        }

        public enum ProgressBarType
        {
            Marquee,
            Progress,
        }

        public enum ProgressBarUsing
        {
            File,
            Static,
        }

        public struct RecruitDenied
        {
            public const string Yes = "1";
            public const string No = "0";
        }

        public struct PayOT
        {
            public const string Yes = "1";
            public const string No = "0";
        }

        public struct CompareOTPlan
        {
            public const string Yes = "1";
            public const string No = "0";
        }

        public struct UsingSS
        {
            public const string Yes = "1";
            public const string No = "0";
        }

        public struct Maternity
        {
            public const string Yes = "1";
            public const string No = "0";
        }

        public struct LeaveNo
        {
            public const string Zero = "0.0";
            public const string Half = "0.5";
            public const string One = "1.0";
        }

        public struct TransactionSubCode
        {
            public const string SH_StockShippingForDelivery = "11";

            //public const string SH_StockIssueForDelivery_Shipping = "11";
            //public const string SH_StockIssueForDelivery_Sample = "12";
            public const string SH_ReturnDeliveryToSupplier = "12";

            public const string SH_ScrappingFromNG = "13";
            public const string SH_StockShippingAdjustment = "14";

            public const string SA_StockArrivingForPurchaseOrder = "21";
            public const string SA_ReturnFromCustomerShipping = "22";
            public const string SA_StockArrivingAdjustment = "23";

            //public const string ST_StockTransfer_ShippingFromStorage = "31";
            //public const string ST_StockTransfer_ArrivingInStorage = "32";
            public const string ST_StockTransfer_WH2WH = "31";

            public const string ST_TransferForProductionLine = "33";
            public const string ST_TransferPosting = "32";
            public const string ST_NGRecycledMaterials = "34";

            public const string OpenClose = "41";
            public const string ShippingArrivingInquiry = "42";
            public const string InventoryInquiry = "43";
            public const string StockModify = "44";
            //public const string PhysicalInventory = "45";
            public const string PhysicalInventory = "#";

            public const string PriceCheck = "51";
            public const string PriceAdjustment = "52";

            public const string OpenStock = "00000";
            public const string CloseStock = "11111";

            public const string SubTotal = "22222";

        }

        public struct DocumentType
        {
            public const string Shipping = "SH";
            public const string Arriving = "AR";
            public const string Transfer = "TR";
            public const string Adjustment = "AD";
            public const string CountSheet = "CS";
        }

        public struct BalanceFlag
        {
            public const string Plus = "+";
            public const string Minus = "-";
        }

        public struct DeliverySheetPrintFlg
        {
            public const string Unpublished = "1";
            public const string Published = "2";
            public const string RePublished = "3";
        }

        public struct StockType
        {
            public const string RM_ProductStock = "1";
            public const string StockInProcessing = "2";
            public const string StockInMoving = "3";
        }

        public struct InventoryPeriodStatus
        {
            public const string Opening = "1";
            public const string Closing = "2";
            public const string Future = "3";
            public const string Lock = "4";
        }

        public enum MonthlyProcessAction
        {
            None = 0,
            Setting = 1,
            OpenPrevious = 2,
            OpenCurrent = 3,
            OpenNext = 4,
            ClosePrevious = 5,
            CloseCurrent = 6,
            CheckPrevious = 7,
            CheckCurrent = 8,
        }

        public struct DocumentTypeAdjustmentString
        {
            public const string ShippingAdjustment = "Stock Shipping Adjustment";
            public const string ArrivingAdjustment = "Stock Arriving Adjustment";
        }

        public struct ItemType
        {
            public const string RawMaterials = "1";
            public const string Products = "2";
            public const string Goods = "3";
            public const string FreeProduct = "4";
        }

        public struct ProcessType
        {
            public const string RM = "1";
            public const string WIP = "2";
            public const string FG = "3";
        }

        public struct SampleShippingFlg
        {
            public const string Shipping = "1";
            public const string Sample = "2";
        }

        public struct CancelFlag
        {
            public const string OK = "1";
            public const string Cancel = "2";
            public const string Moving = "3";
        }

        #region ItemAdditionalIsActive in table dbo.sy_parameters

        public struct ItemAdditionalIsActive
        {
            public const string Enable = "1";
            public const string Disable = "0";
        }

        #endregion ItemAdditionalIsActive in table dbo.sy_parameters

        #region ItemAdditionalIsRequired in table dbo.sy_parameters

        public struct ItemAdditionalIsRequired
        {
            public const string Enable = "1";
            public const string Disable = "0";
        }

        #endregion ItemAdditionalIsRequired in table dbo.sy_parameters

        #region PhysicalInventoryStatus in table dbo.sys_common

        public struct PhysicalInventoryStatus
        {
            public const string Open = "1";
            public const string InProgress = "2";
            public const string Complete = "3";
            public const string Cancelled = "4";
        }

        #endregion PhysicalInventoryStatus in table dbo.sys_common

        public struct PhysicalInventoryAction
        {
            public const string NoAction = "0";
            public const string TakeSnapShot = "1";
            public const string Save = "2";
            public const string Manual = "3";
            public const string Import = "4";
            public const string Adjust = "5";
            public const string Cancel = "6";
            public const string PrintSheet = "7";
            public const string EditOnGrid = "8";
        }

        public struct OpenCloseReturnCode
        {
            public const int Success = 0;
            public const int ReopenLimit = 20;
            public const int PreviousPeriodNotClosed = 21;
            public const int CurrentPeriodNotEntryPrice = 22;
            public const int NotOpen = 23;
            public const int DataInOutNotFound = 24;
            public const int NotLock = 25;
            public const int NotReopen = 26;
            public const int NotUnLock = 27;
            public const int OK = 28;
            public const int NG = 29;
            public const int NegativeStockExist = 30;
            public const int PendingTransferTransactionExist = 31;
            public const int HavingTransferTransactionNotCompleted = 32;
            public const int TwoPeriodAreOpening = 33;
            public const int NextPeriodNotOpened = 34;

        }

        public struct WarehouseType
        {
            public const string Main = "1";
            public const string Production = "2";
        }

        public enum NotificationType
        {
            None = 0,
            HandleNewestUserLogin = 1,
            ExpitedLabourContract = 2,
        }
        public const string FG = "FG";
        public const string NoProcessingLine = "Non";
        public const string FinishedGoods = "Finished Goods";
        public const string FG_RMCode = "FG";
        //public const string ProductionLineBlank = "ProductionLineBlank";
        public struct ProductionLineBlank
        {
            public const string Code = "NPR";
            public const string Name1 = "Chưa gia công";
            public const string Name2 = "No Processing";
            public const string Name3 = "No Processing";
        }

        public struct ProductionLineFG
        {
            public const string Code = "FG";
            public const string Name1 = "Thành phẩm";
            public const string Name2 = "Finished goods";
            public const string Name3 = "Finished goods";
        }

        public struct LogonState
        {
            public const string Online = "1";
            public const string Offine = "2";
            public const string LostConnection = "3";
            public const string DisByAdmin = "4";
            public const string DisByOpenClose = "5";
        }

        public enum ViewPriceMode
        {
            All = 0,
            Purchase = 1,
            Sales = 2,
            None = 3,
        }

        public struct InvoiceStatus
        {
            public const string New = "1";
            public const string Printed = "2";
            public const string Canceled = "3";
        }

        public const bool CheckSafetyStock = false;

        #region For Boramtek

        public struct ItemCategory
        {
            public const string Aluminum = "al";
            public const string Switch = "sw";
            public const string Key = "key";
            public const string Zn = "zn";
            public const string Key_Al = "key_al";
            public const string Key_Zn = "key_zn";
            public const string SW_Al = "sw_al";
            public const string SW_Zn = "sq_zn";

            //Fix for Boramtek
            public const string AlZn = "alzn";
            public const string KeySW = "keysw";
        }

        #endregion
        #region Language
        //public class LanguageResource
        //{
        //    public static string[] ShippingText1 = { "Hàng bán", "Hàng mẫu" };
        //    public static string[] ShippingText2 = { "Shipping", "Sample" };
        //    public static string[] ShippingText3 = { "Shipping", "Sample" };
        //    public const string ShippingText1VN = "Hàng bán";
        //    public const string ShippingText1EN = "Shipping";
        //    public const string ShippingText2VN = "Hàng mẫu";
        //    public const string ShippingText2EN = "Sample";
        //}
        #endregion

        public struct CommentFlg
        {
            public const string Comment = "1";
            public const string Data = "0";
        }

        public struct BRTFactory
        {
            public const string BR_I = "BR-I";
            public const string BR_II = "BR-II";
            public const string BR_III = "BR-III";
        }

        public struct BRTDailyDeliveryWH
        {
            public const string MT3 = "4303";
        }

        public struct Currency
        {
            public const string VND = "VND";
            public const string USD = "USD";
            public const string JPY = "JPY";
            public const string EUR = "EUR";
        }

        public struct FixedAssetReasonType
        {
            public const string Increase = "1";
            public const string Decrease = "2";
        }

        public struct FixedAssetTransactionType
        {
            public const int Revaluation = 1;
            public const int Adjustment = 2;
            public const int Transfer = 3;
            public const int Abandonment = 4;
            public const int Decrease = 5;
        }

        public struct FixedAssetStatus
        {
            public const string Increase = "1";
            public const string Decrease = "2";
            public const string Abandonment = "3";
        }

        public struct FAClosingStatus
        {
            public const string Open = "1";
            public const string Close = "2";
        }

        public const string CheckString = "x";

        public struct LanguageSkill
        {
            public const string Listening = "1";
            public const string Speaking = "2";
            public const string Reading = "3";
            public const string Writing = "4";
            public const string Translating = "5";
        }

        public struct LanguageTestType
        {
            public const string Reading = "1";
            public const string Writing = "2";
        }

        public static string DisplayFee = "0";

        public struct VocaType
        {
            public const string Alphabet = "1";
            public const string Word = "2";
            public const string Sentence = "3";
        }
    }
}