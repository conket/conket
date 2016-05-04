using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ivs.Core.Common;

namespace Nihongo.Models
{
    public class MS_TestResultModels
    {
        public int No { get; set; }
        public int ID { get; set; }
        public string Code { get; set; }
        public int CategoryID { get; set; }
        public int UserID { get; set; }
        public string CategoryCode { get; set; }
        public string UserName { get; set; }
        public string UserUrlImage { get; set; }
        public string UserDisplayName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string IsPass { get; set; }
        public int? CompletedTime { get; set; }
        public string Status { get; set; }
        public int NumOfCorrectVocas { get; set; }
        public int NumOfVocas { get; set; }
        public string Timeline 
        {
            get
            {
                int days = (DateTime.Now - CreateDate.Value).Days;
                int hours = (DateTime.Now - CreateDate.Value).Hours;
                int minutes = (DateTime.Now - CreateDate.Value).Minutes;
                if (hours == 0)
                {
                    return "cách đây " + minutes + " phút";
                }
                if (days == 0)
                {
                    return "cách đây " + hours + " giờ";
                }
                else if (days < 30)
                {
                    return "cách đây " + days + " ngày";
                }
                return "cách đây " + (days / 30) + " tháng";
            }
        }
        public string Description { get; set; }
    }
}