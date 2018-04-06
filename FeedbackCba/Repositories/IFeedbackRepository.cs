using FeedbackCba.Models;
using FeedbackCba.ViewModel;

namespace FeedbackCba.Repositories
{
    public interface IFeedbackRepository
    {
        Feedback GetFeedback(string userId, string url, bool isMainPage);
        int Create(FeedbackViewModel feedback);
        bool Update(FeedbackViewModel feedback);
    }
}