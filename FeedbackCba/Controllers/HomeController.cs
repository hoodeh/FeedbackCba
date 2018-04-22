using FeedbackCba.Core;
using FeedbackCba.Core.ViewModel;
using FeedbackCba.Persistence;
using System.Net;
using System.Web.Mvc;

namespace FeedbackCba.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFeedbackRecorder _feedbackRecorder;
        private readonly ICustomerDomainValidator _domainValidator;

        public HomeController(IUnitOfWork unitOfWork, IFeedbackRecorder feedbackRecorder, ICustomerDomainValidator domainValidator)
        {
            _unitOfWork = unitOfWork;
            _feedbackRecorder = feedbackRecorder;
            _domainValidator = domainValidator;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Feedback(string customerId, string pageUrl = "", bool isMainPage = true, string userId = "")
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You must provide CustomerId");
            }

            var customer = _unitOfWork.Customers.GetCustomer(customerId);
            if (customer == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Customer not exist");
            }

            if (!customer.IsValid())
            {
                return View("ExpiredPackage");
            }

            string hostName;
            if (_domainValidator.IsValidHostName(customerId, out hostName))
            {
                Response.AddHeader("Access-Control-Allow-Origin", hostName);
                Response.AddHeader("Access-Control-Allow-Credentials", "true");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Unauthorized access");
            }

            if (string.IsNullOrWhiteSpace(pageUrl))
            {
                pageUrl = System.Web.HttpContext.Current.Request.Headers["Referer"].ToLower();
            }

            // Check if more than 180 days from last feedback
            if (!_feedbackRecorder.CanProvideFeedback(customerId, pageUrl))
            {
                return new EmptyResult();
            }

            return View(new FeedbackViewModel
            {
                CustomerId = customerId,
                UserId = userId,
                IsMainPage = isMainPage,
                PageUrl = pageUrl,
                Statement = customer.Statement,
                MainQuestion = isMainPage ? customer.AppLevelQuestion : customer.PageLevelQuestion,
                Questions = customer.Questions,
                BgColor = customer.BgColor
            });
        }
    }
}