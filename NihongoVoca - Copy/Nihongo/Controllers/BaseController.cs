using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ivs.Core.Common;
using Nihongo.Attributes;
using Ivs.Core.Web.Attributes;
using Nihongo.Dal.Dao;
using Nihongo.Models;
using Ivs.Core.Data;

namespace Nihongo.Controllers
{
    //[WhitespaceFilter]
    //[Throttle(Name = "TestThrottle", Message = "Bạn phải chờ {n} giây trước khi thực hiện tiếp tục thao tác này.", Seconds = 5)]
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.ReturnUrl = HttpContext.Request.Url.AbsoluteUri;
            // http://localhost:1302/TESTERS/Default6.aspx

            ViewBag.Path = HttpContext.Request.Url.AbsolutePath;
            // /TESTERS/Default6.aspx

            ViewBag.Host = HttpContext.Request.Url.Host;
            // localhost

            ViewBag.PreviousUrl = HttpContext.Request.UrlReferrer;

        }

        //protected override void ExecuteCore()
        //{
        //    base.ExecuteCore();
        //}

        //protected override bool DisableAsyncSupport
        //{
        //    get { return true; }
        //}
    }
}
