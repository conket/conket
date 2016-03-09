using System.Security.Cryptography;
using System.Text;

namespace Ivs.Core.Data
{
    public static class UserSession
    {
        #region propeties

        public static bool ConnectedService { get; set; }

        public static string FunctionId
        {
            get;
            set;
        }

        //  public static Ivs.Core.Data.CommonData.Language LangId
        public static string LangId
        {
            get;
            set;
        }

        public static string LangName
        {
            get;
            set;
        }

        public static string UserCode
        {
            get;
            set;
        }

        public static string UserName
        {
            get;
            set;
        }

        public static string Pw
        {
            get;
            set;
        }

        /// <summary>
        /// OperScope property</summary>
        /// <value>
        /// 0: Hr Administration
        /// 1: Office level
        /// 2: Department level
        /// 3: Section level
        /// 4: Team level
        /// 5: Route level
        /// 6: Operation level	</value>
        public static string OperScope
        {
            get;
            set;
        }

        /// <summary>
        /// TsVerifyRole property</summary>
        /// <value>
        /// 0: Shifter Verified
        /// 1: Captain Verified
        /// 2: Admin Verified	</value>
        public static string TsVerifyRole
        {
            get;
            set;
        }

        public static string ComputerName
        {
            get;
            set;
        }

        public static string IPAddress
        {
            get;
            set;
        }

        public static string EmployeeID
        {
            get;
            set;
        }

        public static string EmployeeOffice
        {
            get;
            set;
        }

        public static string EmployeeDepartment
        {
            get;
            set;
        }

        public static string EmployeeSection
        {
            get;
            set;
        }

        public static string EmployeeTeam
        {
            get;
            set;
        }

        public static string EmployeeRoute
        {
            get;
            set;
        }

        public static string EmployeeOparation
        {
            get;
            set;
        }
        public static string IsLogin
        {
            get;
            set;
        }

        public static string UserSkin
        {
            get;
            set;
        }

        #region propeties of Parameter

        public static string Code
        {
            get;
            set;
        }

        public static string SIPercentComp
        {
            get;
            set;
        }

        public static string HIPercentComp
        {
            get;
            set;
        }

        public static string UIPercentComp
        {
            get;
            set;
        }

        public static string UnionDuesComp
        {
            get;
            set;
        }

        public static string UnionDuesAmtComp
        {
            get;
            set;
        }

        public static string UnionDuesAmtMaxComp
        {
            get;
            set;
        }

        public static string SIPercentEmp
        {
            get;
            set;
        }

        public static string HIPercentEmp
        {
            get;
            set;
        }

        public static string UIPercentEmp
        {
            get;
            set;
        }

        public static string UnionDues
        {
            get;
            set;
        }

        public static string UnionDuesAmount
        {
            get;
            set;
        }

        public static string UnionDuesAmtMax
        {
            get;
            set;
        }

        public static string IncTaxable
        {
            get;
            set;
        }

        public static string ITDedForDep
        {
            get;
            set;
        }

        public static string RateOfLateNight
        {
            get;
            set;
        }

        public static string OTRateWorking
        {
            get;
            set;
        }

        public static string OTRateTern
        {
            get;
            set;
        }

        public static string OTRateOffDay
        {
            get;
            set;
        }

        public static string OTRateHoliday
        {
            get;
            set;
        }

        #region--ddthanh--28052013--

        public static string OTLateNightRateWorking
        {
            get;
            set;
        }

        public static string OTLateNightRateTern
        {
            get;
            set;
        }

        public static string OTLateNightRateOffDay
        {
            get;
            set;
        }

        public static string OTLateNightRateHoliday
        {
            get;
            set;
        }

        #endregion propeties of Parameter

        public static string WorkTimeMaxofDay
        {
            get;
            set;
        }

        public static string TimeInRoundOff
        {
            get;
            set;
        }

        public static string TimeInRoundOffMthod
        {
            get;
            set;
        }

