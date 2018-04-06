using FeedbackCba.Repositories;

namespace FeedbackCba.Persistence
{
    public interface IUnitOfWork
    {
        IFeedbackRepository Feedbacks { get; }
        IUserRepository Users { get; }
        void Complete();
    }
}