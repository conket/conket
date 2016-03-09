using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace Ivs.Core.Web.Data
{
    public class EncrypyRoute : Route
    {
        public EncrypyRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {

        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var routeData = base.GetRouteData(httpContext);
            if (routeData != null)
            {
                var controllerName = routeData.Values["controller"].ToString();
                var actionName = routeData.Values["action"].ToString();

                //routeData.Values["controller"] = fix(controllerName);
                //routeData.Values["action"] = fix(actionName);
            }
            return routeData;
        }
    }
}
