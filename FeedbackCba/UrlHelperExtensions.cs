using System;
using System.Web;
using System.Web.Mvc;

namespace FeedbackCba
{
    public static class UrlHelperExtensions
    {
        public static string GetAbsoluteUrl(this UrlHelper urlHelper, string relativeUrl)
        {
            var urlBuilder = new UriBuilder(HttpContext.Current.Request.Url.AbsoluteUri)
            {
                Path = urlHelper.Content(relativeUrl),
                Query = null,
            };

            return urlBuilder.ToString();
        }
    }
}