using FeedbackCba.DAL;
using FeedbackCba.Models;
using FeedbackCba.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FeedbackCba.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
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
            var user = GetUser(userId);
            var feedback = GetFeedback(user.Guid, url, isMainPage);
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
            UpdateUser(new User {Guid = feedback.UserId, Email = feedback.UserEmail, Name = feedback.UserName});

            if (feedback.Id > 0)
            {
                UpdateFeedback(feedback);
            }
            else
            {
                feedback.Id = CreateFeedback(feedback);
            }

            return Redirect(feedback.PageUrl);
        }


        private User GetUser(string userId)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.Guid == userId) ?? new User { Guid = Guid.NewGuid().ToString() };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new User();
            }
        }

        private Feedback GetFeedback(string userId, string url, bool isMainPage)
        {
            try
            {
                return _context.Feedbacks
                    .Where(f => f.UserId == userId && f.PageUrl == url && f.IsMainPage == isMainPage)
                    .OrderByDescending(f => f.SubmitDate)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new Feedback();
            }
        }

        private int CreateFeedback(FeedbackViewModel feedback)
        {
            try
            { 
                var newFeedback = new Feedback
                {
                    UserId = feedback.UserId,
                    Score = feedback.Score,
                    Answer = feedback.Answer,
                    IsMainPage = feedback.IsMainPage,
                    PageUrl = feedback.PageUrl,
                    SubmitDate = DateTime.Now
                };

                _context.Feedbacks.Add(newFeedback);
                _context.SaveChanges();

                return newFeedback.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        private bool UpdateFeedback(FeedbackViewModel feedback)
        {
            try
            { 
                var existingFeedback = _context.Feedbacks.FirstOrDefault(f => f.Id == feedback.Id && f.UserId == feedback.UserId);
                if (existingFeedback == null)
                {
                    CreateFeedback(feedback);
                }
                else
                {
                    existingFeedback.Answer = feedback.Answer;
                    existingFeedback.Score = feedback.Score;
                    existingFeedback.SubmitDate = DateTime.Now;
                    _context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        private bool UpdateUser(User user)
        {
            try
            {
                var existUser = _context.Users.FirstOrDefault(u => u.Guid == user.Guid);
                if (existUser == null)
                {
                    _context.Users.Add(user);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(user.Email) && existUser.Email != user.Email)
                    {
                        existUser.Email = user.Email;
                    }

                    if (!string.IsNullOrWhiteSpace(user.Name) && existUser.Name != user.Name)
                    {
                        existUser.Name = user.Name;
                    }
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}