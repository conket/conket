using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using Ivs.Core.Data;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Security.Cryptography;

namespace Ivs.Core.Common
{
    public static class CommonMethod
    {
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

        #region Static Function - LMHieu

        /// <summary>
        /// Parse Chuỗi Cho Dynamic SQL.
        /// ' => ''.
        /// LMHieu.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ParseStrQuery(this object obj)
        {
            return ParseString(obj).Replace("'", "''");
        }

        #endregion Static Function - LMHieu

        public static string ParseNextPeriod(this string obj)
        {
            string value = string.Empty;
            try
            {
                int year = DateTime.Now.Year;
                int month = 1;
                int val = SplitPeriod(obj, ref year, ref month);

                //Format is correct
                if (val == 2)
                {
                    value = new DateTime(year, month, 1).AddMonths(1).ToString(CommonData.DateFormat.yyyyMM);
                }
            }
            catch
            {
                value = string.Empty;
            }
            return value;
        }

        public static string ParsePreviousPeriod(this string obj)
        {
            string value = string.Empty;
            try
            {
                int year = DateTime.Now.Year;
                int month = 1;
                int val = SplitPeriod(obj, ref year, ref month);

                //Format is correct
                if (val == 2)
                {
                    value = new DateTime(year, month, 1).AddMonths(-1).ToString(CommonData.DateFormat.yyyyMM);
                }
            }
            catch
            {
                value = string.Empty;
            }
            return value;
        }

        /// <summary>
        /// Split period -> year & month
        /// </summary>
        /// <param name="period">Period string with format yyyymm or yyyy-mm</param>
        public static int SplitPeriod(string period, ref int year, ref int month)
        {
            int value = 0;
            try
            {
                if (!string.IsNullOrEmpty(period))
                {
                    //yyyymm
                    if (period.Length == 6)
                    {
                        year = int.Parse(period.Substring(0, 4));
                        month = int.Parse(period.Substring(4, 2));
                        value = 2;
                    }
                    //yyyymm
                    else if (period.Length == 7)
                    {
                        year = int.Parse(period.Substring(0, 4));
                        month = int.Parse(period.Substring(5, 2));
                        value = 2;
                    }
                }
            }
            catch
            {
                value = 0;
            }
            return value;
        }

        /// <summary>
        /// Parse period to the first date of period
        /// </summary>
        /// <param name="obj">Period format (yyyymm or yyyy-mm)</param>
        /// <returns></returns>
        public static DateTime ParsePeriodDate(string obj)
        {
            DateTime value = DateTime.MinValue;
            try
            {
                int year = DateTime.Now.Year;
                int month = 1;
                int val = SplitPeriod(obj, ref year, ref month);
                if (val == 2)
                {
                    value = new DateTime(year, month, 1);
                }
            }
            catch
            {
                value = DateTime.MinValue;
            }
            return value;
        }

        public static int ParseInt32(object obj)
        {
            if (IsNullOrEmpty(obj))
            {
                return 0;
            }
            int value;
            try
            {
                value = int.Parse(ParseString(obj));
            }
            catch
            {
                value = 0;
            }
            return value;
        }

        public static decimal ParseDecimal(object varStr)
        {
            if (IsNullOrEmpty(varStr))
            {
                return 0;
            }
            decimal value;
            try
            {
                value = decimal.Parse(ParseString(varStr));
            }
            catch
            {
                value = 0;
            }
            return value;
        }

