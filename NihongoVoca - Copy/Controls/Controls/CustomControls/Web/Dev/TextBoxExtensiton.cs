using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DevExpress.Web.Mvc.UI;
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxEditors;
using Ivs.Core.Data;
using Ivs.Core.Common;
using Ivs.Controls.CustomControls.Infrastructure;
using System.Linq.Expressions;

namespace Ivs.Controls.CustomControls.Web.Dev
{
    public static partial class TextBoxExtensiton
    {
        private static TextBoxSettings settings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MvcHtmlString IvsTextBox(this HtmlHelper helper, string name, object value)
        {
            settings = new TextBoxSettings();
            settings.ControlStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            settings.ControlStyle.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle;
            settings.Enabled = true;
            settings.Name = name;
            settings.ReadOnly = false;
            settings.Width = Data.DefaultWidth;
            settings.Properties.EnableClientSideAPI = true;
            //settings.ControlStyle.CssClass = "test1";
            //settings.Properties.CssPostfix = "xxx1";

            //generate regular input without table
            //settings.Properties.Native = true;

            new TextBoxExtension(settings, helper.ViewContext).Bind(value).Render();
            return MvcHtmlString.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MvcHtmlString IvsTextBox(this HtmlHelper helper, string name)
        {
            return IvsTextBox(helper, name, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="controlSetting"></param>
        /// <returns></returns>
        public static MvcHtmlString IvsTextBox(this HtmlHelper helper, ControlSetting controlSetting)
        {
            settings = new TextBoxSettings();
            settings.ControlStyle.HorizontalAlign = controlSetting.HAlignment;
            settings.ControlStyle.VerticalAlign = controlSetting.VAlignment;
            settings.Enabled = controlSetting.Enabled;
            settings.Name = controlSetting.Name;
            settings.Properties.DisplayFormatString = controlSetting.FormatString;
            if (controlSetting.MaxLength > 0)
            {
                settings.Properties.MaxLength = controlSetting.MaxLength;
            }
            settings.Properties.NullText = controlSetting.NullText;
            settings.Properties.Password = controlSetting.Password;
            settings.Properties.EnableClientSideAPI = true;
            //generate regular input without table
            //settings.Properties.Native = true;

            settings.ReadOnly = controlSetting.ReadOnly;
            settings.TabIndex = controlSetting.TabIndex;
            settings.ToolTip = controlSetting.ToolTip;
            settings.Width = controlSetting.Width;

            new TextBoxExtension(settings, helper.ViewContext).Bind(controlSetting.Value).Render();
            return MvcHtmlString.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="controlSetting"></param>
        /// <returns></returns>
        public static MvcHtmlString IvsTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, ControlSetting controlSetting)
        {
            //Get name
            var name = ExpressionHelper.GetExpressionText(expression);

            //Get value from metadata
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            //Change setting's properties by model
            controlSetting.Name = name;
            controlSetting.Value = metadata.Model;
            //controlSetting.SelectedValue = (string)metadata.Model;

            return IvsTextBox(helper, controlSetting);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString IvsTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            //Get name
            var name = ExpressionHelper.GetExpressionText(expression);

            //Get value from metadata
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            return IvsTextBox(helper, name, metadata.Model);
        }

    }
}
