using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Ajax;
using Ivs.Core.Common;


namespace Ivs.Core.Paging
{
    public class AjaxPager
    {
        private AjaxHelper ajaxHelper;
        private ViewContext viewContext;
        private readonly int pageSize;
        private readonly int currentPage;
        private readonly int totalItemCount;
        //private readonly string actionName;
        private readonly RouteValueDictionary linkWithoutPageValuesDictionary;
        private readonly AjaxOptions ajaxOptions;

        public AjaxPager(AjaxHelper helper, ViewContext viewContext, int pageSize, int currentPage, int totalItemCount, AjaxOptions options, RouteValueDictionary valueDictionary)
        {
            this.ajaxHelper = helper;
            this.viewContext = viewContext;
            this.pageSize = pageSize;
            this.currentPage = currentPage;
            this.totalItemCount = totalItemCount;
            this.ajaxOptions = options;
            this.linkWithoutPageValuesDictionary = valueDictionary;
        }

        public string RenderHtml()
        {
            int pageCount = (int)Math.Ceiling(this.totalItemCount / (double)this.pageSize);
            int nrOfPagesToDisplay = 10;

            var sb = new StringBuilder();

            //First


            // Previous
            if (this.currentPage > 1)
            {
                sb.Append(GeneratePageLink("<<", 1));
                sb.Append(GeneratePageLink("<", this.currentPage - 1));
            }
            else
            {
                //sb.Append("<span class=\"disabled\"><</span>");
                sb.Append(this.GeneratePageString("<<", "disabled"));
                sb.Append(this.GeneratePageString("<", "disabled"));
            }

            int start = 1;
            int end = pageCount;

            if (pageCount > nrOfPagesToDisplay)
            {
                int middle = (int)Math.Ceiling(nrOfPagesToDisplay / 2d) - 1;
                int below = (this.currentPage - middle);
                int above = (this.currentPage + middle);

                if (below < 4)
                {
                    above = nrOfPagesToDisplay;
                    below = 1;
                }
                else if (above > (pageCount - 4))
                {
                    above = pageCount;
                    below = (pageCount - nrOfPagesToDisplay);
                }

                start = below;
                end = above;
            }

            if (start > 3)
            {
                sb.Append(GeneratePageLink("1", 1));
                sb.Append(GeneratePageLink("2", 2));
                sb.Append("...");
            }
            for (int i = start; i <= end; i++)
            {
                if (i == this.currentPage)
                {
                    //sb.AppendFormat("<span class=\"page_selected\">{0}</span>", i);
                    //sb.Append(GeneratePageCurrentLink(i));
                    sb.Append(GeneratePageString(i.ToString(), "current"));
                }
                else
                {
                    sb.Append(GeneratePageLink(i.ToString(), i));
                }
            }
            if (end < (pageCount - 3))
            {
                sb.Append("...");
                sb.Append(GeneratePageLink((pageCount - 1).ToString(), pageCount - 1));
                sb.Append(GeneratePageLink(pageCount.ToString(), pageCount));
            }

            // Next
            if (this.currentPage < pageCount)
            {
                sb.Append(GeneratePageLink(">", (this.currentPage + 1)));
                sb.Append(GeneratePageLink(">>", pageCount));
            }
            else
            {
                //sb.Append("<span class=\"disabled\">&gt;</span>");
                sb.Append(this.GeneratePageString(">", "disabled"));
                sb.Append(this.GeneratePageString(">>", "disabled"));
            }
            //Last

            return sb.ToString();
        }

        private string GeneratePageLink(string linkText, int pageNumber)
        {
            var pageLinkValueDictionary = new RouteValueDictionary(this.linkWithoutPageValuesDictionary);
            pageLinkValueDictionary.Add("page", pageNumber);
            return ajaxHelper.ActionLink(linkText, pageLinkValueDictionary["action"].ToString(), pageLinkValueDictionary, ajaxOptions).ToString();
        }

