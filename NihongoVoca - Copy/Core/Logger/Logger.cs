namespace Ivs.Core.Logger
{
    public class Logger
    {
        public static Logger Log = new Logger();

        private log4net.ILog log;

        //log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("App.config"));
        public enum Level
        {
            Error,
            Info,
            Debug,
            Warn,
            Fatal
        }

        public void GetLogger(string objectName)
        {
            if (objectName != null)
            {
                log4net.Config.XmlConfigurator.Configure();
                //log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("App.config"));
                log = log4net.LogManager.GetLogger(objectName);
            }
        }

        public void Write(Level level, string message)
        {
            if (log != null)
            {
                Level _lvl = level;
                switch (_lvl)
                {
                    case Level.Error:
                        if (log.IsErrorEnabled)
                            log.Error(message);
                        break;

                    case Level.Debug:
                        if (log.IsDebugEnabled)
                            log.Debug(message);
                        break;

                    case Level.Fatal:
                        if (log.IsFatalEnabled)
                            log.Fatal(message);
                        break;

                    case Level.Info:
                        if (log.IsInfoEnabled)
                            log.Info(message);
                        break;

                    case Level.Warn:
                        if (log.IsWarnEnabled)
                            log.Warn(message);
                        break;
                }
            }
        }

        public void Write(Level level, System.Exception ex)
        {
            if (log != null)
            {
                Level _lvl = level;
                switch (_lvl)
                {
                    case Level.Error:
                        if (log.IsErrorEnabled)
                            log.Error(ex);
                        break;

                    case Level.Debug:
                        if (log.IsDebugEnabled)
                            log.Debug(ex);
                        break;

                    case Level.Fatal:
                        if (log.IsFatalEnabled)
                            log.Fatal(ex);
                        break;

                    case Level.Info:
                        if (log.IsInfoEnabled)
                            log.Info(ex);
                        break;

                    case Level.Warn:
                        if (log.IsWarnEnabled)
                            log.Warn(ex);
                        break;
                }
            }
        }
    }
}