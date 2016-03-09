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
        [Required(ErrorMessage="Nhập [Tên đăng nhập] đi gái!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Nhập [Mật khẩu] đi gái!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Nhập [Nhập lại mật khẩu] đi gái!")]
        [System.Web.Mvc.Compare("Password", ErrorMessage="Phải giống [Mật khẩu] gái ơi!")]
        public string RePassword { get; set; }
        public bool CreateVoca { get; set; }
        public string DisplayName { get; set; }
        public string Status { get; set; }
        public string IsAdmin { get; set; }
        public string SystemData { get; set; }

        [Required(ErrorMessage = "Nhập [Tên đăng nhập] đi gái!")]
        public string lUserName { get; set; }

        [Required(ErrorMessage = "Nhập [Mật khẩu] đi gái!")]
        public string lPassword { get; set; }

        public bool RememberMe { get; set; }

        public string LoginState { get; set; }
        public string LoginStateDisplay 
        {
            get { return LoginState == CommonData.Status.Disable ? "Offline" : "Online"; }
        }

        public DateTime? LastVisitedDate { get; set; }

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
        public string Email { get; set; }
        public string Address { get; set; }
        public string UrlImage { get; set; }
    }
}