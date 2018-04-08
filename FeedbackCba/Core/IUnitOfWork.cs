using FeedbackCba.Core.Repositories;

namespace FeedbackCba.Core
{
    public interface IUnitOfWork
    {
        IFeedbackRepository Feedbacks { get; }
        IUserRepository Users { get; }
        void Complete();
    }
}