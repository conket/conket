using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nihongo.Models
{
    public class MS_KanjiExampleModel
    {
        public int KanjiID { get; set; }
        public int VocabularyID { get; set; }
        public string KanjiCode { get; set; }
        public string VocabularyCode { get; set; }
        public string Kanji { get; set; }
        public string Hiragana { get; set; }
        public string VMeaning { get; set; }
        public string Pinyin { get; set; }
    }
}