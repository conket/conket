namespace Ivs.Core.Data
{
    public class MySqlErrorNumber
    {
        public const int InvalidHost = 2005;
        public const int InvalidDatabase = 1049;
        public const int AccessDenied = 1045;
        public const int LostConnection = 1042;
        public const int DuplicateKey = 1062;
        public const int ForgeignKeyNotExist = 1452;
        public const int ForgeignKeyViolation = 1451;
        public const int DeadlockFound = 1213;
        public const int LockWaitTimeoutExceeded = 1205;
    }
}