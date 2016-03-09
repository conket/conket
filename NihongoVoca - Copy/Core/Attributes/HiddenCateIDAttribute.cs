using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Ivs.Core.Common;
using Ivs.Core.Data;

namespace Ivs.Core.Attributes
{
    public class HiddenCateIDAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var urlDisplay = CommonMethod.ParseString(filterContext.RouteData.Values["urlDisplay"]);
            if (!CommonMethod.IsNullOrEmpty(urlDisplay))
            {
                int id = 0;
                DataSession.VocaCates.TryGetValue(urlDisplay, out id);
                filterContext.ActionParameters["id"] = id;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
