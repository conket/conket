using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivs.Core.Data
{
    public class ServiceResult
    {
        #region Properties

        public int ReturnCode { get; set; }
        public string ServiceName { get; set; }
        public List<string> ExeptionList { get; set; }

        #endregion

        public ServiceResult() { }

        public ServiceResult(int returnCode, string serviceName)
        {
            this.ReturnCode = returnCode;
            this.ServiceName = serviceName;
        }

        public ServiceResult(int returnCode, string serviceName, List<string> lstExeption)
        {
            this.ReturnCode = returnCode;
            this.ServiceName = serviceName;
            this.ExeptionList = lstExeption;
        }
    }
}
