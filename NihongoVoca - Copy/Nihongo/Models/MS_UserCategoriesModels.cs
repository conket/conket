using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nihongo.Models
{
    public class MS_UserCategoriesModels
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName1 { get; set; }
        public string CategoryName2 { get; set; }
        public string CategoryName3 { get; set; }
        public string CategoryUrlDisplay { get; set; }
        public string VocaSet { get; set; }
        public int? VocaSetID { get; set; }
        public string VocaSetName1 { get; set; }
        public string VocaSetUrlDisplay { get; set; }
        public decimal? VocaSetFee { get; set; }
        public string CategoryUrlImage { get; set; }
        public int? NumOfVocas { get; set; }
        public int? RequiredTimePerVoca { get; set; }
        public int? TotalRequiredTime { get; set; }
        public string Description { get; set; }
        public string IsTrial { get; set; }
        public string IsKanji { get; set; }
        public string IsIgnore { get; set; }
        public string HasLearnt { get; set; }
        public string HasMarked { get; set; }

        private List<MS_UserVocabulariesModels> _vocas = new List<MS_UserVocabulariesModels>();
        public List<MS_UserVocabulariesModels> Vocabularies 
        { 
            get
            {
                return _vocas; 
            }
            set 
            {
                if (value != null)
                {
                    _vocas = value;
                }
                else
                {
                    _vocas = new List<MS_UserVocabulariesModels>();
                }
            }
        }
    }
}