using FeedbackCba.Core;
using FeedbackCba.Core.ViewModel;
using System;
using System.Linq;
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

        [HttpGet]
        public ActionResult Feedback(string customerId, string pageUrl, bool isMainPage = true, string userId = "")
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return HttpNotFound();
            }

            var customer = _unitOfWork.Customers.GetCustomer(customerId);
            if (!customer.IsEnabled || customer.ExpireDate < DateTime.Now)
            {
                return View("ExpiredPackage");
            }

            if(!customer.ValidDomains.Split(';').Any(valiDomain => pageUrl.ToLower().Contains(valiDomain.ToLower())))
            {
                throw new UnauthorizedAccessException("Unauthorized web address");
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

        /// <summary>
        /// POST : /Home/_AjaxUpdateFeedback
        /// <para>Create feedback.</para>
        /// </summary>
        /// <returns>Json object with result</returns>
        [HttpPost]
        public ActionResult _AjaxUpdateFeedback(FeedbackViewModel feedback)
        {
            if (_unitOfWork.Feedbacks.Create(feedback))
            {
                _unitOfWork.Complete();
                return Json(
                    new
                    {
                        type = "success",
                        message = "Feedback created."
                    });
            }

            return Json(
                new
                {
                    type = "error", //or error
                    message = "Cannot update feedback. Please try again later."
                });
        }

    }
}