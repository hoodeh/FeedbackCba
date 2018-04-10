using FeedbackCba.Core.Models;
using FeedbackCba.Core.Repositories;
using FeedbackCba.Core.ViewModel;
using System;
using System.Linq;

namespace FeedbackCba.Persistence.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;

        public FeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Feedback GetFeedback(string customerId, string pageUrl, bool isMainPage, string userId = "")
        {
            try
            {
                return _context.Feedbacks
                    .Where(f => f.CustomerId == new Guid(customerId) && f.PageUrl == pageUrl && f.IsMainPage == isMainPage && (f.UserId ?? "") == userId)
                    .OrderByDescending(f => f.SubmitDate)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new Feedback();
            }
        }

        public bool Create(FeedbackViewModel feedback)
        {
            try
            {
                _context.Feedbacks.Add(new Feedback
                {
                    CustomerId = new Guid(feedback.CustomerId),
                    UserId = feedback.UserId ?? "",
                    Rate = feedback.Rate,
                    IsMainPage = feedback.IsMainPage,
                    PageUrl = feedback.PageUrl,
                    QuestionId = feedback.QuestionId,
                    UserReply = feedback.UserReply ?? "",
                    SubmitDate = DateTime.Now
                });

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