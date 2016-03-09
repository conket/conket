using Ivs.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Ivs.Core.Web.Attributes
{
    public class EncryptActionNameAttribute : ActionNameSelectorAttribute
    {
        //public string ActionName { get; set; }
        //public override bool IsValidForRequest(ControllerContext controllerContext,
        //  System.Reflection.MethodInfo methodInfo)
        //{
        //    string actionName = controllerContext.RouteData.Values["action"].ToString();
        //    string controllerName = controllerContext.RouteData.Values["controller"].ToString();
        //    return (actionName == CommonMethod.DecodeUrl(this.ActionName));
        //}

        public string Name { get; set; }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, System.Reflection.MethodInfo methodInfo)
        {
            return (actionName == (CommonMethod.EncodeUrl(this.Name)));
        }
    }
}
