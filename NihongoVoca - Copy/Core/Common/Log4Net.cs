using System;

namespace Ivs.Core.Common
{
    public static class Log4Net
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// return bug
        /// </summary>
        /// <param name="_bug"></param>
        /// <author>NGUYEN LE QUANG</author>
        public static void getStringError(String _bug, String _methodName, String _className)
        {
            log4net.Config.XmlConfigurator.Configure();
            //log.Warn(_bug);
            //log.Debug(_bug);
            log.Info("Class Name:" + _className + "\t Method Name: " + _methodName);
            log.Error("Content Bug: " + _bug);
            //log.Fatal(_bug);
        }

        public static void getStringError(Exception ce)
        {
            log4net.Config.XmlConfigurator.Configure();
            //log.Warn(_bug);
            //log.Debug(_bug);
            //StringBuilder errString = new StringBuilder();
            //errString.AppendLine();
            //errString.AppendLine();
            //errString.Append(" StackTrace : " + ce.StackTrace);
            //errString.AppendLine();
            //errString.Append("\n TargetSite : " + ce.TargetSite);
            //errString.AppendLine();
            //errString.AppendLine("\n Message: " + ce.Message);
            log.Error(ce);
            //log.Fatal(_bug);
        }
    }
}