        /// <summary>
        ///  Change object value to short value
        /// </summary>
        /// <param name="varStr">
        /// String value that needs change to short type
        /// </param>
        /// <returns>
        /// Return short value
        /// </returns>
        public static short ParseShort(object varStr)
        {
            if (IsNullOrEmpty(varStr))
            {
                return 0;
            }
            try
            {
                return short.Parse(ParseString(varStr));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        ///  Change string value to short value
        /// </summary>
        /// <param name="varStr">
        /// String value that needs change to short type
        /// </param>
        /// <returns>
        /// Return short value
        /// </returns>
        public static short ParseShort(string varStr)
        {
            if (string.IsNullOrEmpty(varStr))
            {
                return 0;
            }
            try
            {
                return short.Parse(varStr);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        ///  Change decimal value to short value
        /// </summary>
        /// <param name="number">
        /// Decimal value that needs change to short type
        /// </param>
        /// <returns>
        /// Return short value
        /// </returns>
        public static short ParseShort(decimal number)
        {
            return (short)number;
        }

        /// <summary>
        ///  Change double value to short value
        /// </summary>
        /// <param name="number">
        /// Double value that needs change to short type
        /// </param>
        /// <returns>
        /// Return short value
        /// </returns>
        public static short ParseShort(double number)
        {
            return (short)number;
        }

        /// <summary>
        ///  Change object value to integer value
        /// </summary>
        /// <param name="varStr">
        /// String value that needs change to integer type
        /// </param>
        /// <returns>
        /// Return integer value
        /// </returns>
        public static int ParseInt(object varStr)
        {
            if (IsNullOrEmpty(varStr))
            {
                return 0;
            }
            try
            {
                return int.Parse(ParseString(varStr));
            }
            catch
            {
                return 0;
            }
        }

        public static int? ParseIntAllowNull(Object varObj)
        {
            if (varObj == null || varObj == DBNull.Value || varObj.Equals(""))
                return null;
            try
            {
                return int.Parse(varObj.ToString());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///  Change string value to integer value
        /// </summary>
        /// <param name="varStr">
        /// String value that needs change to integer type
        /// </param>
        /// <returns>
        /// Return integer value
        /// </returns>
        public static int ParseInt(string varStr)
        {
            if (string.IsNullOrEmpty(varStr))
            {
                return 0;
            }
            try
            {
                return int.Parse(varStr);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        ///  Change decimal value to integer value
        /// </summary>
        /// <param name="number">
        /// Decimal value that needs change to integer type
        /// </param>
        /// <returns>
        /// Return integer value
        /// </returns>
        public static int ParseInt(decimal number)
        {
            return (int)number;
        }

        /// <summary>
        ///  Change double value to integer value
        /// </summary>
        /// <param name="number">
        /// Double value that needs change to integer type
        /// </param>
        /// <returns>
        /// Return integer value
        /// </returns>
        public static int ParseInt(double number)
        {
            return (int)number;
        }

        /// <summary>
        ///  Change bool value to string value('0' ->false, '1'->true)
        /// </summary>
        /// <param name="varStr">
        /// String value that needs change to integer type
        /// </param>
        /// <returns>
        /// Return bool value
        /// </returns>
        public static string ParseString(bool varBool)
        {
            return varBool ? "1" : "0";
        }

        public static string ParseString(string varStr)
        {
            if (varStr == "True")
            {
                return "1";
            }
            else
            {
                if (varStr == "False")
                {
                    return "0";
                }
            }
            if (string.IsNullOrEmpty(varStr))
            {
                return string.Empty;
            }
            return varStr;
        }

        /// <summary>
        ///  Change object to string value(null -->"", not null object.tostring)
        /// </summary>
        /// <param name="varObj">
        /// String value that needs change to integer type
        /// </param>
        /// <returns>
        /// Return bool value
        /// </returns>
        public static string ParseString(Object varObj)
        {
            return varObj == null ? string.Empty : varObj.ToString().Trim();
        }


        /// <summary>
        /// Convert object to String with custom format
        /// </summary>
        /// <param name="varString"></param>
        /// object that need to convert
        /// <param name="format">
        /// Ex: ###,###,###,##0.##, dd, MM
        /// </param>
        public static string ParseString(object obj, string format)
        {
            if (obj != null)
            {
                return String.Format("{0:" + format + "}", obj);
            }
            return string.Empty;
        }

        /// <summary>
        /// Convert object to String with custom format
        /// </summary>
        /// <param name="varString"></param>
        /// object that need to convert
        /// <param name="format">
        /// Ex: ###,###,###,##0.##, dd, MM
        /// </param>
        public static string ParseString(string language, object obj, string format)
        {
            if (obj != null)
            {
                CultureInfo ivsCultureInfo = new CultureInfo(language);
                return String.Format(System.Globalization.CultureInfo.CreateSpecificCulture(ivsCultureInfo.NameCultureInfo), "{0:" + format + "}", obj);
            }
            return string.Empty;
        }

        /// <summary>
        ///  Convert DateString to Internal (for DB)Date Type (common is YYYY-MM-DD)
        /// </summary>
        /// <param name="varStr">
        /// ex: In = "12/11/2012" -->"2012-12-11"
        /// </param>
        /// <returns>
        /// Return string value
        /// </returns>
        public static string ParseInternalDateFormat(DateTime varDt)
        {
            if (varDt == DateTime.MinValue)
            {
                return null;
            }
            else
            {
                return varDt.ToString(CommonData.DateFormat.InternalDateFormat);
            }
        }

        /// <summary>
        ///  Convert DateString to Custom Date Type
        /// </summary>
        /// <param name="varStr">
        /// </param>
        /// <param name="languageCode">
        /// English: En - VietNam: Vn - Japanese: Jp
        /// </param>
        /// <returns>
        /// Return string value
        /// </returns>
        public static string ParseCustomDateFormat(DateTime varDt, string languageCode)
        {
            if (varDt == DateTime.MinValue)
            {
                return null;
            }
            else
            {
                string date = CommonData.StringEmpty;
                if (languageCode.Equals(CommonData.Language.English))
                {
                    date = varDt.ToString(CommonData.DateFormat.MMddyyyy);
                }
                else if (languageCode.Equals(CommonData.Language.VietNamese))
                {
                    date = varDt.ToString(CommonData.DateFormat.DdMMyyyy);
                }
                else if (languageCode.Equals(CommonData.Language.Japanese))
                {
                    date = varDt.ToString(CommonData.DateFormat.Yyyy_MM_dd);
                }
                return date;
            }
        }

        /// <summary>
        ///  Convert DateString to Custom DateTime Type using Second
        /// </summary>
        /// <param name="varStr">
        /// </param>
        /// <param name="languageCode">
        /// English: En - VietNam: Vn - Japanese: Jp
        /// </param>
        /// <returns>
        /// Return string value
        /// </returns>
        public static string ParseCustomDateTimeFormat(DateTime varDt, string languageCode)
        {
            if (varDt == DateTime.MinValue)
            {
                return null;
            }
            else
            {
                string date = CommonData.StringEmpty;
                if (languageCode.Equals(CommonData.Language.English))
                {
                    date = varDt.ToString(CommonData.DateFormat.MMddyyyyHHmmss);
                }
                else if (languageCode.Equals(CommonData.Language.VietNamese))
                {
                    date = varDt.ToString(CommonData.DateFormat.DdMMyyyyHHmmss);
                }
                else if (languageCode.Equals(CommonData.Language.Japanese))
                {
                    date = varDt.ToString(CommonData.DateFormat.Yyyy_MM_ddHHmmss);
                }
                return date;
            }
        }

        /// <summary>
        ///  Convert DateString to Custom DateTime Type No using Second
        /// </summary>
        /// <param name="varStr">
        /// </param>
        /// <param name="languageCode">
        /// English: En - VietNam: Vn - Japanese: Jp
        /// </param>
        /// <returns>
        /// Return string value
        /// </returns>
        public static string ParseCustomDateTimeNoSecondFormat(DateTime varDt, string languageCode)
        {
            if (varDt == DateTime.MinValue)
            {
                return null;
            }
            else
            {
                string date = CommonData.StringEmpty;
                if (languageCode.Equals(CommonData.Language.English))
                {
                    date = varDt.ToString(CommonData.DateFormat.MMddyyyyHHmm);
                }
                else if (languageCode.Equals(CommonData.Language.VietNamese))
                {
                    date = varDt.ToString(CommonData.DateFormat.DdMMyyyyHHmm);
                }
                else if (languageCode.Equals(CommonData.Language.Japanese))
                {
                    date = varDt.ToString(CommonData.DateFormat.Yyyy_MM_ddHHmm);
                }
                return date;
            }
        }

        /// <summary>
        ///  Convert DateString to Internal (for DB)Date Type (common is YYYY-MM-DD)
        /// </summary>
        /// <param name="varStr">
        /// ex: In = "12/11/2012" -->"2012-12-11"
        /// </param>
        /// <returns>
        /// Return string value
        /// </returns>
        public static string ParseInternalDateFormat(string varStr)
        {
            return ParseInternalDateFormat(ParseDate(varStr));
        }

        /// <summary>
        ///  Convert DateTime to Internal (for DB)DateTime Type (common is YYYY-MM-DD HH:mm:ss)
        /// </summary>
        /// <param name="varStr">
        /// ex: In = "12/11/2012 08:00:00" -->"2012-12-11 08:00:00"
        /// </param>
        /// <returns>
        /// Return string value
        /// </returns>
        public static string ParseInternalDateTimeFormat(string date, string time)
        {
            return ParseInternalDateFormat(date) + " " + time;
        }

        /// <summary>
        ///  Convert DateTime to Internal (for DB)DateTime Type (common is YYYY-MM-DD HH:mm:ss)
        /// </summary>
        /// <param name="varStr">
        /// ex: In = "12/11/2012 08:00:00" -->"2012-12-11 08:00:00"
        /// </param>
        /// <returns>
        /// Return string value
        /// </returns>
        public static string ParseInternalDateTimeFormat(DateTime date, string time)
        {
            return ParseInternalDateFormat(date) + " " + time;
        }

        //public static string ParseInternalDateFormat(DateTime varDt)
        //{
        //    if (varDt == DateTime.MinValue)
        //    {
        //        return "";
        //    }
        //    else
        //    {
        //        return varDt.ToString(DateFormat.Yyyy_MM_dd);
        //    }
        //}

        /// <summary>
        ///  Convert DateTime to Internal (for DB)Time Type (common is HH:mm:ss)
        /// </summary>
        /// <param name="varDt">
        /// ex: In = "12/11/2012 08:00:00" -->"08:00:00"
        /// </param>
        /// <returns>
        /// Return string value
        /// </returns>
        public static string ParseInternalTimeFormat(DateTime varDt)
        {
            if (varDt == DateTime.MinValue)
            {
                return null;
            }
            else
            {
                return new TimeSpan(varDt.Hour, varDt.Minute, varDt.Second).ToString();
            }
        }

        /// <summary>
        ///  Convert DateTime string to Internal (for DB)Time Type (common is HH:mm:ss)
        /// </summary>
        /// <param name="varStr">
        /// ex: In = "12/11/2012 08:00:00" -->"08:00:00"
        /// </param>
        /// <returns>
        /// Return string value
        /// </returns>
        public static string ParseInternalTimeFormat(string varStr)
        {
            return ParseInternalTimeFormat(ParseDateTime(varStr));
        }

        public static string ParseInternalDateTimeFormat(DateTime varStr)
        {
            return varStr.ToString(CommonData.DateFormat.Yyyy_MM_ddHHmmss);
        }

        /// <summary>
        ///  Convert DateString to Internal (for DB)Date Type (common is YYYY-MM-DD)
        /// </summary>
        /// <param name="varStr">
        /// 122012 ---> 2012-12
        /// 12/2012---> 2012-12
        /// 12-2012---> 2012-12
        /// 2012/12---> 2012-12
        /// 2012-12---> 2012-12
        /// 12012  ---> 2012-01
        /// 1/2012 ---> 2012-01
        /// 1-2012 ---> 2012-01
        /// 2012/1 ---> 2012-01
        /// 2012-1 ---> 2012-01
        /// </param>
        /// <returns>
        /// Return bool value
        /// </returns>
        public static string ParseInternalPeriodFormat(string varStr)
        {
            string result = CommonData.StringEmpty;
            try
            {
                if (varStr.Length == 5)
                {
                    result = varStr.Substring(1, 4) + "-0" + varStr.Substring(0, 1);
                }
                else
                {
                    result = varStr.Replace('/', '-');

                    if (result.Length == 6)
                    {
                        if (result.Contains("-"))
                        {
                            if (result[1].Equals('-'))
                            {
                                result = result.Substring(2, 4) + "-0" + varStr.Substring(0, 1);
                            }
                            else
                            {
                                result = result.Insert(5, "0");
                            }
                        }
                        else
                        {
                            result = varStr.Substring(2, 4) + "-" + varStr.Substring(0, 2);
                        }
                    }
                    else
                    {
                        if (result[2].Equals('-'))
                        {
                            result = result.Substring(3, 4) + "-" + varStr.Substring(0, 2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        ///  Change object to DateTime value(null -->DateTime.MinValue, not null object.tostring)
        /// </summary>
        /// <param name="varObj">
        /// DateTime value that needs change to integer type
        /// </param>
        /// <returns>
        /// Return DateTime value
        /// </returns>
        public static int ParseDayOfWeek(string varStr)
        {
            int year = ParseInt(varStr.Substring(0, 4));
            int month = ParseInt(varStr.Substring(4, 2));
            int day = ParseInt(varStr.Substring(6, 2));
            DateTime varDate = new DateTime(year, month, day);
            return (int)varDate.DayOfWeek;
        }

        public static int ParseDayOfWeek(DateTime varDate)
        {
            //return varDate == null ? DateTime.MinValue : ((int)varDate.DayOfWeek);
            return ((int)varDate.DayOfWeek);
        }

        /// <summary>
        ///  Change object to DateTime value(null -->DateTime.MinValue, not null object.tostring)
        /// </summary>
        /// <param name="varObj">
        /// DateTime value that needs change to integer type
        /// </param>
        /// <returns>
        /// Return DateTime value
        /// </returns>
        public static DateTime ParseDateTime(Object varObj)
        {
            return varObj == null || varObj == DBNull.Value || varObj.Equals("") ? DateTime.MinValue : DateTime.Parse(varObj.ToString());
        }

        /// <summary>
        ///  Change object to DateTime value(null -->DateTime.MinValue, not null object.tostring)
        ///  Has null
        /// </summary>
        /// <param name="varObj">
        /// DateTime value that needs change to integer type
        /// </param>
        /// <returns>
        /// Return DateTime value
        /// </returns>
        public static DateTime? ParseDateTimeAllowNull(Object varObj)
        {
            if (varObj == null || varObj == DBNull.Value || varObj.Equals(""))
                return null;
            try
            {
                return DateTime.Parse(varObj.ToString());
            }
            catch
            {
                return null;
            }
        }

        public static DateTime ParseDateTime(Object date, string time)
        {
            return ParseDateTime(ParseInternalDateTimeFormat(ParseDateTime(date), time));
        }

        /// <summary>
        ///  Change object to DateTime value(null -->DateTime.MinValue, not null object.tostring)
        /// </summary>
        /// <param name="varObj">
        /// DateTime value that needs change to integer type
        /// </param>
        /// <returns>
        /// Return DateTime value
        /// </returns>
        public static string ParseTimeMinutes(Object varObj)
        {
            return varObj == null || varObj == DBNull.Value || varObj.Equals("") ? "00:00:00" : varObj.ToString();
        }

        public static string ParseTimeMinutes(string varStr)
        {
            return varStr == null || varStr.Equals("") ? "00:00:00" : varStr;
        }

        /// <summary>
        ///  Change object to DateTime value(null -->DateTime.MinValue, not null object.tostring)
        /// </summary>
        /// <param name="varObj">
        /// DateTime value that needs change to integer type
        /// </param>
        /// <returns>
        /// Return DateTime value
        /// </returns>
        public static DateTime ParseDate(Object varObj)
        {
            DateTime dt = varObj == null || varObj == DBNull.Value || varObj.Equals("") ? DateTime.MinValue : DateTime.Parse(varObj.ToString());

            return dt.Date;
        }

        /// <summary>
        ///  Change object to DateTime value(null -->DateTime.MinValue, not null object.tostring)
        ///  Has null
        /// </summary>
        /// <param name="varObj">
        /// DateTime value that needs change to integer type
        /// </param>
        /// <returns>
        /// Return DateTime value
        /// </returns>
        public static DateTime? ParseDateAllowNull(Object varObj)
        {
            if (varObj == null || varObj == DBNull.Value || varObj.Equals(""))
                return null;
            try
            {
                return DateTime.Parse(varObj.ToString()).Date;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///  Change object to Time value(null -->TimeSpan.MinValue, not null object.tostring)
        /// </summary>
        /// <param name="varObj">
        /// String value with format HH:mm:ss
        /// </param>
        /// <returns>
        /// Return Time value
        /// </returns>
        public static TimeSpan ParseTime(object varObj)
        {
            TimeSpan dt = new TimeSpan();
            if (varObj == null || varObj.Equals(""))
            {
                dt = TimeSpan.Zero;
            }
            else if (varObj.ToString().Split(':').Length != 3)
            {
                dt = TimeSpan.Zero;
            }
            else
            {
                dt = TimeSpan.Parse(varObj.ToString());
            }
            return dt;
        }

        public static TimeSpan ParseTime(DateTime varObj)
        {
            return varObj.TimeOfDay;
        }

        public static TimeSpan ParseTimeSpan(object ts)
        {
            TimeSpan tsValue = TimeSpan.Parse("00:00:00");
            try
            {
                tsValue = TimeSpan.Parse(ParseString(ts));
            }
            catch (Exception ex)
            {
                tsValue = TimeSpan.Parse("00:00:00");
            }
            return tsValue;
        }

        /// <summary>
        ///  Change object to DateTime value(null -->DateTime.MinValue, not null object.tostring)
        /// </summary>
        /// <param name="varObj">
        /// DateTime value that needs change to integer type
        /// </param>
        /// <returns>
        /// Return DateTime value
        /// </returns>
        public static DateTime ParseDateTime(string dateTime, string format)
        {
            System.Globalization.CultureInfo provider = new System.Globalization.CultureInfo("en-US");
            return DateTime.ParseExact(dateTime, format, provider);
        }

        /// <summary>
        ///  Change object to DateTime value(null -->DateTime.MinValue, not null object.tostring)
        /// </summary>
        /// <param name="varObj">
        /// DateTime value that needs change to integer type
        /// </param>
        /// <returns>
        /// HH:MM:SS
        /// </returns>
        public static string SubtractDateTime(DateTime dateTime1, DateTime dateTime2)
        {
            string result = CommonData.StringEmpty;
            if (dateTime1.CompareTo(dateTime2) < 0)
            {
                result = "00:00:00";
            }
            else
            {
                TimeSpan resultTimeSpan = dateTime1.Subtract(dateTime2);
                result = resultTimeSpan.Days * 24 + resultTimeSpan.Hours + ":" + resultTimeSpan.Minutes + ":" + resultTimeSpan.Seconds;
            }
            return result;
        }

        /// <summary>
        ///  Change object to DateTime value(null -->DateTime.MinValue, not null object.tostring)
        /// </summary>
        /// <param name="varObj">
        /// DateTime value that needs change to integer type
        /// </param>
        /// <returns>
        /// HH:MM:SS
        /// </returns>
        public static string SubtractTimeSpan(TimeSpan timeSpan1, TimeSpan timeSpan2)
        {
            string result = CommonData.StringEmpty;
            if (timeSpan1.CompareTo(timeSpan2) < 0)
            {
                result = "00:00:00";
            }
            else
            {
                TimeSpan resultTimeSpan = timeSpan1.Subtract(timeSpan2);
                result = resultTimeSpan.Days * 24 + resultTimeSpan.Hours + ":" + resultTimeSpan.Minutes + ":" + resultTimeSpan.Seconds;
            }
            return result;
        }

        /// <summary>
        ///  Change string value to bool value('0' ->false, '1'->true)
        /// </summary>
        /// <param name="varObj">
        /// String value that needs change to integer type
        /// </param>
        /// <returns>
        /// Return string value
        /// </returns>
        public static bool ParseBool(object varObj)
        {
            try
            {
                if (string.IsNullOrEmpty(varObj.ToString()))
                {
                    return false;
                }
                else
                {
                    string str = varObj.ToString();
                    if (str.Equals("1"))
                    {
                        return true;
                    }
                    else if (str.Equals("0"))
                    {
                        return false;
                    }
                    return bool.Parse(varObj.ToString());
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///  Change string value to long value
        /// </summary>
        /// <param name="varStr">
        /// String value that needs change to long type
        /// </param>
        /// <returns>
        /// Return long value
        /// </returns>
        public static long ParseLong(string varStr)
        {
            if (string.IsNullOrEmpty(varStr))
            {
                return 0;
            }
            try
            {
                return long.Parse(varStr);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        ///  Change string value to float value
        /// </summary>
        /// <param name="varStr">
        /// String value that needs change to float type
        /// </param>
        /// <returns>
        /// Return long value
        /// </returns>
        public static float ParseFloat(string varStr)
        {
            if (string.IsNullOrEmpty(varStr))
            {
                return 0;
            }
            try
            {
                return float.Parse(varStr);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        ///  Change string value to double value
        /// </summary>
        /// <param name="varStr">
        /// String value that needs change to double type
        /// </param>
        /// <returns>
        /// Return double value
        /// </returns>
        public static double ParseDouble(string varStr)
        {
            if (string.IsNullOrEmpty(varStr))
            {
                return 0;
            }
            try
            {
                return double.Parse(varStr);
            }
            catch
            {
                return 0;
            }
        }

        public static double ParseDouble(object varStr)
        {
            double value;
            try
            {
                value = double.Parse(ParseString(varStr));
            }
            catch
            {
                value = 0;
            }
            return value;
        }

        /// <summary>
        ///  Change string value to decimal value
        /// </summary>
        /// <param name="varStr">
        /// String value that needs change to decimal type
        /// </param>
        /// <returns>
        /// Return decimal value
        /// </returns>
        public static decimal ParseDecimal(string varStr)
        {
            if (string.IsNullOrEmpty(varStr))
            {
                return 0;
            }
            try
            {
                return decimal.Parse(varStr);
            }
            catch
            {
                return 0;
            }
        }

        public static decimal? ParseDecimalAllowNull(Object varObj)
        {
            if (varObj == null || varObj == DBNull.Value || varObj.Equals(""))
                return null;
            try
            {
                return decimal.Parse(varObj.ToString());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert an Image to Byte Array
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public static byte[] ConvertImageToByteArray(System.Drawing.Image imageIn)
        {
            try
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
            catch
            {
                return null;
            }
        }

        public static byte[] ConvertImageToByteArray(System.Drawing.Bitmap imageIn)
        {
            try
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert a Byte Array to Image
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public static System.Drawing.Image ConvertAByteArrayToImage(byte[] byteArrayIn)
        {
            try
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArrayIn);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                return returnImage;
            }
            catch
            {
                return null;
            }
        }

        public static System.Drawing.Bitmap ConvertAByteArrayToBitmap(byte[] byteArrayIn)
        {
            try
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArrayIn);
                System.Drawing.Bitmap returnImage = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromStream(ms);
                return returnImage;
            }
            catch
            {
                return null;
            }
        }

        public static DateTime Convert2Date(string sdate, string langId)
        {
            DateTime _date = new DateTime();
            switch (langId)
            {
                case CommonData.Language.English:
                    _date = DateTime.Parse(sdate, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                    break;

                case CommonData.Language.Japanese:
                    _date = DateTime.Parse(sdate, System.Globalization.CultureInfo.CreateSpecificCulture("ja-JP"));
                    break;

                case CommonData.Language.VietNamese:
                    _date = DateTime.Parse(sdate, System.Globalization.CultureInfo.CreateSpecificCulture("vi-VN"));
                    break;

                default:
                    _date = DateTime.Parse(sdate, System.Globalization.CultureInfo.CreateSpecificCulture("ja-JP"));
                    break;
            }
            return _date;
        }

        public static DateTime StringToDate(string sdate)
        {
            DateTime date = new DateTime();
            try
            {
                if (CommonData.Language.Japanese == UserSession.LangId)
                {
                    date = DateTime.Parse(sdate, System.Globalization.CultureInfo.CreateSpecificCulture("ja-JP"));
                }
                else if (UserSession.LangId == CommonData.Language.VietNamese)
                {
                    date = DateTime.Parse(sdate, System.Globalization.CultureInfo.CreateSpecificCulture("vi-VN"));
                }
                else
                    date = DateTime.Parse(sdate, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            }
            catch
            {
            }
            return date;
        }

        /// <summary>
        /// Convert a float time (DD.HH:MI:SE) to total time (TotalHH:MI:SE) format
        /// </summary>
        /// <param name="dateTime">
        /// Float DateTime string
        /// </param>
        /// <returns>
        /// string value with time format
        /// </returns>
        public static string ConvertFloatTimeToTotalTime(string dateTime)
        {
            #region Init and check data

            if (string.IsNullOrEmpty(dateTime))
            {
                return string.Empty;
            }
            string[] stringCount = dateTime.Split(':');
            //not format time
            if (stringCount.Length <= 1)
            {
                return dateTime;
            }

            #endregion Init and check data

            #region Find format DD.HH:MI:SE and convert

            string[] stringHours = stringCount[0].Split('.');
            if (stringHours.Length > 1)
            {
                return ((ParseInt((stringHours[0])) * 24) + ParseInt(stringHours[1])) + ":" + stringCount[1];
            }
            return stringCount[0] + ":" + stringCount[1];

            #endregion Find format DD.HH:MI:SE and convert
        }

        /// <summary>
        /// Convert a number to VND currency word
        /// </summary>
        /// <param name="number">
        /// Number that need to convert
        /// </param>
        /// <returns>
        /// string value is converted
        /// </returns>
        public static string ConvertToVNDWord(decimal number)
        {
            string s = number.ToString("#");
            string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
            int i, j, donvi, chuc, tram;
            string str = " ";
            bool booAm = false;
            decimal decS = 0;
            //Tung addnew
            try
            {
                decS = ParseDecimal(s.ToString());
            }
            catch
            {
            }
            if (decS < 0)
            {
                decS = -decS;
                s = decS.ToString();
                booAm = true;
            }
            i = s.Length;
            if (i == 0)
                str = so[0] + str;
            else
            {
                j = 0;
                while (i > 0)
                {
                    donvi = ParseInt(s.Substring(i - 1, 1));
                    i--;
                    if (i > 0)
                        chuc = ParseInt(s.Substring(i - 1, 1));
                    else
                        chuc = -1;
                    i--;
                    if (i > 0)
                        tram = ParseInt(s.Substring(i - 1, 1));
                    else
                        tram = -1;
                    i--;
                    if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                        str = hang[j] + str;
                    j++;
                    if (j > 3) j = 1;
                    if ((donvi == 1) && (chuc > 1))
                        str = "một " + str;
                    else
                    {
                        if ((donvi == 5) && (chuc > 0))
                            str = "lăm " + str;
                        else if (donvi > 0)
                            str = so[donvi] + " " + str;
                    }
                    if (chuc < 0)
                        break;
                    else
                    {
                        if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                        if (chuc == 1) str = "mười " + str;
                        if (chuc > 1) str = so[chuc] + " mươi " + str;
                    }
                    if (tram < 0) break;
                    else
                    {
                        if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                    }
                    str = " " + str;
                }
            }
            if (booAm)
            {
                str = "Âm " + str;
            }
            else
            {
                str = str.Trim();
                str = str[0].ToString().ToUpper() + str.Substring(1);
            }

            return str + " đồng";
        }

        public static void Clone(object origin, out object toObj)
        {
            System.Reflection.PropertyInfo i = null;
            Type type = origin.GetType();
            System.Reflection.PropertyInfo[] properties = type.GetProperties();
            toObj = Activator.CreateInstance(type);
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                // PLCo
                // 2013-11-17 start
                // Is it a nullable type? Get the underlying type
                Type propType = property.PropertyType;
                if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    propType = new NullableConverter(propType).UnderlyingType;
                    object safeValue = (property.GetValue(origin, null) == null) ? null
                                                      : Convert.ChangeType(property.GetValue(origin, null), propType);
                    property.SetValue(toObj, safeValue, null);
                    continue;
                }
                //2013-11-17 end
                property.SetValue(toObj, Convert.ChangeType(property.GetValue(origin, null), property.PropertyType), null);
            }
        }

        /// <summary>
        /// Convert a double number to time format
        /// </summary>
        /// <param name="number">
        /// double number
        /// </param>
        /// <returns>
        /// string value with time format
        /// </returns>
        public static string ConvertHoursToTime(string number)
        {
            string time = "00:00:00";
            //Number is not null and Number's format is xx:xx:xx
            if (!string.IsNullOrEmpty(number) && number.Split(':').Length == 3)
            {
                time = ConvertHoursToTime(ParseTime(number));
            }
            return time;
        }

        /// <summary>
        /// Convert a double number to time format
        /// </summary>
        /// <param name="number">
        /// double number
        /// </param>
        /// <returns>
        /// string value with time format
        /// </returns>
        public static string ConvertHoursToTime(TimeSpan number)
        {
            string time = "00:00:00";
            string hours = (Math.Floor(number.TotalHours)).ToString();
            string minute = number.Minutes.ToString();
            string second = number.Seconds.ToString();
            if (hours.Length == 1)
            {
                hours = "0" + hours;
            }
            if (minute.Length == 1)
            {
                minute = "0" + minute;
            }
            if (second.Length == 1)
            {
                second = "0" + second;
            }

            time = hours + ":" + minute + ":" + second;
            return time;
        }

        /// <summary>
        /// Compare 2 datetime value with string format
        /// </summary>
        /// <param name="value1">DateTime string with format yyyy-mm-dd HH:mm:ss</param>
        /// <param name="value2">DateTime string with format yyyy-mm-dd HH:mm:ss</param>
        /// <returns>
        /// </returns>
        public static int CompareDateTime(string value1, string value2)
        {
            return DateTime.Compare(ParseDateTime(value1)
                                    , ParseDateTime(value2));
        }

        /// <summary>
        /// Compare 2 datetime value
        /// </summary>
        /// <param name="value1">DateTime with format yyyy-mm-dd HH:mm:ss</param>
        /// <param name="value2">DateTime with format yyyy-mm-dd HH:mm:ss</param>
        /// <returns></returns>
        public static int CompareDateTime(DateTime value1, DateTime value2)
        {
            return DateTime.Compare(value1, value2);
        }

        /// <summary>
        /// Compare 2 time value
        /// </summary>
        /// <param name="value1">Time string with format HH:mm:ss</param>
        /// <param name="value2">Time string with format HH:mm:ss</param>
        /// <returns></returns>
        public static int CompareTime(string value1, string value2)
        {
            return (TimeSpan.Parse(value1).CompareTo(TimeSpan.Parse(value2)));
        }

        /// <summary>
        /// Compare 2 time value
        /// </summary>
        /// <param name="value1">TimeSpan with format HH:mm:ss</param>
        /// <param name="value2">TimeSpan with format HH:mm:ss</param>
        /// <returns></returns>
        public static int CompareTime(TimeSpan value1, TimeSpan value2)
        {
            return value1.CompareTo(value2);
        }

        /// <summary>
        /// Check the object is null or empty.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(object obj)
        {
            try
            {
                if (obj == null)
                    return true;
                if (obj.ToString().Trim().Equals(""))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        #region Convert

        public static DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            // Create the result table, and gather all properties of a T
            DataTable table = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Add the properties as columns to the datatable
            foreach (var prop in props)
            {
                Type propType = prop.PropertyType;

                // Is it a nullable type? Get the underlying type
                if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    propType = new NullableConverter(propType).UnderlyingType;

                table.Columns.Add(prop.Name, propType);
            }

            // Add the property values per T as rows to the datatable
            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                    values[i] = props[i].GetValue(item, null);

                table.Rows.Add(values);
            }

            return table;
        }

        #endregion Convert


        #region Stock

        //public static string GetTransactionSubCodeNamePlusTransferPosting(string code, string qualityStatusFrom, string qualityStatusTo)
        //{
        //    string subCode = GetTransactionSubCodeName(code);
        //    if (code.Equals(CommonData.TransactionSubCode.ST_TransferPosting))
        //    {
        //        subCode += "(" + qualityStatusFrom + " -> " + qualityStatusTo + ")";   
        //    }

        //    return subCode;
        //}

        public static string GetTransactionSubCodeName(string code, string sampleShippingFlg = "")
        {
            string transactionSubCode = CommonData.StringEmpty;
            switch (UserSession.LangId)
            {
                case CommonData.Language.English:
                    switch (code)
                    {
                        case CommonData.TransactionSubCode.SH_StockShippingForDelivery:
                            if (CommonMethod.IsNullOrEmpty(sampleShippingFlg))
                            {
                                transactionSubCode = "Stock shipping for delivery";
                            }
                            else if (sampleShippingFlg == CommonData.SampleShippingFlg.Sample)
                            {
                                transactionSubCode = "Stock shipping for delivery (Sample)";
                            }
                            else if (sampleShippingFlg == CommonData.SampleShippingFlg.Shipping)
                            {
                                transactionSubCode = "Stock shipping for delivery (Shipping)";
                            }
                            break;
                        case CommonData.TransactionSubCode.SH_ReturnDeliveryToSupplier:
                            transactionSubCode = "Return delivery to supplier";
                            break;
                        case CommonData.TransactionSubCode.SH_ScrappingFromNG:
                            transactionSubCode = "Scrapping from NG";
                            break;
                        case CommonData.TransactionSubCode.SH_StockShippingAdjustment:
                            transactionSubCode = "Outgoing adjustment";
                            break;
                        case CommonData.TransactionSubCode.SA_StockArrivingForPurchaseOrder:
                            transactionSubCode = "Stock arriving for purchase order";
                            break;
                        case CommonData.TransactionSubCode.SA_ReturnFromCustomerShipping:
                            transactionSubCode = "Return from customer shipping";
                            break;
                        case CommonData.TransactionSubCode.SA_StockArrivingAdjustment:
                            transactionSubCode = "Incoming adjustment";
                            break;
                        case CommonData.TransactionSubCode.ST_StockTransfer_WH2WH:
                            transactionSubCode = "Stock transfer";
                            break;
                        case CommonData.TransactionSubCode.ST_TransferForProductionLine:
                            transactionSubCode = "Transfer for production line";
                            break;
                        case CommonData.TransactionSubCode.ST_TransferPosting:
                            transactionSubCode = "Transfer posting";
                            break;
                        case CommonData.TransactionSubCode.ST_NGRecycledMaterials:
                            transactionSubCode = "NG recycle materials";
                            break;
                        case CommonData.TransactionSubCode.PhysicalInventory:
                            transactionSubCode = "Physical inventory";
                            break;
                        case CommonData.TransactionSubCode.OpenStock:
                            transactionSubCode = "Open Stock";
                            break;
                        case CommonData.TransactionSubCode.CloseStock:
                            transactionSubCode = "End Stock";
                            break;
                        case CommonData.TransactionSubCode.SubTotal:
                            transactionSubCode = "Sub Total";
                            break;
                    }
                    break;

                case CommonData.Language.VietNamese:
                    switch (code)
                    {
                        case CommonData.TransactionSubCode.SH_StockShippingForDelivery:
                            if (CommonMethod.IsNullOrEmpty(sampleShippingFlg))
                            {
                                transactionSubCode = "Xuất hàng chuyển giao";
                            }
                            else if (sampleShippingFlg == CommonData.SampleShippingFlg.Sample)
                            {
                                transactionSubCode = "Xuất hàng chuyển giao (Hàng mẫu)";
                            }
                            else if (sampleShippingFlg == CommonData.SampleShippingFlg.Shipping)
                            {
                                transactionSubCode = "Xuất hàng chuyển giao (Hàng bán)";
                            }
                            break;
                        case CommonData.TransactionSubCode.SH_ReturnDeliveryToSupplier:
                            transactionSubCode = "Trả hàng cho NCC";
                            break;
                        case CommonData.TransactionSubCode.SH_ScrappingFromNG:
                            transactionSubCode = "Hủy hàng NG";
                            break;
                        case CommonData.TransactionSubCode.SH_StockShippingAdjustment:
                            transactionSubCode = "Điều chỉnh xuất kho";
                            break;
                        case CommonData.TransactionSubCode.SA_StockArrivingForPurchaseOrder:
                            transactionSubCode = "Nhập từ đơn mua hàng";
                            break;
                        case CommonData.TransactionSubCode.SA_ReturnFromCustomerShipping:
                            transactionSubCode = "Trả hàng từ khách hàng";
                            break;
                        case CommonData.TransactionSubCode.SA_StockArrivingAdjustment:
                            transactionSubCode = "Điều chỉnh nhập kho";
                            break;
                        case CommonData.TransactionSubCode.ST_StockTransfer_WH2WH:
                            transactionSubCode = "Luân chuyển";
                            break;
                        case CommonData.TransactionSubCode.ST_TransferForProductionLine:
                            transactionSubCode = "Chuyển công đoạn";
                            break;
                        case CommonData.TransactionSubCode.ST_TransferPosting:
                            transactionSubCode = "Chuyển trạng thái";
                            break;
                        case CommonData.TransactionSubCode.ST_NGRecycledMaterials:
                            transactionSubCode = "Tái sử dụng NVL từ NG";
                            break;
                        case CommonData.TransactionSubCode.PhysicalInventory:
                            transactionSubCode = "Kiểm kho";
                            break;
                        case CommonData.TransactionSubCode.OpenStock:
                            transactionSubCode = "Tồn đầu kỳ";
                            break;
                        case CommonData.TransactionSubCode.CloseStock:
                            transactionSubCode = "Tồn cuối kỳ";
                            break;
                        case CommonData.TransactionSubCode.SubTotal:
                            transactionSubCode = "Tổng cộng";
                            break;
                    }
                    break;

                case CommonData.Language.Japanese:
                    switch (code)
                    {
                        case CommonData.TransactionSubCode.SH_StockShippingForDelivery:
                            if (CommonMethod.IsNullOrEmpty(sampleShippingFlg))
                            {
                                transactionSubCode = "Stock shipping for delivery";
                            }
                            else if (sampleShippingFlg == CommonData.SampleShippingFlg.Sample)
                            {
                                transactionSubCode = "Stock shipping for delivery (Sample)";
                            }
                            else if (sampleShippingFlg == CommonData.SampleShippingFlg.Shipping)
                            {
                                transactionSubCode = "Stock shipping for delivery (Shipping)";
                            }
                            break;
                        case CommonData.TransactionSubCode.SH_ReturnDeliveryToSupplier:
                            transactionSubCode = "Return delivery to supplier";
                            break;
                        case CommonData.TransactionSubCode.SH_ScrappingFromNG:
                            transactionSubCode = "Scrapping from NG";
                            break;
                        case CommonData.TransactionSubCode.SH_StockShippingAdjustment:
                            transactionSubCode = "Outgoing adjustment";
                            break;
                        case CommonData.TransactionSubCode.SA_StockArrivingForPurchaseOrder:
                            transactionSubCode = "Stock arriving for purchase order";
                            break;
                        case CommonData.TransactionSubCode.SA_ReturnFromCustomerShipping:
                            transactionSubCode = "Return from customer shipping";
                            break;
                        case CommonData.TransactionSubCode.SA_StockArrivingAdjustment:
                            transactionSubCode = "Incoming adjustment";
                            break;
                        case CommonData.TransactionSubCode.ST_StockTransfer_WH2WH:
                            transactionSubCode = "Stock transfer";
                            break;
                        case CommonData.TransactionSubCode.ST_TransferForProductionLine:
                            transactionSubCode = "Transfer for production line";
                            break;
                        case CommonData.TransactionSubCode.ST_TransferPosting:
                            transactionSubCode = "Transfer posting";
                            break;
                        case CommonData.TransactionSubCode.ST_NGRecycledMaterials:
                            transactionSubCode = "NG recycle materials";
                            break;
                        case CommonData.TransactionSubCode.PhysicalInventory:
                            transactionSubCode = "Physical inventory";
                            break;
                        case CommonData.TransactionSubCode.OpenStock:
                            transactionSubCode = "Open Stock";
                            break;
                        case CommonData.TransactionSubCode.CloseStock:
                            transactionSubCode = "End Stock";
                            break;
                        case CommonData.TransactionSubCode.SubTotal:
                            transactionSubCode = "Sub Total";
                            break;
                    }
                    break;

                default:
                    switch (code)
                    {
                        case CommonData.TransactionSubCode.SH_StockShippingForDelivery:
                            if (CommonMethod.IsNullOrEmpty(sampleShippingFlg))
                            {
                                transactionSubCode = "Stock shipping for delivery";
                            }
                            else if (sampleShippingFlg == CommonData.SampleShippingFlg.Sample)
                            {
                                transactionSubCode = "Stock shipping for delivery (Sample)";
                            }
                            else if (sampleShippingFlg == CommonData.SampleShippingFlg.Shipping)
                            {
                                transactionSubCode = "Stock shipping for delivery (Shipping)";
                            }
                            break;
                        case CommonData.TransactionSubCode.SH_ReturnDeliveryToSupplier:
                            transactionSubCode = "Return delivery to supplier";
                            break;
                        case CommonData.TransactionSubCode.SH_ScrappingFromNG:
                            transactionSubCode = "Scrapping from NG";
                            break;
                        case CommonData.TransactionSubCode.SH_StockShippingAdjustment:
                            transactionSubCode = "Outgoing adjustment";
                            break;
                        case CommonData.TransactionSubCode.SA_StockArrivingForPurchaseOrder:
                            transactionSubCode = "Stock arriving for purchase order";
                            break;
                        case CommonData.TransactionSubCode.SA_ReturnFromCustomerShipping:
                            transactionSubCode = "Return from customer shipping";
                            break;
                        case CommonData.TransactionSubCode.SA_StockArrivingAdjustment:
                            transactionSubCode = "Incoming adjustment";
                            break;
                        case CommonData.TransactionSubCode.ST_StockTransfer_WH2WH:
                            transactionSubCode = "Stock transfer";
                            break;
                        case CommonData.TransactionSubCode.ST_TransferForProductionLine:
                            transactionSubCode = "Transfer for production line";
                            break;
                        case CommonData.TransactionSubCode.ST_TransferPosting:
                            transactionSubCode = "Transfer posting";
                            break;
                        case CommonData.TransactionSubCode.ST_NGRecycledMaterials:
                            transactionSubCode = "NG recycle materials";
                            break;
                        case CommonData.TransactionSubCode.PhysicalInventory:
                            transactionSubCode = "Physical inventory";
                            break;
                        case CommonData.TransactionSubCode.OpenStock:
                            transactionSubCode = "Open Stock";
                            break;
                        case CommonData.TransactionSubCode.CloseStock:
                            transactionSubCode = "End Stock";
                            break;
                        case CommonData.TransactionSubCode.SubTotal:
                            transactionSubCode = "Sub Total";
                            break;
                    }
                    break;
            }
            return transactionSubCode;
        }


        public static string GetDocumentTypeName(string code)
        {
            string documentType = CommonData.StringEmpty;
            switch (UserSession.LangId)
            {
                case CommonData.Language.VietNamese:
                    switch (code)
                    {
                        case CommonData.DocumentType.Shipping:
                            documentType = "Xuất Kho";
                            break;
                        case CommonData.DocumentType.Arriving:
                            documentType = "Nhập Kho";
                            break;
                        case CommonData.DocumentType.Transfer:
                            documentType = "Chuyển Kho";
                            break;
                        case CommonData.DocumentType.Adjustment:
                            documentType = "Điều chỉnh";
                            break;
                    }
                    break;

                case CommonData.Language.English:
                    switch (code)
                    {
                        case CommonData.DocumentType.Shipping:
                            documentType = "Shipping";
                            break;
                        case CommonData.DocumentType.Arriving:
                            documentType = "Arriving";
                            break;
                        case CommonData.DocumentType.Transfer:
                            documentType = "Transfer";
                            break;
                        case CommonData.DocumentType.Adjustment:
                            documentType = "Adjustment";
                            break;
                    }
                    break;

                case CommonData.Language.Japanese:
                    switch (code)
                    {
                        case CommonData.DocumentType.Shipping:
                            documentType = "Shipping";
                            break;
                        case CommonData.DocumentType.Arriving:
                            documentType = "Arriving";
                            break;
                        case CommonData.DocumentType.Transfer:
                            documentType = "Transfer";
                            break;
                        case CommonData.DocumentType.Adjustment:
                            documentType = "Adjustment";
                            break;
                    }
                    break;

                default:
                    switch (code)
                    {
                        case CommonData.DocumentType.Shipping:
                            documentType = "Shipping";
                            break;
                        case CommonData.DocumentType.Arriving:
                            documentType = "Arriving";
                            break;
                        case CommonData.DocumentType.Transfer:
                            documentType = "Transfer";
                            break;
                        case CommonData.DocumentType.Adjustment:
                            documentType = "Adjustment";
                            break;
                    }
                    break;
            }
            return documentType;
        }

        #endregion

        #region Inventory
        //public static string GetInventorySubCodeName(string code)
        //{
        //    string transactionSubCode = CommonData.StringEmpty;
        //    switch (UserSession.LangId)
        //    {
        //        case CommonData.Language.English:
        //            switch (code)
        //            {
        //                case CommonData.TransactionSubCode.SH_StockShippingForDelivery:
        //                    transactionSubCode = "Stock shipping for delivery";
        //                    break;
        //                case CommonData.TransactionSubCode.SH_ReturnDeliveryToSupplier:
        //                    transactionSubCode = "Return delivery to supplier";
        //                    break;
        //                case CommonData.TransactionSubCode.SH_ScrappingFromNG:
        //                    transactionSubCode = "Scrapping from NG";
        //                    break;
        //                case CommonData.TransactionSubCode.SH_StockShippingAdjustment:
        //                    transactionSubCode = "Stock shipping adjustment";
        //                    break;
        //                case CommonData.TransactionSubCode.SA_StockArrivingForPurchaseOrder:
        //                    transactionSubCode = "Stock arriving for purchase order";
        //                    break;
        //                case CommonData.TransactionSubCode.SA_ReturnFromCustomerShipping:
        //                    transactionSubCode = "Return from customer shipping";
        //                    break;
        //                case CommonData.TransactionSubCode.SA_StockArrivingAdjustment:
        //                    transactionSubCode = "Stock arriving adjustment";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_StockTransfer_WH2WH:
        //                    transactionSubCode = "Stock transfer - warehouse to warehouse";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_TransferForProductionLine:
        //                    transactionSubCode = "Transfer for production line";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_TransferPosting:
        //                    transactionSubCode = "Transfer posting";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_NGRecycledMaterials:
        //                    transactionSubCode = "NG recycled materials";
        //                    break;
        //                case CommonData.TransactionSubCode.OpenStock:
        //                    transactionSubCode = "Open Stock";
        //                    break;
        //                case CommonData.TransactionSubCode.CloseStock:
        //                    transactionSubCode = "End Stock";
        //                    break;
        //                case CommonData.TransactionSubCode.SubTotal:
        //                    transactionSubCode = "Sub Total";
        //                    break;
        //            }
        //            break;

        //        case CommonData.Language.VietNamese:
        //            switch (code)
        //            {
        //                case CommonData.TransactionSubCode.SH_StockShippingForDelivery:
        //                    transactionSubCode = "Xuất hàng chuyển giao";
        //                    break;
        //                case CommonData.TransactionSubCode.SH_ReturnDeliveryToSupplier:
        //                    transactionSubCode = "Trả hàng cho NCC";
        //                    break;
        //                case CommonData.TransactionSubCode.SH_ScrappingFromNG:
        //                    transactionSubCode = "Hủy hàng NG";
        //                    break;
        //                case CommonData.TransactionSubCode.SH_StockShippingAdjustment:
        //                    transactionSubCode = "Điều chỉnh xuất kho";
        //                    break;
        //                case CommonData.TransactionSubCode.SA_StockArrivingForPurchaseOrder:
        //                    transactionSubCode = "Nhập từ đơn mua hàng";
        //                    break;
        //                case CommonData.TransactionSubCode.SA_ReturnFromCustomerShipping:
        //                    transactionSubCode = "Trả hàng từ khách hàng";
        //                    break;
        //                case CommonData.TransactionSubCode.SA_StockArrivingAdjustment:
        //                    transactionSubCode = "Điều chỉnh nhập kho";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_StockTransfer_WH2WH:
        //                    transactionSubCode = "Luân chuyển - Từ kho qua kho";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_TransferForProductionLine:
        //                    transactionSubCode = "Chuyển công đoạn";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_TransferPosting:
        //                    transactionSubCode = "Chuyển trạng thái";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_NGRecycledMaterials:
        //                    transactionSubCode = "Tái sử dụng NVL từ NG";
        //                    break;
        //                case CommonData.TransactionSubCode.OpenStock:
        //                    transactionSubCode = "Tồn đầu kỳ";
        //                    break;
        //                case CommonData.TransactionSubCode.CloseStock:
        //                    transactionSubCode = "Tồn cuối kỳ";
        //                    break;
        //                case CommonData.TransactionSubCode.SubTotal:
        //                    transactionSubCode = "Tổng cộng";
        //                    break;
        //            }
        //            break;

        //        case CommonData.Language.Japanese:
        //            switch (code)
        //            {
        //                case CommonData.TransactionSubCode.SH_StockShippingForDelivery:
        //                    transactionSubCode = "Stock shipping for delivery";
        //                    break;
        //                case CommonData.TransactionSubCode.SH_ReturnDeliveryToSupplier:
        //                    transactionSubCode = "Return delivery to supplier";
        //                    break;
        //                case CommonData.TransactionSubCode.SH_ScrappingFromNG:
        //                    transactionSubCode = "Scrapping from NG";
        //                    break;
        //                case CommonData.TransactionSubCode.SH_StockShippingAdjustment:
        //                    transactionSubCode = "Stock shipping adjustment";
        //                    break;
        //                case CommonData.TransactionSubCode.SA_StockArrivingForPurchaseOrder:
        //                    transactionSubCode = "Stock arriving for purchase order";
        //                    break;
        //                case CommonData.TransactionSubCode.SA_ReturnFromCustomerShipping:
        //                    transactionSubCode = "Return from customer shipping";
        //                    break;
        //                case CommonData.TransactionSubCode.SA_StockArrivingAdjustment:
        //                    transactionSubCode = "Stock arriving adjustment";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_StockTransfer_WH2WH:
        //                    transactionSubCode = "Stock transfer - warehouse to warehouse";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_TransferForProductionLine:
        //                    transactionSubCode = "Transfer for production line";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_TransferPosting:
        //                    transactionSubCode = "Transfer posting";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_NGRecycledMaterials:
        //                    transactionSubCode = "NG recycled materials";
        //                    break;
        //                case CommonData.TransactionSubCode.OpenStock:
        //                    transactionSubCode = "Open Stock";
        //                    break;
        //                case CommonData.TransactionSubCode.CloseStock:
        //                    transactionSubCode = "End Stock";
        //                    break;
        //                case CommonData.TransactionSubCode.SubTotal:
        //                    transactionSubCode = "Sub Total";
        //                    break;
        //            }
        //            break;

        //        default:
        //            switch (code)
        //            {
        //                case CommonData.TransactionSubCode.SH_StockShippingForDelivery:
        //                    transactionSubCode = "Stock shipping for delivery";
        //                    break;
        //                case CommonData.TransactionSubCode.SH_ReturnDeliveryToSupplier:
        //                    transactionSubCode = "Return delivery to supplier";
        //                    break;
        //                case CommonData.TransactionSubCode.SH_ScrappingFromNG:
        //                    transactionSubCode = "Scrapping from NG";
        //                    break;
        //                case CommonData.TransactionSubCode.SH_StockShippingAdjustment:
        //                    transactionSubCode = "Stock shipping adjustment";
        //                    break;
        //                case CommonData.TransactionSubCode.SA_StockArrivingForPurchaseOrder:
        //                    transactionSubCode = "Stock arriving for purchase order";
        //                    break;
        //                case CommonData.TransactionSubCode.SA_ReturnFromCustomerShipping:
        //                    transactionSubCode = "Return from customer shipping";
        //                    break;
        //                case CommonData.TransactionSubCode.SA_StockArrivingAdjustment:
        //                    transactionSubCode = "Stock arriving adjustment";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_StockTransfer_WH2WH:
        //                    transactionSubCode = "Stock transfer - warehouse to warehouse";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_TransferForProductionLine:
        //                    transactionSubCode = "Transfer for production line";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_TransferPosting:
        //                    transactionSubCode = "Transfer posting";
        //                    break;
        //                case CommonData.TransactionSubCode.ST_NGRecycledMaterials:
        //                    transactionSubCode = "NG recycled materials";
        //                    break;
        //                case CommonData.TransactionSubCode.OpenStock:
        //                    transactionSubCode = "Open Stock";
        //                    break;
        //                case CommonData.TransactionSubCode.CloseStock:
        //                    transactionSubCode = "End Stock";
        //                    break;
        //                case CommonData.TransactionSubCode.SubTotal:
        //                    transactionSubCode = "Sub Total";
        //                    break;
        //            }
        //            break;
        //    }
        //    return transactionSubCode;
        //}
        #endregion

        #region Sys
        /// <summary>
        /// get IP Address of own Computer
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            try
            {
                //string strHostName = string.Empty;tFirstOrDefa
                //strHostName = System.Net.Dns.GetHostName();
                //IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
                //IPAddress[] addr = ipEntry.AddressList;
                //return addr[addr.Length - 1].ToString();
                IPHostEntry host;
                string localIP = string.Empty;
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        localIP = ip.ToString();
                    }
                }
                return localIP;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// get computer name of own computer
        /// </summary>
        /// <returns></returns>
        public static string GetComputerName()
        {
            try
            {
                return Environment.MachineName;
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion 

        public static void CopyDirectory(string strSource, string strDestination)
        {
            if (!Directory.Exists(strDestination))
            {
                Directory.CreateDirectory(strDestination);
            }
            DirectoryInfo dirInfo = new DirectoryInfo(strSource);
            FileInfo[] files = dirInfo.GetFiles();
            foreach (FileInfo tempfile in files)
            {
                tempfile.CopyTo(Path.Combine(strDestination, tempfile.Name), true);
            }
            DirectoryInfo[] dirctororys = dirInfo.GetDirectories();
            foreach (DirectoryInfo tempdir in dirctororys)
            {
                CopyDirectory(Path.Combine(strSource, tempdir.Name), Path.Combine(strDestination, tempdir.Name));
            }

        }

        public static bool CompareDataTable(DataTable dt1, DataTable dt2)
        {
            if (dt1.Rows.Count != dt2.Rows.Count || dt1.Columns.Count != dt2.Columns.Count)
                return false;


            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int c = 0; c < dt1.Columns.Count; c++)
                {
                    if (!Equals(dt1.Rows[i][c], dt2.Rows[i][c]))
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Round a number
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal Round(object obj)
        {
            if (obj == null)
            {
                return 0;
            }

            return Math.Round(ParseDecimal(obj), MidpointRounding.AwayFromZero);
        }

        public static string ParseString(this DateTime? dateTime, string format)
        {
            if (dateTime == null)
                return string.Empty;
            else if (dateTime.Value.Year == 1)
                return string.Empty;
            return dateTime.Value.ToString(format);
        }

        public static string ParseString(this DateTime dateTime, string format)
        {
            if (dateTime == null)
                return string.Empty;
            else if (dateTime.Year == 1)
                return string.Empty;
            return dateTime.ToString(format);
        }

        public static DateTime? ConvertStringToDateTime(this string dateTime, string format)
        {
            try
            {
                return DateTime.ParseExact(dateTime, format, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string EncodeUrl(string encodeStr)
        {
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeStr);
            return Convert.ToBase64String(encoded);
        }
        public static string DecodeUrl(string decodeStr)
        {
            byte[] encoded = Convert.FromBase64String(decodeStr);
            return System.Text.Encoding.UTF8.GetString(encoded);
        }

        public static string Encrypt(this string plainText)
        {
            string key = "jdsg432387#";
            byte[] EncryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            EncryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByte = Encoding.UTF8.GetBytes(plainText);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write);
            cStream.Write(inputByte, 0, inputByte.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        public static string Decrypt(this string encryptedText)
        {
            string key = "jdsg432387#";
            byte[] DecryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            byte[] inputByte = new byte[encryptedText.Length];

            DecryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByte = Convert.FromBase64String(encryptedText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(DecryptKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByte, 0, inputByte.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
    }

    #region Serilization
    public class ObjectSerializer<T> where T : class
    {
        public static T LoadFromFile(string fileName)
        {
            string str = "";
            try
            {
                StreamReader reader = File.OpenText(fileName);
                str = reader.ReadToEnd();
                reader.Close();
            }
            catch (IOException exception)
            {
                return default(T);
            }
            if (string.IsNullOrEmpty(str))
            {
                return default(T);
            }
            return ObjectSerializer<T>.LoadFromXML(str);

        }




        public static bool SaveToFile(T t, string fileName, bool autoCreateFile)
        {
            string str = ObjectSerializer<T>.ToXMLString(t);
            try
            {
                if (!File.Exists(fileName) && !autoCreateFile)
                {
                    return false;
                }
                StreamWriter writer = File.CreateText(fileName);
                writer.Write(str);
                writer.Close();
                return true;
            }
            catch (IOException exception)
            {
                
                return false;
            }
        }


        public static T LoadFromXML(string strXML)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                StringReader textReader = new StringReader(strXML);
                T local = (T)serializer.Deserialize(textReader);
                textReader.Close();
                return local;
            }
            catch (Exception)
            {
                return default(T);
            }
        }


        private static string ToXMLString(T tobject)
        {
            string message;
            MemoryStream stream = new MemoryStream();
            try
            {
                new XmlSerializer(typeof(T)).Serialize(stream, tobject);
                stream.Seek(0L, SeekOrigin.Begin);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
                string str = Encoding.UTF8.GetString(buffer, 0, (int)stream.Length);
                stream.Close();
                message = str;
            }
            catch (Exception exception)
            {               
                message = exception.Message;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return message;
        }
    }
    #endregion
}