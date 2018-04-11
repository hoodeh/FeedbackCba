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
        private readonly ApplicationDbContext _context;

        public CustomerReporitory(ApplicationDbContext context)
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
                Console.WriteLine("FeedbackCba.Persistence.Repositories.GetCustomer" + ex);
                return null;
            }
        }

        public bool IsValidCustomer(string customerId)
        {
            try
            {
                var customer = GetCustomer(customerId);
                return customer != null && customer.IsEnabled && customer.ExpireDate > DateTime.Now;
            }
            catch (Exception ex)
            {
                ex.Data.Add("customerId", customerId);
                Console.WriteLine("FeedbackCba.Persistence.Repositories.IsValidCustomer" + ex);
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
                Console.WriteLine("FeedbackCba.Persistence.Repositories.GetValidDomains" + ex);
                return null;
            }
        }

    }
}