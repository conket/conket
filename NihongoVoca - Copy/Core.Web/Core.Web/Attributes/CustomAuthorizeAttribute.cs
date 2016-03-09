using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Ivs.Core.Common;
using System.Web;
using Ivs.Core.Data;
using System.Web.Routing;
using System.Net;

namespace Ivs.Core.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            //Check session time out
            bool isSessionTimeout = true;
            if (!CommonMethod.IsNullOrEmpty(HttpContext.Current.Session))
            {
                isSessionTimeout = (CommonMethod.IsNullOrEmpty(UserSession.UserCode)) ? true : false;
            }

            if (isSessionTimeout)
            {
                SetResultForSessionTimeout(ref filterContext);
                return;
            }

            //prevent user access page by entering address bar
            //if (filterContext.HttpContext != null)
            //{
            //    if (filterContext.HttpContext.Request.UrlReferrer == null
            //        //|| filterContext.HttpContext.Request.UrlReferrer.Host != "mysite.com"
            //        )
            //    {
            //        string action = filterContext.ActionDescriptor.ActionName;
            //        if (!action.Equals("Index", StringComparison.OrdinalIgnoreCase))
            //        {
            //            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "InvalidRequest" } });
            //            return;
            //        }
            //    }
            //}

            //base.OnAuthorization(filterContext);
        }

        public void SetResultForAuthorization(ref AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        Error = "Session timeout",
                        RedirectURL = urlHelper.Action("InvalidRequest", "Home"),
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "InvalidRequest" } });
            }
        }

        public void SetResultForSessionTimeout(ref AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        Error = "Session timeout",
                        RedirectURL = urlHelper.Action("Login", "Account"),
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "Login" } });
            }
        }
    }
}
