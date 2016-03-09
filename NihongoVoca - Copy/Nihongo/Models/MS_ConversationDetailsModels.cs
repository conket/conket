using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nihongo.Models
{
    public class MS_ConversationDetailsModels
    {
        public int ID { get; set; }
        public string ConversationCode { get; set; }
        public int LineNumber { get; set; }
        public string PersonName { get; set; }
        public string Romaji { get; set; }
        public string Japanese { get; set; }
        public string Vietnamese { get; set; }
        public string English { get; set; }
        public string UrlAudio { get; set; }
        public string Description { get; set; }
        public string LessonCode { get; set; }
    }
}