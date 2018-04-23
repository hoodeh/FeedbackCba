using System.Linq;
using System.Web;
using FeedbackCba.Core;

namespace FeedbackCba.Persistence
{

    public interface ICustomerDomainValidator
    {
        bool IsValidHostName(string customerId, out string hostName);
    }

    public class CustomerDomainValidator : ICustomerDomainValidator
    {
        private readonly HttpContextBase _httpContext;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerDomainValidator(IUnitOfWork unitOfWork, HttpContextBase httpContext)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
        }

        public bool IsValidHostName(string customerId, out string hostName)
        {
            hostName = string.Empty;
            var host = _httpContext.Request.Headers["Origin"].ToLower();
            var validDomains = _unitOfWork.Customers.GetValidDomains(customerId);
            if (validDomains.Any(p => p.ToLower().Equals(host)))
            {
                hostName = host;
                return true;
            }

            return false;
        }
    }
}