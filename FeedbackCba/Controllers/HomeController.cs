using FeedbackCba.Core;
using FeedbackCba.Core.Models;
using FeedbackCba.Core.ViewModel;
using System;
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

        public ActionResult _Feedback(string url, bool isMainPage, string userId)
        {
            var user = _unitOfWork.Users.GetUser(userId);
            var feedback = _unitOfWork.Feedbacks.GetFeedback(user.Guid, url, isMainPage);
            if (feedback == null || DateTime.Now > feedback.SubmitDate.AddDays(180))
            {
                feedback = new Feedback {UserId = user.Guid, PageUrl = url, IsMainPage = isMainPage};
            }

            return PartialView(new FeedbackViewModel
            {
                Id = feedback.Id,
                PageUrl = feedback.PageUrl,
                IsMainPage = feedback.IsMainPage,
                Answer = feedback.Answer,
                Score = feedback.Score,
                SubmitDate = feedback.SubmitDate,
                UserId = user.Guid,
                UserName = user.Name,
                UserEmail = user.Email
            });
        }

        [HttpPost]
        public ActionResult _UpdateFeedback(FeedbackViewModel feedback)
        {
            _unitOfWork.Users.Update(new User { Guid = feedback.UserId, Email = feedback.UserEmail, Name = feedback.UserName });

            if (feedback.Id > 0)
            {
                _unitOfWork.Feedbacks.Update(feedback);
            }
            else
            {
                feedback.Id = _unitOfWork.Feedbacks.Create(feedback);
            }

            return Redirect(feedback.PageUrl);
        }

    }
}