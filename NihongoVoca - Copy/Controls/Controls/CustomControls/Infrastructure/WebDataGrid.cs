using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Linq.Expressions;
using Ivs.Core.Common;
using System.Collections;
using System.Web.UI;
using System.IO;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using System.ComponentModel;
using Ivs.Core.Interface;
using System.Web;
using Ivs.Controls.CustomControls.Infrastructure.BaseControl;
using System.Reflection;
using Ivs.Core.Data;

namespace Ivs.Controls.CustomControls.Infrastructure
{
    public class WebDataGrid<TModel> : WebBaseGrid<TModel>, IWebControl, IHtmlString//IWebDataGrid<TModel>
    {
        public WebDataGrid()
        {
            base.GridColumns = new List<WebGridColumn>();
        }

        public WebDataGrid(HtmlHelper htmlHelper)
        {
            this.Helper = htmlHelper;
            base.GridColumns = new List<WebGridColumn>();
        }

        public WebDataGrid(HtmlHelper htmlHelper, IEnumerable<TModel> dataSource)
        {
            base.Helper = htmlHelper;
            base.DataSource = dataSource;
            base.GridColumns = new List<WebGridColumn>();
        }

        public string ToHtmlString()
        {
            var defaultDivWrapClass = "grid-wrapper";
            var defaultDivTableClass = "table-responsive";
            var defaultDivPagingClass = "pagination pagination-sm pagination-md";

            //Div wraps the grid
            TagBuilder divWrapBuilder = new TagBuilder("div");
            divWrapBuilder.AddCssClass(defaultDivWrapClass);
            //Div to recognize responsive table
            TagBuilder divTableBuilder = new TagBuilder("div");
            //Add class for Div
            divTableBuilder.AddCssClass(defaultDivTableClass);

            //Add rendered table html to responsive
            divTableBuilder.InnerHtml = RenderControl();

            //Add rendered table html to wrap
            divWrapBuilder.InnerHtml = divTableBuilder.ToString();

            if (this.IsPaging)
            {
                //Div wraps the grid
                TagBuilder divPagingBuilder = new TagBuilder("div");
                divPagingBuilder.AddCssClass(defaultDivPagingClass);
                divPagingBuilder.InnerHtml = RenderPaging();
                return (divWrapBuilder.ToString(TagRenderMode.Normal) + divPagingBuilder.ToString(TagRenderMode.Normal));
            }
            // Return the string
            return (divWrapBuilder.ToString(TagRenderMode.Normal));
        }

        private string RenderPaging()
        {
            var action = this.Helper.ViewContext.RouteData.Values["action"];
            var controller = this.Helper.ViewContext.RouteData.Values["controller"];
            var writer = new HtmlTextWriter(new StringWriter());
            //var model = (Ivs.Core.Interface.IPagedList<TModel>)this.Helper.ViewData.Model;
            var model = (Ivs.Core.Interface.IPagedList<TModel>)this.DataSource;
            if (model != null && model.Count > 0)
            {
                AjaxHelper ajaxPaging = new AjaxHelper(this.Helper.ViewContext, this.Helper.ViewDataContainer);
                var paging = ajaxPaging.Pager(new AjaxOptions
                                   {
                                       UpdateTargetId = "searchResult",
                                       //OnBegin = "beginPaging",
                                       //OnSuccess = "successPaging",
                                       //OnFailure = "failurePaging",
                                       InsertionMode = InsertionMode.Replace,
                                       HttpMethod = "POST",
                                       //OnComplete = "onComplete"

                                   }
                                   , model.PageSize
                                   , model.PageNumber
                                   , model.TotalItemCount
                                   , new
                                    {
                                        controller = controller,
                                        action = action,

                                    });
                writer.Write(paging);
            }
            return writer.InnerWriter.ToString();
        }

