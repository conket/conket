using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Ivs.Core.Interface;
using Ivs.Core.Common;
using System.ComponentModel;
using Ivs.Controls.CustomControls.Infrastructure.BaseControl;
using System.Web.Mvc;

namespace Ivs.Controls.CustomControls.Infrastructure
{
    public class WebGridColumn : WebBaseColumn//: //IWebControl//, IHtmlString
    {
        
        public WebGridColumn()
            : this(CommonData.StringEmpty)
        {
        }

        public WebGridColumn(string name)
            : this(name, name)
        {
        }

        public WebGridColumn(string name, string headerText)
            : this(name, headerText, false)
        {
        }

        public WebGridColumn(string name, string headerText, bool checkedColumn = false)
        {
            base.Name = name;
            base.HeaderText = headerText;
            base.IsCheckedColumn = checkedColumn;
            base.AlignValue = CommonData.Alignment.Default;
            base.InputType = CommonData.InputWebType.text;
        }

        public WebGridColumn(HtmlHelper helper, string name, string headerText)
            : this(helper, name, headerText, false)
        {
        }

        public WebGridColumn(HtmlHelper helper, string name, string headerText, bool checkedColumn = false)
        {
            base.Helper = helper;
            base.Name = name;
            base.HeaderText = headerText;
            base.IsCheckedColumn = checkedColumn;
            base.AlignValue = CommonData.Alignment.Default;
            base.InputType = CommonData.InputWebType.text;
        }
    }
}
