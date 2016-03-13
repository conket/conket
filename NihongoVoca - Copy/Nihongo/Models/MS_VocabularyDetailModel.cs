using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nihongo.Models
{
    public class MS_VocabularyDetailModel 
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryCode { get; set; }
        public int? VocabularyID { get; set; }
        public string VocabularyCode { get; set; }
        public string KanjiCode { get; set; }
        public int? KanjiID { get; set; }
        public int LineNumber { get; set; }
    }
}