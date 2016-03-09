using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ivs.Core.Common;
using System.ComponentModel;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ivs.Controls.CustomControls.Infrastructure.BaseControl
{
    public class WebBaseColumn : WebBaseControl
    {
        public bool IsCheckedColumn { get; protected set; }
        public bool IsPrimaryColumn { get; protected set; }
        public bool IsHidden { get; protected set; }
        public CommonData.Alignment AlignValue { get; protected set; }
        public bool AllowInput { get; protected set; }
        public CommonData.InputWebType InputType { get; protected set; }
        public bool IsDropDownList { get; protected set; }
        public IEnumerable<SelectListItem> DropDownListDataSource { get; protected set; }
        public string HeaderText { get; protected set; }
        
        public string AlignValueClass
        {
            get
            {
                string result = CommonData.StringEmpty;
                switch (this.AlignValue)
                {
                    case CommonData.Alignment.Default:
                        switch (this.InputType)
	                    {
                            case CommonData.InputWebType.text:
                                result = "text-left";
                                break;
                            case CommonData.InputWebType.password:
                                result = "text-left";
                                break;
                            case CommonData.InputWebType.date:
                                result = "text-center";
                                break;
                            case CommonData.InputWebType.datetime:
                                result = "text-center";
                                break;
                            case CommonData.InputWebType.time:
                                result = "text-center";
                                break;
                            case CommonData.InputWebType.email:
                                result = "text-left";
                                break;
                            case CommonData.InputWebType.number:
                                result = "text-right";
                                break;
                            case CommonData.InputWebType.month:
                                result = "text-center";
                                break;
                            case CommonData.InputWebType.week:
                                result = "text-center";
                                break;
                            default:
                                result = "text-left";
                                break;
	                    }
                        break;
                    case CommonData.Alignment.Near:
                        result = "text-left";
                        break;
                    case CommonData.Alignment.Center:
                        result = "text-center";
                        break;
                    case CommonData.Alignment.Far:
                        result = "text-right";
                        break;
                    default:
                        break;
                }

                return result;
            }
        }

        private bool _sortable = true;
        [DefaultValue(true)]
        public bool Sortable
        {
            get
            {
                return _sortable;
            }
            protected set
            {
                _sortable = value;
            }
        }

        public WebBaseColumn SetTitle(string headerText)
        {
            this.HeaderText = headerText;
            return this;
        }

        public WebBaseColumn SetSortable(bool sortable = true)
        {
            this.Sortable = sortable;
            return this;
        }

        public WebBaseColumn SetHidden(bool isHidden = true)
        {
            this.IsHidden = isHidden;
            return this;
        }

        public WebBaseColumn SetPrimary(bool isPrimaryColumn = true)
        {
            this.IsPrimaryColumn = isPrimaryColumn;
            return this;
        }

        public WebBaseColumn SetAlignmentValue(CommonData.Alignment alignment)
        {
            this.AlignValue = alignment;
            return this;
        }

        public WebBaseColumn SetInputType(CommonData.InputWebType inputType = CommonData.InputWebType.text)
        {
            this.InputType = inputType;
            return this;
        }

        public WebBaseColumn SetFormatString(string formatString)
        {
            this.FormatString = formatString;
            return this;
        }

        public WebBaseColumn SetAllowInput(bool allowInput = true, bool isDropDownList = false, IEnumerable<SelectListItem> dropDownListDataSource = null)
        {
            this.AllowInput = allowInput;
            this.IsDropDownList = isDropDownList;
            this.DropDownListDataSource = dropDownListDataSource;
            return this;
        }
    }
}
