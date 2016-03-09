using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Ivs.Core.Common;
using System.Security.Cryptography;
using System.IO;
using System.Web.Mvc.Ajax;
using System.Web;

namespace Ivs.Core.Web.Common
{
    public static class HtmlHelpers
    {

        public static MvcHtmlString CustomActionLink(this HtmlHelper helper, string linkText, string action, string controller, object routeValues, object htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("a");
            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            tagBuilder.InnerHtml = linkText;

            tagBuilder.Attributes["href"] = urlHelper.Action(action, controller, routeValues);

            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(tagBuilder.ToString());
        }

        public static MvcHtmlString CustomActionLink(this HtmlHelper helper, string linkText, string action, string controller)
        {
            return CustomActionLink(helper, linkText, action, controller, null, null);
        }

        //public static MvcHtmlString EncodedActionLink(this HtmlHelper helper, string linkText, string action, string controller, object routeValues, object htmlAttributes)
        //{
        //    string queryString = string.Empty;
        //    string htmlAttributesString = string.Empty;
        //    if (routeValues != null)
        //    {
        //        RouteValueDictionary d = new RouteValueDictionary(routeValues);
        //        for (int i = 0; i < d.Keys.Count; i++)
        //        {
        //            if (i > 0)
        //            {
        //                queryString += "?";
        //            }
        //            queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
        //        }
        //    }

        //    if (htmlAttributes != null)
        //    {
        //        RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
        //        for (int i = 0; i < d.Keys.Count; i++)
        //        {
        //            htmlAttributesString += " " + d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
        //        }
        //    }

        //    //<a href="/Answer?questionId=14">What is Entity Framework??</a>
        //    StringBuilder ancor = new StringBuilder();
        //    ancor.Append("<a ");
        //    if (htmlAttributesString != string.Empty)
        //    {
        //        ancor.Append(htmlAttributesString);
        //    }
        //    ancor.Append(" href='");
        //    if (controller != string.Empty)
        //    {
        //        ancor.Append("/" + controller);
        //    }

        //    if (action != "Index")
        //    {
        //        ancor.Append("/" + action);
        //    }
        //    if (queryString != string.Empty)
        //    {
        //        //ancor.Append("?q=" + Encrypt(queryString));
        //        ancor.Append("?q=" + HttpUtility.UrlEncode(Encrypt(queryString)));
        //    }
        //    ancor.Append("'");
        //    ancor.Append(">");
        //    ancor.Append(linkText);
        //    ancor.Append("</a>");
        //    return new MvcHtmlString(ancor.ToString());
        //}

        public static MvcHtmlString EncodedActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            string queryString = string.Empty;
            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    if (i > 0)
                    {
                        queryString += "?";
                    }
                    queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            if (actionName != string.Empty)
            {
                actionName = (CommonMethod.EncodeUrl(actionName));
            }
            if (queryString != string.Empty)
            {
                queryString = ("?q=" + HttpUtility.UrlEncode(CommonMethod.EncodeUrl(queryString)));
            }
            var lnk = ajaxHelper.ActionLink(linkText, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString());
        }

        private static string Encrypt(string plainText)
        {
            string key = "jdsg432387#";
            byte[] EncryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            EncryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByte = Encoding.UTF8.GetBytes(plainText);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write);
            cStream.Write(inputByte, 0, inputByte.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }    
    }
}
