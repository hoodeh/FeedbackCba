using FeedbackCba.Core.Repositories;

namespace FeedbackCba.Core
{
    public interface IUnitOfWork
    {
        ICustomerReporitory Customers { get; }
        IFeedbackRepository Feedbacks { get; }
        void Complete();
    }
}