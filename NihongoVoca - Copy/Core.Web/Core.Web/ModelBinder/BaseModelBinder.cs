using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Ivs.Core.Interface;
using Ivs.Core.Common;
using Ivs.Core.Data;

namespace Ivs.Core.Web.ModelBinder
{
    public class BaseModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            if (modelType.Equals(typeof(IModel)))
            {
                string controllerName = CommonMethod.ParseString(controllerContext.RouteData.Values["controller"]);
                //var modelList = Ivs.Core.Web.Controllers.BaseController.ModelList;
                if (ApplicationState.Contain(controllerName))
                {
                    Type type = ApplicationState.GetValue<Type>(controllerName);
                    if (type != null)
                    {
                        var model = Activator.CreateInstance(type);
                        bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, type);
                        return model;
                    }
                }
            }

            return base.CreateModel(controllerContext, bindingContext, modelType);
        }
    }
}
