using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ivs.Controls.CustomControls.Web.Extensions
{
    public static partial class HtmlHelperForExtension
    {
        public static HtmlHelper<TModel> HtmlHelperFor<TModel>(this HtmlHelper helper) where TModel : class, new()
        {
            return HtmlHelperFor<TModel>(helper.ViewContext, helper.ViewDataContainer.ViewData, helper.RouteCollection);
        }

        public static HtmlHelper<TModel> HtmlHelperFor<TModel>(this HtmlHelper helper, TModel model)
        {
            return HtmlHelperFor<TModel>(helper.ViewContext, helper.ViewDataContainer.ViewData, helper.RouteCollection, model);
        }

        public static HtmlHelper<TModel> HtmlHelperFor<TModel>(ViewContext viewContext, ViewDataDictionary viewData, RouteCollection routeCollection) where TModel : class, new()
        {
            TModel model = new TModel();
            return HtmlHelperFor<TModel>(viewContext, viewData, routeCollection, model);
        }

        public static HtmlHelper<TModel> HtmlHelperFor<TModel>(ViewContext viewContext, ViewDataDictionary viewData, RouteCollection routeCollection, TModel model)
        {
            var newViewData = new ViewDataDictionary(viewData) { Model = model };
            ViewContext newViewContext = new ViewContext(
                viewContext.Controller.ControllerContext,
                viewContext.View,
                newViewData,
                viewContext.TempData,
                viewContext.Writer);
            var viewDataContainer = new ViewDataContainer(newViewContext.ViewData);
            return new HtmlHelper<TModel>(newViewContext, viewDataContainer, routeCollection);
        }

        private class ViewDataContainer : System.Web.Mvc.IViewDataContainer
        {
            public System.Web.Mvc.ViewDataDictionary ViewData { get; set; }

            public ViewDataContainer(System.Web.Mvc.ViewDataDictionary viewData)
            {
                ViewData = viewData;
            }
        }
    }
}