        public string RenderControl()
        {
            var defaultControlClass = "table table-bordered table-hover table-condensed";
            TagBuilder builder = new TagBuilder("table");
            builder.AddCssClass(defaultControlClass);
            if (CommonMethod.IsNullOrEmpty(this.Name))
            {
                builder.Attributes.Add("name", "table-result");
                builder.Attributes.Add("id", "table-result");
            }
            else
            {
                builder.Attributes.Add("name", this.Name);
                builder.Attributes.Add("id", this.Name);
            }

            // Render table header
            builder.InnerHtml += RenderHeader();

            // Render table row
            if (this.DataSource != null)
            {
                int rowIndex = 0;
                foreach (var item in this.DataSource)
                {
                    builder.InnerHtml += RenderRow(item, rowIndex);
                    rowIndex++;
                }
            }

            return builder.ToString(TagRenderMode.Normal);

            #region Create HtmlTextWriter
            //var writer = new HtmlTextWriter(new StringWriter());
            //// Open table tag
            //writer.AddAttribute("class", defaultControlClass);
            //if (CommonMethod.IsNullOrEmpty(this.Name))
            //{
            //    writer.AddAttribute("name", "table-result");
            //    writer.AddAttribute("id", "table-result");
            //}
            //else
            //{
            //    writer.AddAttribute("name", this.Name);
            //    writer.AddAttribute("id", this.Name);
            //}
            //writer.RenderBeginTag(HtmlTextWriterTag.Table);

            //// Render table header
            //writer.RenderBeginTag(HtmlTextWriterTag.Thead);
            //RenderHeader(writer);
            //writer.RenderEndTag();

            //// Render table body
            //writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
            //if (this.DataSource != null)
            //{
            //    foreach (var item in this.DataSource)
            //    {
            //        RenderRow(writer, item);
            //    }
            //}
            //writer.RenderEndTag();

            //// Close table tag
            //writer.RenderEndTag();
            //return writer.InnerWriter.ToString();

            #endregion
        }

