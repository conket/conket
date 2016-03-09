using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Ivs.Core.Common;
using Ivs.Core.Data;
using Ivs.Core.Logger;
using Nihongo.Dal.Mapping;
using Nihongo.Dal.Data;

namespace Nihongo.Dal.Dao
{
    public class BaseDao : Entities, IDisposable
    {
        //public BaseDao()
        //    : base(GetConnectionString())
        //{

        //    ApplicationState.SetValue("Lock", true);
        //    this.CommandTimeout = 60;
        //}

        public BaseDao()
            : base(GetConnectionString())
        {

            ApplicationState.SetValue("Lock", true);
            this.CommandTimeout = 60;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            ApplicationState.SetValue("Lock", false);
        }

        #region Properties

        public Entities DBContext
        {
            get;
            set;
        }

        public System.Data.Common.DbTransaction Transaction
        {
            get;
            set;
        }

        //private List<List<History>> _LstLstHistory = new List<List<History>>();

        //public List<List<History>> LstLstHistory
        //{
        //    get { return _LstLstHistory; }
        //    set { _LstLstHistory = value; }
        //}

        #endregion Properties

        #region Create and Get EntityConnection to metadata for model of databasefirst

        //=============StartUpdate 12/31/2013(HMHieu)=====================

        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            // Set the properties for the data source.

            if (DbConfig.Mode == CommonData.Status.Disable)
            {
                sqlBuilder.DataSource = DbConfig.ServerName;
                sqlBuilder.InitialCatalog = DbConfig.DbName;
                sqlBuilder.UserID = DbConfig.Username;
                sqlBuilder.Password = DbConfig.PassWord;
            }
            else
            {
                sqlBuilder.DataSource = @"nihongovoca.mssql.somee.com";
                sqlBuilder.InitialCatalog = "nihongovoca";
                sqlBuilder.PersistSecurityInfo = false;
                sqlBuilder.UserID = "chkien0911_SQLLogin_1";
                sqlBuilder.Password = "chkien0911";
            }

            sqlBuilder.MultipleActiveResultSets = true;
            // sqlBuilder.IntegratedSecurity = true;


            // Build the SqlConnection connection string.
            string providerString = sqlBuilder.ToString();

            var entityBuilder = new EntityConnectionStringBuilder();

            // Initialize the EntityConnectionStringBuilder.
            //Set the provider name.

            entityBuilder.Provider = "System.Data.SqlClient";

            providerString += "; Connection Timeout = 60";
            // Set the provider-specific connection string.
            entityBuilder.ProviderConnectionString = providerString;

            // Set the Metadata location.
            var modelName = DbConfig.MetadataModel;//"EFModelsMSSQL";
            entityBuilder.Metadata = string.Format(@"res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl", modelName);

            return entityBuilder.ToString();
            //return new EntityConnection(entityBuilder.ToString());
        }

        //==============================================================

        #endregion Create and Get EntityConnection to metadata for model of databasefirst

        #region Execute

        /// <summary>
        /// Begin Transaction
        /// </summary>
        public void BeginTransaction()
        {
            if (base.Connection.State != ConnectionState.Open)
            {
                base.Connection.Open();
                Transaction = base.Connection.BeginTransaction();
            }
        }


        /// <summary>
        /// Begin Transaction
        /// </summary>
        public void BeginTransaction(IsolationLevel isolateLevel)
        {
            if (base.Connection.State != ConnectionState.Open)
            {
                base.Connection.Open();
                Transaction = base.Connection.BeginTransaction(isolateLevel);
            }
        }

        /// <summary>
        /// Commit Transaction
        /// </summary>
        public int Commit()
        {
            int returnCode = 0;
            try
            {
                if (base.Connection.State == ConnectionState.Open)
                {
                    Transaction.Commit();
                    base.Connection.Close();
                }
            }
            catch (System.Exception ex)
            {
                this.Rollback();
                returnCode = ProcessDbException(ex);
            }
            return returnCode;
        }

        /// <summary>
        /// Rollback Transaction
        /// </summary>
        public int Rollback()
        {
            int returnCode = 0;
            try
            {
                if (base.Connection.State == ConnectionState.Open)
                {
                    Transaction.Rollback();
                    base.Connection.Close();
                }
            }
            catch (System.Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }
            return returnCode;
        }

