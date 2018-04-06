using FeedbackCba.Models;

namespace FeedbackCba.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string userId);
        bool Update(User user);
    }
}