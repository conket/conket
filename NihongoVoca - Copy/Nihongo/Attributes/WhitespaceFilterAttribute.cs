﻿using Nihongo.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Nihongo.Attributes
{
    public class WhitespaceFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;
            response.Filter = new WhiteSpaceFilter(response.Filter, s =>
            {
                //s = Regex.Replace(s, @"s+", " ");
                s = Regex.Replace(s, @"\n|\t", " ");
                s = Regex.Replace(s, @"s*ns*", "n");
                s = Regex.Replace(s, @"s*>s*<s*", "><");
                s = Regex.Replace(s, @"<!--(.*?)-->", ""); //Remove comments
                // single-line doctype must be preserved
                var firstEndBracketPosition = s.IndexOf(">");
                if (firstEndBracketPosition >= 0)
                {
                    s = s.Remove(firstEndBracketPosition, 1);
                    s = s.Insert(firstEndBracketPosition, ">");
                }
                return s;
            });
        }
    }
}