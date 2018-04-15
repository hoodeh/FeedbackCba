using FeedbackCba.Core.Models;
using FeedbackCba.Core.Repositories;
using System;
using System.Linq;
using FeedbackCba.Core.Dtos;

namespace FeedbackCba.Persistence.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IApplicationDbContext _context;

        public FeedbackRepository(IApplicationDbContext context)
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
                ex.Data.Add("customerId", customerId);
                ex.Data.Add("pageUrl", pageUrl);
                ex.Data.Add("isMainPage", isMainPage);
                ex.Data.Add("userId", userId);
                Console.WriteLine("FeedbackRepository.GetFeedback" + ex);
                return new Feedback();
            }
        }

        public bool Create(string customerId, FeedbackDto feedback)
        {
            try
            {
                _context.Feedbacks.Add(new Feedback
                {
                    CustomerId = new Guid(customerId),
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
                ex.Data.Add("customerId", customerId);
                Console.WriteLine("FeedbackRepository.Create" + ex);
                return false;
            }
        }
    }
}