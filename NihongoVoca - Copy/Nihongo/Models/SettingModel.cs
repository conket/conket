using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nihongo.Models
{
    public class SettingModel
    {
        public int VocaPerLearn { get; set; }
        public int VocaPerReview { get; set; }
        public bool SoundEffect { get; set; }
    }
}