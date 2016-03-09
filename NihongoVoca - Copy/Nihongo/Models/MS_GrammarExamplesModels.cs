using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nihongo.Models
{
    public class MS_GrammarExamplesModels
    {
        public int ID { get; set; }
        public string GrammarCode { get; set; }
        public int? LineNumber { get; set; }
        public string Romaji { get; set; }
        public string Hiragana { get; set; }
        public string Katakana { get; set; }
        public string Kanji { get; set; }
        public string VMeaning { get; set; }
        public string EMeaning { get; set; }
        public string UrlAudio { get; set; }
    }
}