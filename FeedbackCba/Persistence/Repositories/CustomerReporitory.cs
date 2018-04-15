using FeedbackCba.Core.Models;
using FeedbackCba.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FeedbackCba.Persistence.Repositories
{
    public class CustomerReporitory : ICustomerReporitory
    {
        private readonly IApplicationDbContext _context;

        public CustomerReporitory(IApplicationDbContext context)
        {
            _context = context;
        }

        public Customer GetCustomer(string customerId)
        {
            try
            {
                return _context.Customers
                    .Where(c => c.Id == new Guid(customerId))
                    .Include(c => c.Questions)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.Data.Add("customerId", customerId);
                Console.WriteLine("CustomerReporitory.GetCustomer" + ex);
                return null;
            }
        }

        public bool IsValidCustomer(string customerId)
        {
            try
            {
                return _context.Customers.Any(c => c.Id == new Guid(customerId) && c.IsEnabled && c.ExpireDate > DateTime.Now);
            }
            catch (Exception ex)
            {
                ex.Data.Add("customerId", customerId);
                Console.WriteLine("CustomerReporitory.IsValidCustomer" + ex);
                return false;
            }
        }

        public IEnumerable<string> GetValidDomains(string customerId)
        {
            try
            {
                var validDomains = _context.Customers
                    .Where(c => c.Id == new Guid(customerId))
                    .Select(c => c.ValidDomains)
                    .FirstOrDefault();

                return (validDomains ?? "").Split(';');
            }
            catch (Exception ex)
            {
                ex.Data.Add("customerId", customerId);
                Console.WriteLine("CustomerReporitory.GetValidDomains" + ex);
                return null;
            }
        }

    }
}