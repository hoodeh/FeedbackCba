using FeedbackCba.Core.Models;
using FeedbackCba.Core.ViewModel;

namespace FeedbackCba.Core.Repositories
{
    public interface IFeedbackRepository
    {
        Feedback GetFeedback(string userId, string url, bool isMainPage);
        int Create(FeedbackViewModel feedback);
        bool Update(FeedbackViewModel feedback);
    }
}