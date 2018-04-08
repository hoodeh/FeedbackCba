using System;
using System.Linq;
using FeedbackCba.Core.Models;
using FeedbackCba.Core.Repositories;
using FeedbackCba.Core.ViewModel;

namespace FeedbackCba.Persistence.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;
        
        public FeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Feedback GetFeedback(string userId, string url, bool isMainPage)
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

        public int Create(FeedbackViewModel feedback)
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

        public bool Update(FeedbackViewModel feedback)
        {
            try
            {
                var existingFeedback = _context.Feedbacks.FirstOrDefault(f => f.Id == feedback.Id && f.UserId == feedback.UserId);
                if (existingFeedback == null)
                {
                    Create(feedback);
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
    }
}