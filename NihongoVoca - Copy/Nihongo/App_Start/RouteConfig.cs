using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ivs.Core.Common;
//using Nihongo.Data;

namespace Nihongo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Lesson route
            //routes.MapRoute(
            //    name: "Lesson",
            //    url: "{controller}/{lessonId}/{action}/{id}",
            //    defaults: new { controller = "Lesson", action = "Index", lessonId = UrlParameter.Optional, id = UrlParameter.Optional }
            //);


            //routes.MapRoute(
            //    name: "BangChuCai",
            //    url: "Bang-Chu-Cai/{action}/{id}",
            //    defaults: new { controller = "BangChuCai", action = "Index", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
                name: "Home",
                url: "tu-vung-tieng-nhat/{action}",
                defaults: new { controller = "Home", action = "Index"}
            );

            routes.MapRoute(
                name: "Alphabet",
                url: "bang-chu-cai-tieng-nhat/{action}",
                defaults: new { controller = "Alphabet", action = "Index"}
            );

            routes.MapRoute(
                name: "Payment",
                url: "dang-ky/{action}/{urlDisplay}",
                defaults: new { controller = "Payment", action = "Index", urlDisplay = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Drawing",
                url: "luyen-viet-tu-vung-tieng-nhat/",
                defaults: new { controller = "Library", action = "Drawing" }
            );

            routes.MapRoute(
                name: "Achievement",
                url: "bang-thanh-tich-hoc-tu-vung-tieng-nhat/{action}/{id}",
                defaults: new { controller = "Achievement", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Library",
                url: "thu-vien-tu-vung-tieng-nhat/{action}/{id}/{urlDisplay}",
                defaults: new { controller = "Library", action = "Index", id = UrlParameter.Optional, urlDisplay = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default1",
                url: "{controller}/{action}/{id}/{urlDisplay}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, urlDisplay = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //        "Default", // Route name
            //        "chkien0911/{controller}/{action}/{id}", // URL with parameters
            //        new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //        new { controller = new SubdomainRouteConstraint("chkien0911.") },
            //        new[] { "MyProjectNameSpace.Controllers" }
            //        );

            //routes.Add(new SubdomainRoute());
        }
    }
}