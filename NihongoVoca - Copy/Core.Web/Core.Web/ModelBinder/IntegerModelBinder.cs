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
    public class IntegerModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (value != null)
            {
                if (bindingContext.ModelType == typeof(int) || bindingContext.ModelType == typeof(Nullable<int>))
                {
                    return CommonMethod.ParseInt32(CommonMethod.ParseDecimal(value.AttemptedValue));
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}