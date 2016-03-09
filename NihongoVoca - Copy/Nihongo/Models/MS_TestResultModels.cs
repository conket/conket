using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ivs.Core.Common;

namespace Nihongo.Models
{
    public class MS_TestResultModels
    {
        public int No { get; set; }
        public int ID { get; set; }
        public string Code { get; set; }
        public int CategoryID { get; set; }
        public string CategoryCode { get; set; }
        public string UserName { get; set; }
        public string UserUrlImage { get; set; }
        public string UserDisplayName { get; set; }
        public DateTime CreateDate { get; set; }
        public string IsPass { get; set; }
        public int? CompletedTime { get; set; }
        public string Status { get; set; }
        public int NumOfCorrectVocas { get; set; }
        public int NumOfVocas { get; set; }
    }
}