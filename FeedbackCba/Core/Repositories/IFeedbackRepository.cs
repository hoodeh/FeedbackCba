using FeedbackCba.Core.Models;
using FeedbackCba.Dtos;

namespace FeedbackCba.Core.Repositories
{
    public interface IFeedbackRepository
    {
        Feedback GetFeedback(string customerId, string pageUrl, bool isMainPage, string userId = "");
        bool Create(string customerId, FeedbackDto feedback);
    }
}