        /// <summary>
        /// Use for insert, update, delete
        /// </summary>
        /// <returns></returns>
        public int Saves()
        {
            int returnCode = 0;

            try
            {
                int rows = base.SaveChanges();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        /// <summary>
        /// Save the change and save history
        /// </summary>
        /// <param name="action"> </param>
        /// <param name="dto">object need to save</param>
        /// <param name="operID">id of action</param>
        /// <param name="functiongGr"> </param>
        /// <param name="tableName"> </param>
        /// <returns></returns>
        public int Saves(string functiongGr, string action, object dto, string tableName)
        {
            var returnCode = 0;
            try
            {
                //Save data
                int row = base.SaveChanges();

            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            //returnCode = 0;

            return returnCode;
        }

        public int Saves(string functiongGr, string action, string userCode, string userName, string computerName, string employeeID, object dto, string tableName)
        {
            var returnCode = 0;
            try
            {
                //Save data
                int row = base.SaveChanges();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            //returnCode = 0;

            return returnCode;
        }

        #endregion Execute

        #region Save History


        /// <summary>
        /// History to insert System Journal
        /// </summary>
        //public class History
        //{
        //    public string Field { get; set; }
        //    public object Value { get; set; }
        //}

        //private int saveHitory(string FunctiongGr, string action, List<List<History>> LstLstHistory, string tableName)
        //{
        //    sy_systemjournals hisDTO;

        //    // initial sys_historydetail object
        //    sy_systemjournaldetail hisDetailDto;

        //    int numRow = 0;

        //    foreach (var lstHis in LstLstHistory)
        //    {
        //        numRow = 0;
        //        hisDTO = new sy_systemjournals();
        //        hisDTO.TableName = tableName;
        //        hisDTO.Action = action;
        //        hisDTO.ActionDate = DateTime.Now;
        //        hisDTO.UserCode = UserSession.UserCode;
        //        hisDTO.UserName = UserSession.UserName;
        //        hisDTO.EmployeeID = UserSession.EmployeeID;
        //        //hisDTO.EmployeeName = ;
        //        hisDTO.FunctionID = FunctiongGr;
        //        hisDTO.ComputerName = UserSession.ComputerName;

        //        hisDetailDto = new sy_systemjournaldetail();

        //        foreach (var his in lstHis)
        //        {
        //            string name = his.Field;
        //            string value = string.Empty;

        //            PropertyInfo[] proInfo = his.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //            if (proInfo[1].GetValue(his,null) != null)
        //                value = proInfo[1].GetValue(his, null).ToString();

        //            #region Set value his detail

        //            if (!name.Equals(string.Empty))
        //            {
        //                numRow++;
        //                switch (numRow)
        //                {
        //                    #region "Set value for sys_historydetail object"
        //                    case 1:
        //                        hisDetailDto.FieldName1 = name;
        //                        hisDetailDto.FieldValue1 = value;
        //                        break;
        //                    case 2:
        //                        hisDetailDto.FieldName2 = name;
        //                        hisDetailDto.FieldValue2 = value;
        //                        break;
        //                    case 3:
        //                        hisDetailDto.FieldName3 = name;
        //                        hisDetailDto.FieldValue3 = value;
        //                        break;
        //                    case 4:
        //                        hisDetailDto.FieldName4 = name;
        //                        hisDetailDto.FieldValue4 = value;
        //                        break;
        //                    case 5:
        //                        hisDetailDto.FieldName5 = name;
        //                        hisDetailDto.FieldValue5 = value;
        //                        break;
        //                    case 6:
        //                        hisDetailDto.FieldName6 = name;
        //                        hisDetailDto.FieldValue6 = value;
        //                        break;
        //                    case 7:
        //                        hisDetailDto.FieldName7 = name;
        //                        hisDetailDto.FieldValue7 = value;
        //                        break;
        //                    case 8:
        //                        hisDetailDto.FieldName8 = name;
        //                        hisDetailDto.FieldValue8 = value;
        //                        break;
        //                    case 9:
        //                        hisDetailDto.FieldName9 = name;
        //                        hisDetailDto.FieldValue9 = value;
        //                        break;
        //                    case 10:
        //                        hisDetailDto.FieldName10 = name;
        //                        hisDetailDto.FieldValue10 = value;
        //                        break;

        //                    case 11:
        //                        hisDetailDto.FieldName11 = name;
        //                        hisDetailDto.FieldValue11 = value;
        //                        break;
        //                    case 12:
        //                        hisDetailDto.FieldName12 = name;
        //                        hisDetailDto.FieldValue12 = value;
        //                        break;
        //                    case 13:
        //                        hisDetailDto.FieldName13 = name;
        //                        hisDetailDto.FieldValue13 = value;
        //                        break;
        //                    case 14:
        //                        hisDetailDto.FieldName14 = name;
        //                        hisDetailDto.FieldValue14 = value;
        //                        break;
        //                    case 15:
        //                        hisDetailDto.FieldName15 = name;
        //                        hisDetailDto.FieldValue15 = value;
        //                        break;
        //                    case 16:
        //                        hisDetailDto.FieldName16 = name;
        //                        hisDetailDto.FieldValue16 = value;
        //                        break;
        //                    case 17:
        //                        hisDetailDto.FieldName17 = name;
        //                        hisDetailDto.FieldValue17 = value;
        //                        break;
        //                    case 18:
        //                        hisDetailDto.FieldName18 = name;
        //                        hisDetailDto.FieldValue18 = value;
        //                        break;
        //                    case 19:
        //                        hisDetailDto.FieldName19 = name;
        //                        hisDetailDto.FieldValue19 = value;
        //                        break;
        //                    case 20:
        //                        hisDetailDto.FieldName20 = name;
        //                        hisDetailDto.FieldValue20 = value;
        //                        break;

        //                    case 21:
        //                        hisDetailDto.FieldName21 = name;
        //                        hisDetailDto.FieldValue21 = value;
        //                        break;
        //                    case 22:
        //                        hisDetailDto.FieldName22 = name;
        //                        hisDetailDto.FieldValue22 = value;
        //                        break;
        //                    case 23:
        //                        hisDetailDto.FieldName23 = name;
        //                        hisDetailDto.FieldValue23 = value;
        //                        break;
        //                    case 24:
        //                        hisDetailDto.FieldName24 = name;
        //                        hisDetailDto.FieldValue24 = value;
        //                        break;
        //                    case 25:
        //                        hisDetailDto.FieldName25 = name;
        //                        hisDetailDto.FieldValue25 = value;
        //                        break;
        //                    case 26:
        //                        hisDetailDto.FieldName26 = name;
        //                        hisDetailDto.FieldValue26 = value;
        //                        break;
        //                    case 27:
        //                        hisDetailDto.FieldName27 = name;
        //                        hisDetailDto.FieldValue27 = value;
        //                        break;
        //                    case 28:
        //                        hisDetailDto.FieldName28 = name;
        //                        hisDetailDto.FieldValue28 = value;
        //                        break;
        //                    case 29:
        //                        hisDetailDto.FieldName29 = name;
        //                        hisDetailDto.FieldValue29 = value;
        //                        break;
        //                    case 30:
        //                        hisDetailDto.FieldName30 = name;
        //                        hisDetailDto.FieldValue30 = value;
        //                        break;

        //                    case 31:
        //                        hisDetailDto.FieldName31 = name;
        //                        hisDetailDto.FieldValue31 = value;
        //                        break;
        //                    case 32:
        //                        hisDetailDto.FieldName32 = name;
        //                        hisDetailDto.FieldValue32 = value;
        //                        break;
        //                    case 33:
        //                        hisDetailDto.FieldName33 = name;
        //                        hisDetailDto.FieldValue33 = value;
        //                        break;
        //                    case 34:
        //                        hisDetailDto.FieldName34 = name;
        //                        hisDetailDto.FieldValue34 = value;
        //                        break;
        //                    case 35:
        //                        hisDetailDto.FieldName35 = name;
        //                        hisDetailDto.FieldValue35 = value;
        //                        break;
        //                    case 36:
        //                        hisDetailDto.FieldName36 = name;
        //                        hisDetailDto.FieldValue36 = value;
        //                        break;
        //                    case 37:
        //                        hisDetailDto.FieldName37 = name;
        //                        hisDetailDto.FieldValue37 = value;
        //                        break;
        //                    case 38:
        //                        hisDetailDto.FieldName38 = name;
        //                        hisDetailDto.FieldValue38 = value;
        //                        break;
        //                    case 39:
        //                        hisDetailDto.FieldName39 = name;
        //                        hisDetailDto.FieldValue39 = value;
        //                        break;
        //                    case 40:
        //                        hisDetailDto.FieldName40 = name;
        //                        hisDetailDto.FieldValue40 = value;
        //                        break;

        //                    case 41:
        //                        hisDetailDto.FieldName41 = name;
        //                        hisDetailDto.FieldValue41 = value;
        //                        break;
        //                    case 42:
        //                        hisDetailDto.FieldName42 = name;
        //                        hisDetailDto.FieldValue42 = value;
        //                        break;
        //                    case 43:
        //                        hisDetailDto.FieldName43 = name;
        //                        hisDetailDto.FieldValue43 = value;
        //                        break;
        //                    case 44:
        //                        hisDetailDto.FieldName44 = name;
        //                        hisDetailDto.FieldValue44 = value;
        //                        break;
        //                    case 45:
        //                        hisDetailDto.FieldName45 = name;
        //                        hisDetailDto.FieldValue45 = value;
        //                        break;
        //                    case 46:
        //                        hisDetailDto.FieldName46 = name;
        //                        hisDetailDto.FieldValue46 = value;
        //                        break;
        //                    case 47:
        //                        hisDetailDto.FieldName47 = name;
        //                        hisDetailDto.FieldValue47 = value;
        //                        break;
        //                    case 48:
        //                        hisDetailDto.FieldName48 = name;
        //                        hisDetailDto.FieldValue48 = value;
        //                        break;
        //                    case 49:
        //                        hisDetailDto.FieldName49 = name;
        //                        hisDetailDto.FieldValue49 = value;
        //                        break;
        //                    case 50:
        //                        hisDetailDto.FieldName50 = name;
        //                        hisDetailDto.FieldValue50 = value;
        //                        break;

        //                    case 51:
        //                        hisDetailDto.FieldName51 = name;
        //                        hisDetailDto.FieldValue51 = value;
        //                        break;
        //                    case 52:
        //                        hisDetailDto.FieldName52 = name;
        //                        hisDetailDto.FieldValue52 = value;
        //                        break;
        //                    case 53:
        //                        hisDetailDto.FieldName53 = name;
        //                        hisDetailDto.FieldValue53 = value;
        //                        break;
        //                    case 54:
        //                        hisDetailDto.FieldName54 = name;
        //                        hisDetailDto.FieldValue54 = value;
        //                        break;
        //                    case 55:
        //                        hisDetailDto.FieldName55 = name;
        //                        hisDetailDto.FieldValue55 = value;
        //                        break;
        //                    case 56:
        //                        hisDetailDto.FieldName56 = name;
        //                        hisDetailDto.FieldValue56 = value;
        //                        break;
        //                    case 57:
        //                        hisDetailDto.FieldName57 = name;
        //                        hisDetailDto.FieldValue57 = value;
        //                        break;
        //                    case 58:
        //                        hisDetailDto.FieldName58 = name;
        //                        hisDetailDto.FieldValue58 = value;
        //                        break;
        //                    case 59:
        //                        hisDetailDto.FieldName59 = name;
        //                        hisDetailDto.FieldValue59 = value;
        //                        break;
        //                    case 60:
        //                        hisDetailDto.FieldName60 = name;
        //                        hisDetailDto.FieldValue60 = value;
        //                        break;

        //                    case 61:
        //                        hisDetailDto.FieldName61 = name;
        //                        hisDetailDto.FieldValue61 = value;
        //                        break;
        //                    case 62:
        //                        hisDetailDto.FieldName62 = name;
        //                        hisDetailDto.FieldValue62 = value;
        //                        break;
        //                    case 63:
        //                        hisDetailDto.FieldName63 = name;
        //                        hisDetailDto.FieldValue63 = value;
        //                        break;
        //                    case 64:
        //                        hisDetailDto.FieldName64 = name;
        //                        hisDetailDto.FieldValue64 = value;
        //                        break;
        //                    case 65:
        //                        hisDetailDto.FieldName65 = name;
        //                        hisDetailDto.FieldValue65 = value;
        //                        break;
        //                    case 66:
        //                        hisDetailDto.FieldName66 = name;
        //                        hisDetailDto.FieldValue66 = value;
        //                        break;
        //                    case 67:
        //                        hisDetailDto.FieldName67 = name;
        //                        hisDetailDto.FieldValue67 = value;
        //                        break;
        //                    case 68:
        //                        hisDetailDto.FieldName68 = name;
        //                        hisDetailDto.FieldValue68 = value;
        //                        break;
        //                    case 69:
        //                        hisDetailDto.FieldName69 = name;
        //                        hisDetailDto.FieldValue69 = value;
        //                        break;
        //                    case 70:
        //                        hisDetailDto.FieldName70 = name;
        //                        hisDetailDto.FieldValue70 = value;
        //                        break;

        //                    case 71:
        //                        hisDetailDto.FieldName71 = name;
        //                        hisDetailDto.FieldValue71 = value;
        //                        break;
        //                    case 72:
        //                        hisDetailDto.FieldName72 = name;
        //                        hisDetailDto.FieldValue72 = value;
        //                        break;
        //                    case 73:
        //                        hisDetailDto.FieldName73 = name;
        //                        hisDetailDto.FieldValue73 = value;
        //                        break;
        //                    case 74:
        //                        hisDetailDto.FieldName74 = name;
        //                        hisDetailDto.FieldValue74 = value;
        //                        break;
        //                    case 75:
        //                        hisDetailDto.FieldName75 = name;
        //                        hisDetailDto.FieldValue75 = value;
        //                        break;
        //                    case 76:
        //                        hisDetailDto.FieldName76 = name;
        //                        hisDetailDto.FieldValue76 = value;
        //                        break;
        //                    case 77:
        //                        hisDetailDto.FieldName77 = name;
        //                        hisDetailDto.FieldValue77 = value;
        //                        break;
        //                    case 78:
        //                        hisDetailDto.FieldName78 = name;
        //                        hisDetailDto.FieldValue78 = value;
        //                        break;
        //                    case 79:
        //                        hisDetailDto.FieldName79 = name;
        //                        hisDetailDto.FieldValue79 = value;
        //                        break;
        //                    case 80:
        //                        hisDetailDto.FieldName80 = name;
        //                        hisDetailDto.FieldValue80 = value;
        //                        break;

        //                    case 81:
        //                        hisDetailDto.FieldName81 = name;
        //                        hisDetailDto.FieldValue81 = value;
        //                        break;
        //                    case 82:
        //                        hisDetailDto.FieldName82 = name;
        //                        hisDetailDto.FieldValue82 = value;
        //                        break;
        //                    case 83:
        //                        hisDetailDto.FieldName83 = name;
        //                        hisDetailDto.FieldValue83 = value;
        //                        break;
        //                    case 84:
        //                        hisDetailDto.FieldName84 = name;
        //                        hisDetailDto.FieldValue84 = value;
        //                        break;
        //                    case 85:
        //                        hisDetailDto.FieldName85 = name;
        //                        hisDetailDto.FieldValue85 = value;
        //                        break;
        //                    case 86:
        //                        hisDetailDto.FieldName86 = name;
        //                        hisDetailDto.FieldValue86 = value;
        //                        break;
        //                    case 87:
        //                        hisDetailDto.FieldName87 = name;
        //                        hisDetailDto.FieldValue87 = value;
        //                        break;
        //                    case 88:
        //                        hisDetailDto.FieldName88 = name;
        //                        hisDetailDto.FieldValue88 = value;
        //                        break;
        //                    case 89:
        //                        hisDetailDto.FieldName89 = name;
        //                        hisDetailDto.FieldValue89 = value;
        //                        break;
        //                    case 90:
        //                        hisDetailDto.FieldName90 = name;
        //                        hisDetailDto.FieldValue90 = value;
        //                        break;

        //                    case 91:
        //                        hisDetailDto.FieldName91 = name;
        //                        hisDetailDto.FieldValue91 = value;
        //                        break;
        //                    case 92:
        //                        hisDetailDto.FieldName92 = name;
        //                        hisDetailDto.FieldValue92 = value;
        //                        break;
        //                    case 93:
        //                        hisDetailDto.FieldName93 = name;
        //                        hisDetailDto.FieldValue93 = value;
        //                        break;
        //                    case 94:
        //                        hisDetailDto.FieldName94 = name;
        //                        hisDetailDto.FieldValue94 = value;
        //                        break;
        //                    case 95:
        //                        hisDetailDto.FieldName95 = name;
        //                        hisDetailDto.FieldValue95 = value;
        //                        break;
        //                    case 96:
        //                        hisDetailDto.FieldName96 = name;
        //                        hisDetailDto.FieldValue96 = value;
        //                        break;
        //                    case 97:
        //                        hisDetailDto.FieldName97 = name;
        //                        hisDetailDto.FieldValue97 = value;
        //                        break;
        //                    case 98:
        //                        hisDetailDto.FieldName98 = name;
        //                        hisDetailDto.FieldValue98 = value;
        //                        break;
        //                    case 99:
        //                        hisDetailDto.FieldName99 = name;
        //                        hisDetailDto.FieldValue99 = value;
        //                        break;
        //                    case 100:
        //                        hisDetailDto.FieldName100 = name;
        //                        hisDetailDto.FieldValue100 = value;
        //                        break;
        //                    #endregion
        //                }
        //            }
        //            #endregion
        //        }
        //        // add sys_historydetail object into sys_history object
        //        hisDTO.sy_systemjournaldetail.Add(hisDetailDto);
        //        // save object imformation into database
        //        // add dto object into sys_history object
        //        this.sy_systemjournals.AddObject(hisDTO);
        //    }
        //    return SaveChanges();

        //}

        #endregion Save History

        #region Convert

        public DataTable ToDataTable<T>(IEnumerable<T> items)
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

        #region Exception

        /// <summary>
        /// handel Sql Exception
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>
        /// a number: base on MySql exception number
        /// 0: Delete successful
        /// 1: Access denied, login to database fail(invalid username, invalid password)
        /// 2: Invalid host, cannot find server(host) that set in app config file
        /// 3: Invalid database, cannot find database that set in DbConfig file
        /// 4: Lost connection, cannot connect to database because lost connection
        /// 5: Duplicate key: insert Primary Key or Unique Key that already exist in database
        /// 6: Forgeign key not exist: insert a foreign key that not exist in primary key
        /// 7: Foreign Key Violation: Foreign Key Violation (delete primary key that is foreign key in other table)
        /// 8: Data not found: Delete or Update data that not exist in database
        /// 9: Exception occured: other exception
        /// </returns>
        protected int ProcessDbException(Exception ex)
        {
            int returnCode = 0;
            try
            {
                switch (DbConfig.ServerType)
                {
                    case DbConfig.DbType.MySql:
                        returnCode = ProcessMySqlException(ex);
                        break;

                    case DbConfig.DbType.MSSQL:
                        returnCode = ProcessMsSqlException(ex);
                        break;

                    default:
                        returnCode = CommonData.DbReturnCode.ExceptionOccured;
                        break;
                }
            }
            catch
            {
                returnCode = CommonData.DbReturnCode.ExceptionOccured;
            }

            #region Logger
            //Logger.Log.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().Name);
            //Logger.Log.Write(Logger.Level.Error, ex);
            #endregion

            return returnCode;
        }

        /// <summary>
        /// check Exception in MySql DBMS
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>
        /// a number: base on MySql exception number
        /// 0: Delete successful
        /// 1: Access denied, login to database fail(invalid username, invalid password)
        /// 2: Invalid host, cannot find server(host) that set in app config file
        /// 3: Invalid database, cannot find database that set in DbConfig file
        /// 4: Lost connection, cannot connect to database because lost connection
        /// 5: Duplicate key: insert Primary Key or Unique Key that already exist in database
        /// 6: Forgeign key not exist: insert a foreign key that not exist in primary key
        /// 7: Foreign Key Violation: Foreign Key Violation (delete primary key that is foreign key in other table)
        /// 8: Data not found: Delete or Update data that not exist in database
        /// 9: Exception occured: other exception
        /// </returns>
        private int ProcessMySqlException(Exception ex)
        {
            int returnCode = 0;

            //MySql.Data.MySqlClient.MySqlException mySqlEx = (MySql.Data.MySqlClient.MySqlException)ex.InnerException;

            //switch (mySqlEx.Number)
            //{
            //    case MySqlErrorNumber.AccessDenied:
            //        returnCode = CommonData.DbReturnCode.AccessDenied;
            //        break;

            //    case MySqlErrorNumber.DuplicateKey:
            //        returnCode = CommonData.DbReturnCode.DuplicateKey;
            //        break;

            //    case MySqlErrorNumber.ForgeignKeyNotExist:
            //        returnCode = CommonData.DbReturnCode.ForgeignKeyNotExist;
            //        break;

            //    case MySqlErrorNumber.ForgeignKeyViolation:
            //        returnCode = CommonData.DbReturnCode.ForeignKeyViolation;
            //        break;

            //    case MySqlErrorNumber.InvalidDatabase:
            //        returnCode = CommonData.DbReturnCode.InvalidDatabase;
            //        break;

            //    case MySqlErrorNumber.InvalidHost:
            //        returnCode = CommonData.DbReturnCode.InvalidHost;
            //        break;

            //    case MySqlErrorNumber.LostConnection:
            //        returnCode = CommonData.DbReturnCode.LostConnection;
            //        break;

            //    case MySqlErrorNumber.LockWaitTimeoutExceeded:
            //        returnCode = CommonData.DbReturnCode.LockWaitTimeoutExceeded;
            //        break;

            //    case MySqlErrorNumber.DeadlockFound:
            //        returnCode = CommonData.DbReturnCode.DeadlockFound;
            //        break;

            //    default:
            //        returnCode = CommonData.DbReturnCode.ExceptionOccured;
            //        break;
            //}

            return returnCode;
        }

        /// <summary>
        /// check Exception in MsSql DBMS
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>
        /// a number: base on MsSql exception number
        /// 0: Delete successful
        /// 1: Access denied, login to database fail(invalid username, invalid password)
        /// 2: Invalid host, cannot find server(host) that set in app config file
        /// 3: Invalid database, cannot find database that set in DbConfig file
        /// 4: Lost connection, cannot connect to database because lost connection
        /// 5: Duplicate key: insert Primary Key or Unique Key that already exist in database
        /// 6: Forgeign key not exist: insert a foreign key that not exist in primary key
        /// 7: Foreign Key Violation: Foreign Key Violation (delete primary key that is foreign key in other table)
        /// 8: Data not found: Delete or Update data that not exist in database
        /// 9: Exception occured: other exception
        /// </returns>
        private int ProcessMsSqlException(Exception ex)
        {
            int returnCode = 0;

            System.Data.SqlClient.SqlException msSqlEx = (System.Data.SqlClient.SqlException)ex.InnerException;

            switch (msSqlEx.Number)
            {
                case MsSqlErrorNumber.AccessDenied:
                    returnCode = CommonData.DbReturnCode.AccessDenied;
                    break;

                case MsSqlErrorNumber.DuplicateKey:
                    returnCode = CommonData.DbReturnCode.DuplicateKey;
                    break;

                case MsSqlErrorNumber.ForgeignKeyViolation:
                    returnCode = CommonData.DbReturnCode.ForeignKeyViolation;
                    break;

                case MsSqlErrorNumber.InvalidDatabase:
                    returnCode = CommonData.DbReturnCode.InvalidDatabase;
                    break;

                case MsSqlErrorNumber.InvalidHost:
                    returnCode = CommonData.DbReturnCode.InvalidHost;
                    break;

                case MsSqlErrorNumber.LostConnection:
                    returnCode = CommonData.DbReturnCode.LostConnection;
                    break;

                case MsSqlErrorNumber.IsCloseConnection:
                    returnCode = CommonData.DbReturnCode.LostConnection;
                    break;

                case MsSqlErrorNumber.TimeOut:
                    returnCode = CommonData.DbReturnCode.LockWaitTimeoutExceeded;
                    break;

                default:
                    returnCode = CommonData.DbReturnCode.ExceptionOccured;
                    break;
            }

            return returnCode;
        }

        #endregion Exception

        #region GetDateTime

        /// <summary>
        /// Lay gio he thong tren may server luu database
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateTimeByEntityQuery()
        {
            try
            {
                //var connect = this.Connection;
                //var command = ((EntityConnection)connect).StoreConnection.CreateCommand();
                //command.CommandType = System.Data.CommandType.Text;

                //switch (DbConfig.ServerType)
                //{
                //    case DbConfig.DbType.MSSQL:
                //        command.CommandText = "SELECT GETDATE()";
                //        break;
                //    case DbConfig.DbType.MySql:
                //        command.CommandText = "SELECT NOW()";
                //        break;
                //    case DbConfig.DbType.Oracle:

                //        break;
                //}
                //if (connect.State != ConnectionState.Open)
                //{
                //if (((EntityConnection)connect).StoreConnection.State != ConnectionState.Open)
                //{
                //    ((EntityConnection)connect).StoreConnection.Open();
                //}
                //}

                //return (DateTime)command.ExecuteScalar();

                var dbDate = this.CreateQuery<DateTime>("CurrentDateTime() ").AsEnumerable().First();

                return dbDate;
            }
            catch
            {
                return DateTime.Now;
            }
        }

        #endregion GetDateTime

        #region Insert & Update infomation

        /// <summary>
        /// functionGroupId : CommonData.FunctionGr
        /// </summary>
        /// <typeparam name="MP"></typeparam>
        /// <param name="objEntity"></param>
        /// <param name="functionGroupId"></param>
        protected void CreateInsertHistory<MP>(MP objEntity, string functionGroupId)
        {
            DateTime dateTime = (this.GetDateTimeByEntityQuery());
            SetProperty<MP>(objEntity, CommonKey.InsertPIC, UserSession.UserCode);
            SetProperty<MP>(objEntity, CommonKey.InsertPG, functionGroupId);
            SetProperty<MP>(objEntity, CommonKey.InsertDate, dateTime);

            SetProperty<MP>(objEntity, CommonKey.UpdatePIC, UserSession.UserCode);
            SetProperty<MP>(objEntity, CommonKey.UpdatePG, functionGroupId);
            SetProperty<MP>(objEntity, CommonKey.UpdateDate, dateTime);
        }

        /// <summary>
        /// functionGroupId : CommonData.FunctionGr
        /// </summary>
        /// <typeparam name="MP"></typeparam>
        /// <param name="objEntity"></param>
        /// <param name="functionGroupId"></param>
        protected void CreateUpdateHistory<MP>(MP objEntity, string functionGroupId)
        {
            DateTime dateTime = (this.GetDateTimeByEntityQuery());
            string value;
            if (GetProperty<MP>(objEntity, "Insert_Date") == null)
            {
                value = CommonMethod.ParseInternalDateTimeFormat(dateTime);
                SetProperty<MP>(objEntity, "Insert_PIC", UserSession.UserCode);
                SetProperty<MP>(objEntity, "Insert_PG", functionGroupId);
            }
            else
            {
                value = GetProperty<MP>(objEntity, "Insert_Date").ToString();
            }

            DateTime temp = DateTime.Parse(value);
            SetProperty(objEntity, "Insert_Date", (temp));

            SetProperty(objEntity, "Update_PIC", UserSession.UserCode);
            SetProperty(objEntity, "Update_PG", functionGroupId);
            SetProperty(objEntity, "Update_Date", (dateTime));
        }

        //private void SetProperty<MP>(MP obj, string name, string value)
        //{
        //    var type = typeof(MP);
        //    var properties = type.GetProperties();

        //    foreach (System.Reflection.PropertyInfo property in properties)
        //    {
        //        if (property.Name == name)
        //        {
        //            property.SetValue(obj, Convert.ChangeType(value, property.PropertyType), null);
        //        }
        //    }
        //}

        private void SetProperty<MP>(MP obj, string name, object value)
        {
            var type = typeof(MP);
            var properties = type.GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                if (property.Name == name)
                {
                    Type propertyType = Type.GetType(property.PropertyType.FullName);
                    object newValue;
                    if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        if (value == null)
                        {
                            newValue = null;
                        }
                        else
                        {
                            newValue = Convert.ChangeType(value, propertyType.GetGenericArguments()[0]);
                        }
                    }
                    else
                    {
                        newValue = Convert.ChangeType(value, property.PropertyType);
                    }

                    property.SetValue(obj, newValue, null);
                }
            }
        }

        private object GetProperty<MP>(MP obj, string name)
        {
            Type type = typeof(MP);

            return type.GetProperty(name).GetValue(obj, null);
        }

        #endregion Insert & Update infomation
    }
}