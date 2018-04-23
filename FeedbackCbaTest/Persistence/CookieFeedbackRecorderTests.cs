using FeedbackCba.Core;
using FeedbackCba.Persistence;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web;

namespace FeedbackCbaTest.Persistence
{
    [TestClass]
    public class CookieFeedbackRecorderTests
    {
        private CookieFeedbackRecorder  _feedback;
        private string _customerId = "dbb2db69-917f-4989-90a6-48ec7562ee39";
        private string _pageUrl = "https://cba.com.au";
        private Mock<HttpResponseBase> _response;
        private Mock<ISystemClock> _clock;

        [TestInitialize]
        public void TestInitialize()
        {
            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            _response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            _clock = new Mock<ISystemClock>();

            _response.SetupGet(x => x.Cookies).Returns(new HttpCookieCollection());
            request.SetupGet(x => x.Cookies).Returns(new HttpCookieCollection());
            
            context.SetupGet(x => x.Request).Returns(request.Object);
            context.SetupGet(x => x.Response).Returns(_response.Object);

            _feedback = new CookieFeedbackRecorder(context.Object, _clock.Object);
        }

        [TestMethod]
        public void RecordFeedback_NewUser_ShouldCreateCookie()
        {
            var now = DateTime.Now;
            _clock.SetupGet(p => p.Now).Returns(now);
            _feedback.RecordFeedback(_customerId, _pageUrl);

            var feedbackCookie = _response.Object.Cookies["CbaFeedback_" + _customerId];
            var result = feedbackCookie[_pageUrl];

            result.Should().NotBeNull();
        }

        [TestMethod]
        public void RecordFeedback_NewUser_CookieValueShouldBeDateTimeNow()
        {
            var now = DateTime.Now;
            _clock.SetupGet(p => p.Now).Returns(now);
            _feedback.RecordFeedback(_customerId, _pageUrl);

            var feedbackCookie = _response.Object.Cookies["CbaFeedback_" + _customerId];
            var result = feedbackCookie[_pageUrl];

            result.Should().Be(now.ToString("O"));
        }

    }
}
