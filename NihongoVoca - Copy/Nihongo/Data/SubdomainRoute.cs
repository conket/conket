using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace Nihongo.Data
{
    public class SubdomainRoute : RouteBase
    {

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext.Request == null || httpContext.Request.Url == null)
            {
                return null;
            }

            var host = httpContext.Request.Url.Host;
            var index = host.IndexOf(".");
            string[] segments = httpContext.Request.Url.PathAndQuery.TrimStart('/').Split('/');

            //if (index < 0)
            //{
            //    return null;
            //}

            //var subdomain = host.Substring(0, index);
            //string[] blacklist = { "www", "yourdomain", "mail" };

            //if (blacklist.Contains(subdomain))
            //{
            //    return null;
            //}

            string controller = (segments.Length > 0 && segments[0] != "") ? segments[0] : "Home";
            string action = (segments.Length > 1) ? segments[1] : "Index";

            var routeData = new RouteData(this, new MvcRouteHandler());
            routeData.Values.Add("controller", controller); //Goes to the relevant Controller  class
            routeData.Values.Add("action", action); //Goes to the relevant action method on the specified Controller
            //routeData.Values.Add("subdomain", "chkien0911"); //pass subdomain as argument to action method
            return routeData;

            //if (index < 0)
            //    return null;

            //var subDomain = url.Substring(0, index);

            //if (subDomain == "user1")
            //{
            //    var routeData = new RouteData(this, new MvcRouteHandler());
            //    routeData.Values.Add("controller", "User1"); //Goes to the User1Controller class
            //    routeData.Values.Add("action", "Index"); //Goes to the Index action on the User1Controller

            //    return routeData;
            //}

            //if (subDomain == "user2")
            //{
            //    var routeData = new RouteData(this, new MvcRouteHandler());
            //    routeData.Values.Add("controller", "User2"); //Goes to the User2Controller class
            //    routeData.Values.Add("action", "Index"); //Goes to the Index action on the User2Controller

            //    return routeData;
            //}


            //return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            //Implement your formating Url formating here
            return null;
        }
    }
}