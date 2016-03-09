namespace Nihongo.Dal.Data
{
    public static class DbConfig
    {
        public static string Username
        {
            get
            {
                return "ivs";
                //return "sa";
                //return System.Configuration.ConfigurationManager.AppSettings["Username"];
            }
        }

        public static DbType ServerType
        {
            get
            {
                //return DbType.MySql;
                return DbType.MSSQL;
            }
        }

        public static string PassWord
        {
            get
            {
                return "ivs";
                //return System.Configuration.ConfigurationManager.AppSettings["PassWord"];
            }
        }

        public static string ServerName
        {
            get
            {
                //return System.Configuration.ConfigurationManager.AppSettings["ServerName"];
                return @".\";
            }
        }

        public static string Mode
        {
            get
            {
                //0: local
                //1: publish
                return System.Configuration.ConfigurationManager.AppSettings["Mode"];
            }
        }

        public static string WebServicePort
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["WebServicePort"];
            }
        }

        public static string DbName
        {
            get
            {
                return "nihongo_voca";
                //return "ivs_wms_dev";
                //return "ivs_wms_test";
                //return System.Configuration.ConfigurationManager.AppSettings["DbName"];
            }
        }

        public static string MetadataModel
        {
            get
            {
                string metadataModel = string.Empty;
                if (ServerType == DbType.MSSQL)
                {
                    metadataModel = "Dal.Mapping.EFModels";
                }
                else if (ServerType == DbType.MySql)
                {
                    metadataModel = "EFModels.EFModelsMySQL";
                }
                return metadataModel;
                //return "EFModelsMSSQL";
                //return System.Configuration.ConfigurationManager.AppSettings["MetadataModel"];
            }
        }

        public enum DbType
        {
            MySql,
            Oracle,
            MSSQL
        }

        //===================NDNHAT-TimeSheetBatch=============
        public static string TimeSheetLogPath
        {
            get
            {
                string logPath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
                if (!System.IO.Directory.Exists(logPath))
                {
                    System.IO.Directory.CreateDirectory(logPath);
                }
                return logPath;
            }
        }

        //=========================END=========================

        //=============StartUpdate 14/05/2013(Kien)=====================
        public static string TempFolder
        {
            get
            {
                string tempFolder = System.Configuration.ConfigurationManager.AppSettings["TempFolder"];
                if (!System.IO.Directory.Exists(tempFolder))
                {
                    System.IO.Directory.CreateDirectory(tempFolder);
                }
                return tempFolder;
                //return System.AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static string UsingProxy
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["UsingProxy"];
            }
        }

        //==============================================================
    }
}