using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nihongo.Models
{
    public class MS_GrammarsModels
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string LessonCode { get; set; }
        public string Definition { get; set; }
        public string Use { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }


        public IEnumerable<MS_GrammarExamplesModels> Examples { get; set; }
    }
}