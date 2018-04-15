using FeedbackCba.Core;
using FeedbackCba.Core.ViewModel;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FeedbackCba.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpOptions]
        [ActionName("Feedback")]
        public ActionResult FeedbackOptions(string customerId, string pageUrl, bool isMainPage = true, string userId = "")
        {
            Response.AddHeader("Access-Control-Allow-Origin", "*");
            Response.AddHeader("Access-Control-Allow-Headers", "Content-Type");
            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        public ActionResult Feedback(string customerId, string pageUrl, bool isMainPage = true, string userId = "")
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You broke the Internet!");
            }

            var customer = _unitOfWork.Customers.GetCustomer(customerId);
            if (customer == null)
            {
                return HttpNotFound();
            }

            if (!customer.IsValid())
            {
                return View("ExpiredPackage");
            }

            if(!string.IsNullOrEmpty(customer.ValidDomains) && 
                !customer.ValidDomains.Split(';').Any(valiDomain => pageUrl.ToLower().Contains(valiDomain.ToLower())))
            {
                throw new UnauthorizedAccessException("Unauthorized web address");
            }

            // Check if more than 180 days from last feedback
            var recentFeedback = _unitOfWork.Feedbacks.GetFeedback(customerId, pageUrl, isMainPage, userId);
            if (recentFeedback != null && recentFeedback.SubmitDate.AddDays(180) > DateTime.Now)
            {
                return new EmptyResult();
            }

            Response.AddHeader("Access-Control-Allow-Origin", "*");

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