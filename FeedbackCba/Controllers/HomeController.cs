using FeedbackCba.DAL;
using FeedbackCba.Models;
using FeedbackCba.Repositories;
using FeedbackCba.ViewModel;
using System;
using System.Web.Mvc;

namespace FeedbackCba.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserRepository _userRepository;
        private readonly FeedbackRepository _feedbackRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _userRepository = new UserRepository(_context);
            _feedbackRepository = new FeedbackRepository(_context);
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
            var user = _userRepository.GetUser(userId);
            var feedback = _feedbackRepository.GetFeedback(user.Guid, url, isMainPage);
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
            _userRepository.Update(new User { Guid = feedback.UserId, Email = feedback.UserEmail, Name = feedback.UserName });

            if (feedback.Id > 0)
            {
                _feedbackRepository.Update(feedback);
            }
            else
            {
                feedback.Id = _feedbackRepository.Create(feedback);
            }

            return Redirect(feedback.PageUrl);
        }

    }
}