using FeedbackCba.Core.Models;

namespace FeedbackCba.Core.Repositories
{
    public interface ICustomerReporitory
    {
        Customer GetCustomer(string customerId);
    }
}