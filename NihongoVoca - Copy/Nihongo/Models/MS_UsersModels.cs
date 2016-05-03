using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Ivs.Core.Common;

namespace Nihongo.Models
{
    public class MS_UsersModels
    {
        public int No { get; set; }
        public int ID { get; set; }
        public int VocaPerLearn { get; set; }
        public int VocaPerReview { get; set; }
        public string SoundEffect { get; set; }

        [Required(ErrorMessage = "Nhập [Email] đi bạn!")]
        [StringLength(100)]
        public string Email { get; set; }

        //[Required(ErrorMessage="Nhập [Tên đăng nhập] đi bạn!")]
        public string UserName { get; set; }

        [StringLength(50, ErrorMessage = "[Mật khẩu] phải có ít nhất {2} kí tự.", MinimumLength = 6)]
        [Required(ErrorMessage = "Nhập [Mật khẩu] đi bạn!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Nhập [Nhập lại mật khẩu] đi bạn!")]
        [StringLength(50, ErrorMessage = "[Nhập lại mật khẩu] phải có ít nhất {2} kí tự.", MinimumLength = 6)]
        [System.Web.Mvc.Compare("Password", ErrorMessage="Phải giống [Mật khẩu] bạn ơi!")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }

        public bool CreateVoca { get; set; }

        [StringLength(50)]
        public string DisplayName { get; set; }
        public string Status { get; set; }
        public string IsAdmin { get; set; }
        public string SystemData { get; set; }

        //[Required(ErrorMessage = "Nhập [Tên đăng nhập] đi bạn!")]
        public string lUserName { get; set; }

        //[Required(ErrorMessage = "Nhập [Mật khẩu] đi bạn!")]
        public string lPassword { get; set; }

        public bool RememberMe { get; set; }

        public string LoginState { get; set; }
        public string LoginStateDisplay 
        {
            get { return LoginState == CommonData.Status.Disable ? "Offline" : "Online"; }
        }

        public DateTime? LastVisitedDate { get; set; }
        public int NumOfLearntVoca { get; set; }
        public string NumOfLearntVocaDisplay
        {
            get
            {
                return NumOfLearntVoca.ToString("N0");
            }
        }

        public int Point { get; set; }
        public string PointDisplay
        {
            get
            {
                return Point.ToString("N0");
            }
        }
        public int AccumulatedPoint { get; set; }
        public string AccumulatedPointDisplay
        {
            get
            {
                return AccumulatedPoint.ToString("N0");
            }
        }
        public int TotalHasLearnt { get; set; }
        public string TotalHasLearntDisplay
        {
            get
            {
                return TotalHasLearnt.ToString("N0");
            }
        }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string UrlImage { get; set; }
        public int Followers { get; set; }
        public int Followings { get; set; }
        public bool Followed { get; set; }
        private List<MS_UserVocaSet> _userVocaSets = new List<MS_UserVocaSet>();
        public List<MS_UserVocaSet> UserVocaSets 
        {
            get
            {
                return _userVocaSets;
            }
            set
            {
                _userVocaSets = value;
            }
        }

        private List<MS_UsersModels> _users = new List<MS_UsersModels>();
        public List<MS_UsersModels> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
            }
        }
    }
}