        private string RenderRow(TModel item, int rowIndex)
        {
            TagBuilder tRowBuilder = new TagBuilder("tr");
            foreach (var column in GridColumns)
            {
                string generatedId = column.Name + "-" + rowIndex;

                #region Write value

                if (column.IsCheckedColumn)
                {
                    TagBuilder tDBuilder = new TagBuilder("td");
                    tDBuilder.Attributes.Add("class", "text-center");

                    TagBuilder checkboxBuilder = new TagBuilder("input");
                    checkboxBuilder.MergeAttribute("id", generatedId);
                    checkboxBuilder.MergeAttribute("name", column.Name);
                    checkboxBuilder.MergeAttribute("type", "checkbox");
                    checkboxBuilder.MergeAttribute("value", "false");
                    //writer.Write(checkboxBuilder.ToString(TagRenderMode.Normal));

                    //writer.RenderEndTag();
                    tDBuilder.InnerHtml += checkboxBuilder.ToString(TagRenderMode.Normal);
                    tRowBuilder.InnerHtml += tDBuilder.ToString(TagRenderMode.Normal);
                }
                else
                {
                    //var property = item.GetType().GetProperty(column.Name);
                    //var propertyType = property.PropertyType;
                    var value = GetDisplayValue(item, column);

                    if (column.IsHidden)
                    {
                        TagBuilder hiddenBuilder = new TagBuilder("input");
                        hiddenBuilder.MergeAttribute("id", generatedId);
                        hiddenBuilder.MergeAttribute("name", column.Name);
                        hiddenBuilder.MergeAttribute("type", "hidden");
                        hiddenBuilder.MergeAttribute("value", this.Helper.Encode(value));
                        if (column.IsPrimaryColumn)
                        {
                            hiddenBuilder.AddCssClass("primary-column");
                        }
                        //writer.Write(hiddenBuilder.ToString(TagRenderMode.Normal));
                        tRowBuilder.InnerHtml += hiddenBuilder.ToString(TagRenderMode.Normal);
                    }
                    else
                    {
                        TagBuilder tDBuilder = new TagBuilder("td");
                        tDBuilder.AddCssClass(column.AlignValueClass);
                        //writer.RenderBeginTag(HtmlTextWriterTag.Td);

                        #region Primary column (ID is almost)

                        if (column.IsPrimaryColumn)
                        {
                            TagBuilder hiddenBuilder = new TagBuilder("input");
                            hiddenBuilder.MergeAttribute("id", generatedId);
                            hiddenBuilder.MergeAttribute("name", column.Name);
                            hiddenBuilder.MergeAttribute("type", "hidden");
                            hiddenBuilder.MergeAttribute("value", this.Helper.Encode(value));
                            hiddenBuilder.AddCssClass("primary-column");

                            tDBuilder.InnerHtml += hiddenBuilder.ToString(TagRenderMode.Normal);
                            //writer.Write(hiddenBuilder.ToString(TagRenderMode.Normal));
                        }

                        #endregion

                        if (column.AllowInput)
                        {
                            if (column.IsDropDownList)
                            {
                                #region Dropdownlist

                                TagBuilder inputBuilder = new TagBuilder("select");
                                inputBuilder.Attributes.Add("id", generatedId);
                                inputBuilder.Attributes.Add("name", column.Name);
                                if (column.MaxLength > 0)
                                {
                                    inputBuilder.Attributes.Add("maxlength", CommonMethod.ParseString(column.MaxLength));
                                }
                                //Add validation attributes
                                foreach (var attr in column.ValidationAttributes)
                                {
                                    inputBuilder.Attributes.Add(attr.Key, CommonMethod.ParseString(attr.Value));
                                }
                                inputBuilder.AddCssClass("combobox");
                                //Created StringBuilder object to store option data fetched oen by one from list.
                                StringBuilder options = new StringBuilder();
                                //Iterated over the IEnumerable list.
                                if (column.DropDownListDataSource != null)
                                {
                                    foreach (var selectListItem in column.DropDownListDataSource)
                                    {
                                        string selectedStr = ((!CommonMethod.IsNullOrEmpty(value) && selectListItem.Value.Equals(value)) || selectListItem.Selected
                                                                ? "selected = 'selected'"
                                                                : CommonData.StringEmpty);
                                        //Each option represents a value in dropdown. For each element in the list, option element is created and appended to the stringBuilder object.
                                        if (CommonMethod.IsNullOrEmpty(selectListItem.Value))
                                        {
                                            options = options.AppendLine("<option value=''>.</option>");
                                        }
                                        else
                                        {
                                            options = options.AppendLine("<option value='" + selectListItem.Value + "' " + selectedStr + ">" + selectListItem.Text + "</option>");
                                        }
                                    }
                                }


                                //assigned all the options to the dropdown using innerHTML property.
                                inputBuilder.InnerHtml = options.ToString();
                                tDBuilder.InnerHtml += inputBuilder.ToString(TagRenderMode.Normal);

                                #endregion
                            }
                            else
                            {
                                #region Input

                                string defaultControlClass = " form-control ";
                                TagBuilder inputBuilder = new TagBuilder("input");
                                inputBuilder.MergeAttribute("id", generatedId);
                                inputBuilder.MergeAttribute("name", column.Name);
                                switch (column.InputType)
                                {
                                    case CommonData.InputWebType.text:
                                        inputBuilder.Attributes.Add("type", "text");
                                        break;
                                    case CommonData.InputWebType.password:
                                        inputBuilder.Attributes.Add("type", "password");
                                        break;
                                    case CommonData.InputWebType.date:
                                        inputBuilder.Attributes.Add("type", "text");
                                        defaultControlClass += " datepicker";
                                        if (UserSession.LangId == CommonData.Language.Japanese)
                                        {
                                            inputBuilder.Attributes.Add("data-date-format", CommonData.DateFormat.Yyyy_MM_dd.ToUpper());
                                        }
                                        else if (UserSession.LangId == CommonData.Language.VietNamese)
                                        {
                                            inputBuilder.Attributes.Add("data-date-format", CommonData.DateFormat.DdMMyyyy.ToUpper());
                                        }
                                        else
                                        {
                                            inputBuilder.Attributes.Add("data-date-format", CommonData.DateFormat.MMddyyyy.ToUpper());
                                        }

                                        break;
                                    case CommonData.InputWebType.datetime:
                                        inputBuilder.Attributes.Add("type", "text");
                                        defaultControlClass += " datetimepicker";
                                        if (UserSession.LangId == CommonData.Language.Japanese)
                                        {
                                            inputBuilder.Attributes.Add("data-date-format", CommonData.DateFormat.Yyyy_MM_ddHHmmss.ToUpper());
                                        }
                                        else if (UserSession.LangId == CommonData.Language.VietNamese)
                                        {
                                            inputBuilder.Attributes.Add("data-date-format", CommonData.DateFormat.DdMMyyyyHHmmss.ToUpper());
                                        }
                                        else
                                        {
                                            inputBuilder.Attributes.Add("data-date-format", CommonData.DateFormat.MMddyyyyHHmmss.ToUpper());
                                        }
                                        break;
                                    case CommonData.InputWebType.time:
                                        inputBuilder.Attributes.Add("type", "text");
                                        defaultControlClass += " timepickerr";
                                        break;
                                    case CommonData.InputWebType.email:
                                        inputBuilder.Attributes.Add("type", "text");
                                        defaultControlClass += " email";
                                        break;
                                    case CommonData.InputWebType.number:
                                        inputBuilder.Attributes.Add("type", "text");
                                        defaultControlClass += " number";
                                        break;
                                    case CommonData.InputWebType.month:
                                        inputBuilder.Attributes.Add("type", "text");
                                        defaultControlClass += " month";
                                        break;
                                    case CommonData.InputWebType.week:
                                        inputBuilder.Attributes.Add("type", "text");
                                        defaultControlClass += " week";
                                        break;
                                    default:
                                        inputBuilder.Attributes.Add("type", "text");
                                        break;
                                }
                                inputBuilder.MergeAttribute("value", this.Helper.Encode(value));
                                if (column.MaxLength > 0)
                                {
                                    inputBuilder.Attributes.Add("maxlength", CommonMethod.ParseString(column.MaxLength));
                                }
                                //Add validation attributes
                                foreach (var attr in column.ValidationAttributes)
                                {
                                    inputBuilder.Attributes.Add(attr.Key, CommonMethod.ParseString(attr.Value));
                                }
                                inputBuilder.AddCssClass(defaultControlClass);
                                inputBuilder.AddCssClass(column.AlignValueClass);

                                tDBuilder.InnerHtml += inputBuilder.ToString(TagRenderMode.Normal);

                                #endregion
                            }
                        }
                        else
                        {
                            tDBuilder.InnerHtml += this.Helper.Encode(value);
                        }

                        //writer.Write(this.Helper.Encode(value));
                        //writer.RenderEndTag();
                        tRowBuilder.InnerHtml += tDBuilder.ToString(TagRenderMode.Normal);
                    }
                }

                #endregion
            }

            return tRowBuilder.ToString(TagRenderMode.Normal);
            #region Html text writer
            //writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            //foreach (var column in GridColumns)
            //{
            //    #region Write value

            //    if (column.IsCheckedColumn)
            //    {
            //        writer.AddAttribute("class", "text-center");
            //        writer.RenderBeginTag(HtmlTextWriterTag.Td);

            //        TagBuilder checkboxBuilder = new TagBuilder("input");
            //        checkboxBuilder.MergeAttribute("name", column.Name);
            //        checkboxBuilder.MergeAttribute("type", "checkbox");
            //        checkboxBuilder.MergeAttribute("value", "false");
            //        writer.Write(checkboxBuilder.ToString(TagRenderMode.Normal));

            //        writer.RenderEndTag();
            //    }
            //    else
            //    {
            //        var property = item.GetType().GetProperty(column.Name);
            //        var value = property.GetValue(item, null) ?? CommonData.StringEmpty;
            //        if (column.IsHidden)
            //        {
            //            TagBuilder hiddenBuilder = new TagBuilder("input");
            //            hiddenBuilder.MergeAttribute("name", column.Name);
            //            hiddenBuilder.MergeAttribute("type", "hidden");
            //            hiddenBuilder.MergeAttribute("value", this.Helper.Encode(value));
            //            if (column.IsPrimaryColumn)
            //            {
            //                hiddenBuilder.AddCssClass("primary-column");
            //            }
            //            writer.Write(hiddenBuilder.ToString(TagRenderMode.Normal));
            //        }
            //        else
            //        {
            //            switch (column.AlignValue)
            //            {
            //                case CommonData.Alignment.Default:
            //                    writer.AddAttribute("class", "text-left");
            //                    break;
            //                case CommonData.Alignment.Near:
            //                    writer.AddAttribute("class", "text-left");
            //                    break;
            //                case CommonData.Alignment.Center:
            //                    writer.AddAttribute("class", "text-center");
            //                    break;
            //                case CommonData.Alignment.Far:
            //                    writer.AddAttribute("class", "text-right");
            //                    break;
            //                default:
            //                    break;
            //            }
            //            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            //            if (column.IsPrimaryColumn)
            //            {
            //                TagBuilder hiddenBuilder = new TagBuilder("input");
            //                hiddenBuilder.MergeAttribute("name", column.Name);
            //                hiddenBuilder.MergeAttribute("type", "hidden");
            //                hiddenBuilder.MergeAttribute("value", this.Helper.Encode(value));
            //                hiddenBuilder.AddCssClass("primary-column");

            //                writer.Write(hiddenBuilder.ToString(TagRenderMode.Normal));
            //            }
            //            writer.Write(this.Helper.Encode(value));
            //            writer.RenderEndTag();
            //        }
            //    }
                    
            //    #endregion

            //}
            //writer.RenderEndTag();
            #endregion
        }

