using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivs.Core.Data
{
    public class InvoiceResult
    {
        public int ID { get; set; }
        public string InvNo { get; set; }
        public int ReturnCode { get; set; }

        private List<InvoiceResultDto> _errorList = new List<InvoiceResultDto>();
        public List<InvoiceResultDto> ErrorList
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

    public class InvoiceResultDto
    {
        public int Line { get; set; }
        public string DocumentNumber { get; set; }
        public string ItemCode { get; set; }
        public int ReturnCode { get; set; }
    }
}