        private string GenerateCustomPageLink(string linkText, int pageNumber)
        {
            var pageLinkValueDictionary = new RouteValueDictionary(this.linkWithoutPageValuesDictionary);
            pageLinkValueDictionary.Add("page", pageNumber);
            //string result = CommonMethod.IsNullOrEmpty(htmlClass) ? "<li>" : "<li class='" + htmlClass + "'>";
            return "<li>" + ajaxHelper.ActionLink(linkText, pageLinkValueDictionary["action"].ToString(), pageLinkValueDictionary, ajaxOptions).ToString() + "</li>";
        }

        private string GeneratePageString(string linkText, string htmlClass)
        {
            return "<label class ='" + htmlClass + "'>" + linkText + "</label>";
        }

        private string GenerateCustomPageString(string linkText, string htmlClass)
        {
            return "<li class ='" + htmlClass + "'><span>" + linkText + "</span></li>";
        }

        public string RenderCustomHtml()
        {
            int pageCount = (int)Math.Ceiling(this.totalItemCount / (double)this.pageSize);
            int nrOfPagesToDisplay = 10;

            var sb = new StringBuilder();

            //First
            sb.Append("<ul class='pagination pagination-sm'>");

            // Previous
            if (this.currentPage > 1)
            {
                sb.Append(this.GenerateCustomPageLink("<<", 1));
                sb.Append(this.GenerateCustomPageLink("<", this.currentPage - 1));
            }
            else
            {
                //sb.Append("<span class=\"disabled\"><</span>");
                sb.Append(this.GenerateCustomPageString("<<", "disabled"));
                sb.Append(this.GenerateCustomPageString("<", "disabled"));
            }

            int start = 1;
            int end = pageCount;

            if (pageCount > nrOfPagesToDisplay)
            {
                int middle = (int)Math.Ceiling(nrOfPagesToDisplay / 2d) - 1;
                int below = (this.currentPage - middle);
                int above = (this.currentPage + middle);

                if (below < 4)
                {
                    above = nrOfPagesToDisplay;
                    below = 1;
                }
                else if (above > (pageCount - 4))
                {
                    above = pageCount;
                    below = (pageCount - nrOfPagesToDisplay);
                }

                start = below;
                end = above;
            }

            if (start > 3)
            {
                sb.Append(this.GenerateCustomPageLink("1", 1));
                sb.Append(this.GenerateCustomPageLink("2", 2));
                sb.Append(this.GenerateCustomPageString("...", "disabled"));
                //sb.Append("...");
            }
            for (int i = start; i <= end; i++)
            {
                if (i == this.currentPage)
                {
                    //sb.AppendFormat("<span class=\"page_selected\">{0}</span>", i);
                    //sb.Append(GeneratePageCurrentLink(i));
                    sb.Append(this.GenerateCustomPageString(i.ToString(), "active"));
                }
                else
                {
                    sb.Append(this.GenerateCustomPageLink(i.ToString(), i));
                }
            }
            if (end < (pageCount - 3))
            {
                //sb.Append("...");
                sb.Append(this.GenerateCustomPageString("...", "disabled"));
                sb.Append(this.GenerateCustomPageLink((pageCount - 1).ToString(), pageCount - 1));
                sb.Append(this.GenerateCustomPageLink(pageCount.ToString(), pageCount));
            }

            // Next
            if (this.currentPage < pageCount)
            {
                sb.Append(this.GenerateCustomPageLink(">", (this.currentPage + 1)));
                sb.Append(this.GenerateCustomPageLink(">>", pageCount));
            }
            else
            {
                //sb.Append("<span class=\"disabled\">&gt;</span>");
                sb.Append(this.GenerateCustomPageString(">", "disabled"));
                sb.Append(this.GenerateCustomPageString(">>", "disabled"));
            }
            //Last

            sb.Append("</ul>");
            return sb.ToString();
        }
    }
}
