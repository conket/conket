using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nihongo.Models
{
    public class MS_VocaSetsModels
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string UrlDisplay { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string IsKanji { get; set; }
        public int? PriorityLevel { get; set; }
        public decimal? Fee { get; set; }
        public decimal? UsefulLife { get; set; }
        public string UrlImage { get; set; }
        public int? NumOfCategories { get; set; }
        public int? NumOfVocas { get; set; }
        public int? NumOfRegistedPerson { get; set; }
        public int? NumOfFinishedPerson { get; set; }
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

        private List<MS_VocaCategoriesModels> _vocaCate = new List<MS_VocaCategoriesModels>();
        public List<MS_VocaCategoriesModels> VocaCategories
        {
            get { return _vocaCate; }
            set 
            {
                if (value == null)
                {
                    _vocaCate = new List<MS_VocaCategoriesModels>();
                }
                else
                {
                    _vocaCate = value;
                }
            }
        }
    }
}