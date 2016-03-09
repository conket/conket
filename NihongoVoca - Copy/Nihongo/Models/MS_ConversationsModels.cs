using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nihongo.Models
{
    public class MS_ConversationsModels
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string LessonCode { get; set; }
        public int NumOfPerson { get; set; }
        public string UrlImage { get; set; }
        public string UrlAudio { get; set; }
        public string UrlVideo { get; set; }
        public string Description { get; set; }
    }
}