        public static string TimeOutRoundOff
        {
            get;
            set;
        }

        public static string TimeOutRoundOffMthod
        {
            get;
            set;
        }

        public static string WorkingTimeRoundOff
        {
            get;
            set;
        }

        public static string WorkingTimeRoundOffMthod
        {
            get;
            set;
        }

        public static string DailyOTRoundOff
        {
            get;
            set;
        }

        public static string DailyOTRoundOffMthod
        {
            get;
            set;
        }

        public static string TSRoundOff
        {
            get;
            set;
        }

        public static string TSRoundOffMthod
        {
            get;
            set;
        }

        public static string OTRoundOff
        {
            get;
            set;
        }

        public static string OTRoundOffMthod
        {
            get;
            set;
        }

        public static string EarlyOutHours
        {
            get;
            set;
        }

        public static string LatelyInHours
        {
            get;
            set;
        }

        public static string EarlyOutTimes
        {
            get;
            set;
        }

        public static string LatelyInTimes
        {
            get;
            set;
        }

        //public static string HrlyRateMethod
        //{
        //    get;
        //    set;
        //}

        public static string DaysForHrlyRate
        {
            get;
            set;
        }

        public static string NightTimeFrom
        {
            get;
            set;
        }

        public static string NightTimeTo
        {
            get;
            set;
        }

        public static string NightTimeAllowance
        {
            get;
            set;
        }

        public static string Industriousness
        {
            get;
            set;
        }

        public static string OTHour
        {
            get;
            set;
        }

        public static string TransferType
        {
            get;
            set;
        }

        public static string Pay
        {
            get;
            set;
        }

        public static string Rate
        {
            get;
            set;
        }

        public static string MthApply
        {
            get;
            set;
        }

        public static string Pay2
        {
            get;
            set;
        }

        public static string Rate2
        {
            get;
            set;
        }

        public static string MthApply2
        {
            get;
            set;
        }

        public static string DaysMax
        {
            get;
            set;
        }

        public static string ValidToMthNextYr
        {
            get;
            set;
        }

        public static string LeaveDaysPerYr
        {
            get;
            set;
        }

        public static string Year1
        {
            get;
            set;
        }

        public static string LeaveDay1
        {
            get;
            set;
        }

        public static string Desc1
        {
            get;
            set;
        }

        public static string Year2
        {
            get;
            set;
        }

        public static string LeaveDay2
        {
            get;
            set;
        }

        public static string Desc2
        {
            get;
            set;
        }

        public static string Year3
        {
            get;
            set;
        }

        public static string LeaveDay3
        {
            get;
            set;
        }

        public static string Desc3
        {
            get;
            set;
        }

        public static string Year4
        {
            get;
            set;
        }

        public static string LeaveDay4
        {
            get;
            set;
        }

        public static string Desc4
        {
            get;
            set;
        }

        public static string Year5
        {
            get;
            set;
        }

        public static string LeaveDay5
        {
            get;
            set;
        }

        public static string Desc5
        {
            get;
            set;
        }

        public static string PercentOfProbation
        {
            get;
            set;
        }

        public static string DaysKeepSystemJournals
        {
            get;
            set;
        }

        public static string TypeApplyForAL
        {
            get;
            set;
        }

        public static string TypeApplyForSeniority
        {
            get;
            set;
        }

        public static string TransportAllowanceWithBus
        {
            get;
            set;
        }

        public static string TransportAllowanceWithoutBus
        {
            get;
            set;
        }

        public static string CompareOTPlan
        {
            get;
            set;
        }

        #endregion propeties

        #endregion

        #region Public Method

        public static bool IsAuthority()
        {
            return true;
        }

        public static string Md5(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        #endregion

        #region Inventory

        public static bool CheckSafetyStock
        {
            get;
            set;
        }

        #endregion

        #region Invoice

        public static bool AllowEditInvQty
        {
            get;
            set;
        }

        #endregion
    }
}