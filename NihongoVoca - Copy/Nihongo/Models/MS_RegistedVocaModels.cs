using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nihongo.Models
{
    public class MS_RegistedVocaModels
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int VocaSetID { get; set; }
        public string VocaSetUrlDisplay { get; set; }
        public string VocaSetName1 { get; set; }
        public string Status { get; set; }
        public decimal? Fee { get; set; }
        public decimal? UsefulLife { get; set; }
        public int? NumOfCategories { get; set; }
        public int? NumOfVocas { get; set; }
        public string Description { get; set; }
        public string IsSequence { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int RemainDays
        {
            get
            {
                if (EndDate.HasValue)
                {
                    return (EndDate.Value - DateTime.Now).Days + 1;
                }
                return 0;
            }
        }

    }
}