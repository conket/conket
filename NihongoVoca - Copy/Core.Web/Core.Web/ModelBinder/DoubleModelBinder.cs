using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;
using Ivs.Core.Common;

namespace Ivs.Core.Web.ModelBinder
{
    public class DoubleModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (value != null)
            {
                if (bindingContext.ModelType == typeof(double) || bindingContext.ModelType == typeof(Nullable<double>))
                {
                    return CommonMethod.ParseDouble(value.AttemptedValue);
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}