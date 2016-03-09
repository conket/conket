using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Ivs.Core.Data;
using Ivs.Core.Common;
using Ivs.Controls.CustomControls.Infrastructure;
using System.Linq.Expressions;
using System.Web.Routing;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace Ivs.Controls.CustomControls.Web.Extensions
{
    public static partial class DataGridExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static WebDataGrid<TModel> IvsDataGrid<TModel>(this HtmlHelper helper, IEnumerable<TModel> dataSource)
        {
            return (new WebDataGrid<TModel>(helper, dataSource));
            ////Get datasource
            ////var items = (IEnumerable<T>)(dataSource == null ? helper.ViewData.Model : dataSource);
            //var items = helper.ViewData.Model;
            ////Get column names
            ////if (columns == null)
            ////    columns = typeof(T).GetProperties().Select(p => p.Name).ToArray();

            //HtmlTable table = new HtmlTable();
            


            ////Add class to recognize boostrap
            //var defaultDiv1Class = "grid-wrapper";
            //var defaultDivTableClass = "table-responsive";
            

            ////Div wraps the grid
            //TagBuilder divTableBuilder = new TagBuilder("div");
            ////Add class for Div
            //divTableBuilder.AddCssClass(defaultDivTableClass);
            ////Add rendered table html to div3
            ////divTableBuilder.InnerHtml = RendreGrid(helper, dataSource, columns);

            //// Return the string
            //return MvcHtmlString.Create(divTableBuilder.ToString(TagRenderMode.Normal));
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        //public static MvcHtmlString IvsDataGrid<T>(this HtmlHelper helper)
        //{
        //    return IvsDataGrid<T>(helper, null, null);
        //}

        //public static MvcHtmlString IvsDataGrid<T>(this HtmlHelper helper, IEnumerable<T> dataSource)
        //{
            
        //    return IvsDataGrid<T>(helper, dataSource, null);
        //}
    }
}
