using FeedbackCba.Core;
using System;
using System.Web;

namespace FeedbackCba.Persistence
{
    public class CookieFeedbackRecorder : IFeedbackRecorder
    {
        private readonly HttpContextBase _httpContext;
        private readonly ISystemClock _clock;

        public CookieFeedbackRecorder(HttpContextBase httpContext, ISystemClock clock)
        {
            _httpContext = httpContext;
            _clock = clock;
        }

        public void RecordFeedback(string customerId, string pageUrl)
        {
            var feedbackCookie = _httpContext.Request.Cookies["CbaFeedback_" + customerId];
            if (feedbackCookie == null)
            {
                feedbackCookie = new HttpCookie("CbaFeedback_" + customerId);
                feedbackCookie.Domain = "localhost";
            }

            feedbackCookie[pageUrl] = _clock.Now.ToString("O");
            feedbackCookie.Expires = _clock.Now.AddDays(180);

            _httpContext.Response.Cookies.Add(feedbackCookie);
        }

        public bool CanProvideFeedback(string customerId, string pageUrl)
        {
            var feedbackCookie = _httpContext.Request.Cookies["CbaFeedback_" + customerId];
            if (feedbackCookie != null)
            {
                if (feedbackCookie[pageUrl] != null)
                {
                    var submitDate = DateTime.Parse(feedbackCookie[pageUrl]);
                    if (submitDate.AddDays(180) > _clock.Now)
                    {
                        return false;
                    }

                    feedbackCookie.Values.Remove(pageUrl);
                    _httpContext.Response.Cookies.Add(feedbackCookie);
                }
            }

            return true;
        }
    }
}