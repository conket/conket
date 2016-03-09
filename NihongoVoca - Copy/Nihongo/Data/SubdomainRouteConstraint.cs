using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Nihongo.Data
{
    public class SubdomainRouteConstraint : IRouteConstraint
    {
        private readonly string SubdomainWithDot;

        public SubdomainRouteConstraint(string subdomainWithDot)
        {
            SubdomainWithDot = subdomainWithDot;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var url = httpContext.Request.Headers["HOST"];
            var index = url.IndexOf(".");

            //if (index < 0)
            //{
            //    return false;
            //}
            //This will bi not enough in real web. Because the domain names will end with ".com",".net"
            //so probably there will be a "." in url.So check if the sub is not "yourdomainname" or "www" at runtime.
            var sub = url.Split('.')[0];

            //if (sub == "www" || sub == "yourdomainname" || sub == "mail")
            //{
            //    return false;
            //}

            //Add a custom parameter named "user". Anything you like :)
            //values.Add("user", );
            return true;
        }
    }
}
