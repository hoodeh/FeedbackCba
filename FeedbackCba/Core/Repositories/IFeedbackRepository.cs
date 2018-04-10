using FeedbackCba.Core.Models;
using FeedbackCba.Core.ViewModel;

namespace FeedbackCba.Core.Repositories
{
    public interface IFeedbackRepository
    {
        Feedback GetFeedback(string customerId, string pageUrl, bool isMainPage, string userId = "");
        bool Create(FeedbackViewModel feedback);
    }
}