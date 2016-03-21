using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace TaskMA.Helpers
{
    public static class HtmlHelpers
    {
        public static string Truncate(this HtmlHelper helper, string input, int length)
        {
            if (input.Length <= length)
            {
                return input;
            }
            else
            {
                return input.Substring(0, length) + "...";
            }

        }

        public static MvcHtmlString MyTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var member = (MemberExpression)expression.Body;
            string propertyName = member.Member.Name;
            var resultScript = "<script>$(function() {$('#" + propertyName + "').datepicker();});</script>";
            MvcHtmlString result = new MvcHtmlString(resultScript);
            return new MvcHtmlString(helper.MyTextBoxFor(expression).ToHtmlString() + result);
        }

    }
}