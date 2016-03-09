using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ivs.Core.Common;

namespace Ivs.Core.Data
{
    public class IvMonthlyProcessResult
    {
        public int ReturnCode { get; set; }

        private List<IvMonthlyProcessResultDto> _errorList = new List<IvMonthlyProcessResultDto>();
        public List<IvMonthlyProcessResultDto> ErrorList
        {
            get
            {
                return _errorList;
            }
            set
            {
                _errorList = value;
            }
        }
    }

    public class IvMonthlyProcessResultDto
    {
        public int Line { get; set; }
        public string DocumentNumber { get; set; }
        public string Period1 { get; set; }
        public string Period2 { get; set; }
        public string Period { get; set; }
        public string ItemCode { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseCode2 { get; set; }
        public string QualityStatus { get; set; }
        public string QualityStatusDisplay 
        {
            get
            {
                string result = CommonData.StringEmpty;
                switch (QualityStatus)
                {
                    case CommonData.QualityStatus.NC:
                        result = "NC";
                        break;
                    case CommonData.QualityStatus.NG:
                        result = "NG";
                        break;
                    case CommonData.QualityStatus.OK:
                        result = "OK";
                        break;
                    case CommonData.QualityStatus.QI:
                        result = "QI";
                        break;
                    default:
                        break;
                }

                return result;
            }
            set { ;} 
        }
        public string ProductionLine { get; set; }
        public int ReturnCode { get; set; }
    }
}
