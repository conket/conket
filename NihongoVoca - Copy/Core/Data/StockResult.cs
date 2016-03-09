using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivs.Core.Data
{
    public class StockResult
    {
        public string DocumentNumber { get; set; }
        public int ReturnCode { get; set; }

        private List<StockResultDto> _errorList = new List<StockResultDto>();
        public List<StockResultDto> ErrorList
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

    public class StockResultDto
    {
        public int Line { get; set; }
        public int ReturnCode { get; set; }
        public string Period { get; set; }
        public string InvNo { get; set; }
    }
}
