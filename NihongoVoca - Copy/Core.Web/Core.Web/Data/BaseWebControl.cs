using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Ivs.Core.Web.Data
{
    public class BaseWebControl
    {
        public virtual HtmlHelper Helper { get; protected set; }
        public virtual string Name { get; protected set; }
    }
}
