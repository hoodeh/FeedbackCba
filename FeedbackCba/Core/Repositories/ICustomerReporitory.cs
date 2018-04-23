using FeedbackCba.Core.Models;
using System.Collections.Generic;

namespace FeedbackCba.Core.Repositories
{
    public interface ICustomerReporitory
    {
        Customer GetCustomer(string customerId);
        bool IsValidCustomer(string customerId);
        IEnumerable<string> GetValidDomains(string customerId);
    }
}