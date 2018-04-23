using FeedbackCba.Core.Dtos;
using FeedbackCba.Core.Models;

namespace FeedbackCba.Core.Repositories
{
    public interface IFeedbackRepository
    {
        Feedback GetFeedback(string customerId, string pageUrl, bool isMainPage, string userId = "");
        bool Create(string customerId, FeedbackDto feedback);
    }
}