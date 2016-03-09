using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.WebPages;

namespace Ivs.Core.Web.Common
{
    public static class ExtensionMethods
    {
        public static HelperResult RenderSection(this WebPageBase webPage, string name, Func<dynamic, HelperResult> defaultContents)
        {
            if (webPage.IsSectionDefined(name))
            {
                return webPage.RenderSection(name);
            }
            return defaultContents(null);
        }
    }
}
