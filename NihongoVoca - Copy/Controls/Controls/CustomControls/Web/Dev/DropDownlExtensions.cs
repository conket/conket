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
    public static partial class DropDownlExtensions
    {
        public static ComboBoxSettings settings;

        public static MvcHtmlString IvsDropDownDataList(this HtmlHelper helper, ControlSetting controlSetting)
        {
            settings = new ComboBoxSettings();
            settings.ControlStyle.HorizontalAlign = controlSetting.HAlignment;
            settings.ControlStyle.VerticalAlign = controlSetting.VAlignment;
            settings.Enabled = controlSetting.Enabled;
            settings.Name = controlSetting.Name;
            settings.ReadOnly = controlSetting.ReadOnly;
            settings.TabIndex = controlSetting.TabIndex;
            settings.ToolTip = controlSetting.ToolTip;
            settings.Width = controlSetting.Width;

            if (controlSetting.MaxLength > 0)
            {
                settings.Properties.MaxLength = controlSetting.MaxLength;
            }
            switch (controlSetting.DropDownStyle)
            {
                case Ivs.Controls.CustomControls.Infrastructure.DropDownStyle.DropDown:
                    settings.Properties.DropDownStyle =  DevExpress.Web.ASPxEditors.DropDownStyle.DropDown;
                    break;
                case Ivs.Controls.CustomControls.Infrastructure.DropDownStyle.DropDownList:
                    settings.Properties.DropDownStyle =  DevExpress.Web.ASPxEditors.DropDownStyle.DropDownList;
                    break;
                default:
                    settings.Properties.DropDownStyle = DevExpress.Web.ASPxEditors.DropDownStyle.DropDown;
                    break;
            }
            
            settings.Properties.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            settings.Properties.Columns.Add(controlSetting.ValueMember);
            settings.Properties.Columns.Add(controlSetting.DisplayMember);
            //settings.Properties.Columns[0].Caption = CommonData.StringEmpty;
            //settings.Properties.Columns[1].Caption = CommonData.StringEmpty;
            //settings.Properties.DataSource = controlSetting.DataSource;
            settings.Properties.ValueField = controlSetting.ValueMember;
            //settings.Properties.DataMember = controlSetting.DisplayMember;
            settings.Properties.TextField = controlSetting.DisplayMember;
            settings.Properties.ValueType = controlSetting.ValueMember.GetType();

            settings.Properties.EnableClientSideAPI = true;

            //generate regular input without table
            //settings.Properties.Native = true;

            #region Set selected value if any

            // if the valueToBindTo was found, bind it as the selected value
            //if (bindObject != null)
            //    comboBox.Bind(bindObject);

            //if (!CommonMethod.IsNullOrEmpty(controlSetting.SelectedValue))
            //{
            //    settings.PreRender = (sender, e) =>
            //    {
            //        MVCxComboBox cbo = sender as MVCxComboBox;
            //        cbo.Value = controlSetting.SelectedValue;
            //    };
            //}

            //if (controlSetting.SelectedIndex >= 0)
            //{
            //    settings.PreRender = (sender, e) =>
            //    {
            //        MVCxComboBox cbo = sender as MVCxComboBox;
            //        cbo.SelectedIndex = controlSetting.SelectedIndex;
            //    };
            //}

            #endregion

            if (CommonMethod.IsNullOrEmpty(controlSetting.Value))
            {
                new ComboBoxExtension(settings, helper.ViewContext).BindList(controlSetting.DataSource).Render();
            }
            else
            {
                new ComboBoxExtension(settings, helper.ViewContext).BindList(controlSetting.DataSource).Bind(controlSetting.Value).Render();
            }
            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString IvsDropDownDataListFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, ControlSetting controlSetting)
        {
            //Get name
            var name = ExpressionHelper.GetExpressionText(expression);

            //Get value from metadata
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            //Change setting's properties by model
            controlSetting.Name = name;
            controlSetting.Value = metadata.Model;
            //controlSetting.SelectedValue = (string)metadata.Model;

            return IvsDropDownDataList(helper, controlSetting);
        }
    }
}
