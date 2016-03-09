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
    public class DateTimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string language = Thread.CurrentThread.CurrentUICulture.Name;
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (value != null)
            {
                DateTime? date = null;
                //displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);
                // use the format specified in the DisplayFormat attribute to parse the date
                if (this.ParseDate(value.AttemptedValue, language, out date))
                {
                    return date;
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }

        private bool ParseDate(string value, string language, out DateTime? result)
        {
            bool isOK = true;
            result = null;
            if (value != null)
            {
                string[] values = value.Split('/');
                try
                {
                    switch (language)
                    {
                        case CommonData.CultureInfo.English:
                            //value's format: mm/dd/yyyy
                            result = new DateTime(int.Parse(values[2]), int.Parse(values[0]), int.Parse(values[1]));
                            break;
                        case CommonData.CultureInfo.Japanese:
                            //value's format: yyyy-mm-dd
                            values = value.Split('-');
                            result = new DateTime(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
                            break;
                        case CommonData.CultureInfo.VietNamese:
                            //value's format: dd/mm/yyyy
                            result = new DateTime(int.Parse(values[2]), int.Parse(values[1]), int.Parse(values[0]));
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    isOK = false;
                }
            }

            return isOK;
        }
    }
}