        private object GetDisplayValue(TModel item, WebGridColumn column)
        {
            var property = item.GetType().GetProperty(column.Name);
            var value = property.GetValue(item, null) ?? CommonData.StringEmpty;
            var displayValue = value;
            if (!CommonMethod.IsNullOrEmpty(value))
            {
                if (property.PropertyType == typeof(string))
                {
                    displayValue = value;
                }
                else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                {
                    if (column.InputType == CommonData.InputWebType.date)
                    {
                        if (UserSession.LangId == CommonData.Language.Japanese)
                        {
                            displayValue = CommonMethod.ParseDateTime(value).ToString(CommonData.DateFormat.Yyyy_MM_dd);
                        }
                        else if (UserSession.LangId == CommonData.Language.VietNamese)
                        {
                            displayValue = CommonMethod.ParseDateTime(value).ToString(CommonData.DateFormat.DdMMyyyy);
                        }
                        else
                        {
                            displayValue = CommonMethod.ParseDateTime(value).ToString(CommonData.DateFormat.MMddyyyy);
                        }
                    }
                    else if (column.InputType == CommonData.InputWebType.datetime)
                    {
                        if (UserSession.LangId == CommonData.Language.Japanese)
                        {
                            displayValue = CommonMethod.ParseDateTime(value).ToString(CommonData.DateFormat.Yyyy_MM_ddHHmmss);
                        }
                        else if (UserSession.LangId == CommonData.Language.VietNamese)
                        {
                            displayValue = CommonMethod.ParseDateTime(value).ToString(CommonData.DateFormat.DdMMyyyyHHmmss);
                        }
                        else
                        {
                            displayValue = CommonMethod.ParseDateTime(value).ToString(CommonData.DateFormat.MMddyyyyHHmmss);
                        }
                    }
                    else if (column.InputType == CommonData.InputWebType.time)
                    {
                        //Not finished yet
                    }
                }
                else
                {
                    if (CommonMethod.IsNullOrEmpty(column.FormatString))
                    {
                        displayValue = CommonMethod.ParseDecimal(value).ToString(CommonData.NumberFormat.N0);
                    }
                    else
                    {
                        displayValue = CommonMethod.ParseDecimal(value).ToString(column.FormatString);
                    }
                }
            }
            return displayValue;
        }

