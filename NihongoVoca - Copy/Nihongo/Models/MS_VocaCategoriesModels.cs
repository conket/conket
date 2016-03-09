using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nihongo.Models
{
    public class MS_VocaCategoriesModels
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string UrlDisplay { get; set; }
        public string VocaSet { get; set; }
        public int VocaSetID { get; set; }
        public string VocaSetName1 { get; set; }
        public string VocaSetUrlDisplay { get; set; }
        public decimal? VocaSetFee { get; set; }
        public string UrlImage { get; set; }
        public int? NumOfVocas { get; set; }
        public int? RequiredTimePerVoca { get; set; }
        public int? TotalRequiredTime { get; set; }
        public string Description { get; set; }
        public string IsTrial { get; set; }
        public string IsKanji { get; set; }
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