        private string RenderHeader()
        {
            TagBuilder tHeadBuilder = new TagBuilder("thead");
            
            foreach (var column in this.GridColumns)
            {
                if (!column.IsHidden)
                {
                    TagBuilder tHBuilder = new TagBuilder("th");
                    var action = this.Helper.ViewContext.RouteData.Values["action"];
                    var controller = this.Helper.ViewContext.RouteData.Values["controller"];
                    if (column.IsCheckedColumn)
                    {
                        tHBuilder.InnerHtml = this.Helper.Encode(column.HeaderText);
                        //writer.Write(this.Helper.Encode(column.HeaderText));
                    }
                    else
                    {
                        if (!this.AllowSort)
                        {
                            //writer.Write(this.Helper.Encode(column.HeaderText));
                            tHBuilder.InnerHtml = this.Helper.Encode(column.HeaderText);
                        }
                        else
                        {
                            if (!column.Sortable)
                            {
                                //writer.Write(this.Helper.Encode(column.HeaderText));
                                tHBuilder.InnerHtml = this.Helper.Encode(column.HeaderText);
                            }
                            else
                            {
                                if (CommonMethod.IsNullOrEmpty(action) || CommonMethod.IsNullOrEmpty(controller))
                                {
                                    //writer.Write(this.Helper.Encode(column.HeaderText));
                                    tHBuilder.InnerHtml = this.Helper.Encode(column.HeaderText);
                                }
                                else
                                {
                                    if (base.DataSource == null || base.DataSource.Count() == 0)
                                    {
                                        tHBuilder.InnerHtml = this.Helper.Encode(column.HeaderText);
                                    }
                                    else
                                    {
                                        #region Create ajax action link

                                        AjaxHelper linkHelper = new AjaxHelper(this.Helper.ViewContext, this.Helper.ViewDataContainer);
                                        var link = linkHelper.ActionLink(column.HeaderText
                                                        , action.ToString()
                                                        , controller.ToString()
                                                        , new RouteValueDictionary
                                                {
                                                    { 
                                                         "sortColumn", column.Name
                                                    },
                                                    { 
                                                        "sortOrder", this.Helper.ViewBag.SortColumn == column.Name
                                                                    ? (this.Helper.ViewBag.SortOrder == "asc" ? "desc" : "asc") 
                                                                    : "asc" 
                                                    }
                                                }
                                                        , new AjaxOptions
                                                        {
                                                            UpdateTargetId = "searchResult",
                                                            InsertionMode = InsertionMode.Replace,
                                                            HttpMethod = "POST",
                                                            //OnBegin = "onBegin",
                                                            //OnComplete = "onComplete",
                                                            //OnFailure = "onComplete"
                                                        });

                                        #endregion

                                        tHBuilder.InnerHtml = link.ToHtmlString();
                                        //writer.Write(link);
                                    }
                                }
                            }
                        }
                    }

                    //Add th to thead
                    tHeadBuilder.InnerHtml += tHBuilder;
                }
            }

            return tHeadBuilder.ToString(TagRenderMode.Normal);
            #region HtmlTextWriter

            //writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            //foreach (var column in this.GridColumns)
            //{
            //    if (!column.IsHidden)
            //    {
            //        writer.RenderBeginTag(HtmlTextWriterTag.Th);
            //        var action = this.Helper.ViewContext.RouteData.Values["action"];
            //        var controller = this.Helper.ViewContext.RouteData.Values["controller"];
            //        if (column.IsCheckedColumn)
            //        {
            //            writer.Write(this.Helper.Encode(column.HeaderText));
            //        }
            //        else
            //        {
            //            if (!this.AllowSort)
            //            {
            //                writer.Write(this.Helper.Encode(column.HeaderText));
            //            }
            //            else
            //            {
            //                if (!column.AllowSort)
            //                {
            //                    writer.Write(this.Helper.Encode(column.HeaderText));
            //                }
            //                else
            //                {
            //                    if (CommonMethod.IsNullOrEmpty(action) || CommonMethod.IsNullOrEmpty(controller))
            //                    {
            //                        writer.Write(this.Helper.Encode(column.HeaderText));
            //                    }
            //                    else
            //                    {
            //                        #region Create ajax action link

            //                        AjaxHelper linkHelper = new AjaxHelper(this.Helper.ViewContext, this.Helper.ViewDataContainer);
            //                        var link = linkHelper.ActionLink(column.HeaderText
            //                                        , action.ToString()
            //                                        , controller.ToString()
            //                                        , new RouteValueDictionary
            //                            {
            //                                { 
            //                                     "sortColumn", column.Name
            //                                },
            //                                { 
            //                                    "sortOrder", this.Helper.ViewBag.SortColumn == column.Name
            //                                                ? (this.Helper.ViewBag.SortOrder == "asc" ? "desc" : "asc") 
            //                                                : "asc" 
            //                                }
            //                            }
            //                                        , new AjaxOptions
            //                                        {
            //                                            UpdateTargetId = "searchResult",
            //                                            InsertionMode = InsertionMode.Replace,
            //                                            HttpMethod = "POST",
            //                                            //OnBegin = "onBegin",
            //                                            //OnComplete = "onComplete",
            //                                            //OnFailure = "onComplete"
            //                                        });

            //                        #endregion

            //                        writer.Write(link);
            //                    }
            //                }
            //            }
            //        }

            //        writer.RenderEndTag();
            //    }
            //}
            //writer.RenderEndTag();
            #endregion
        }

    }

    
}
