using FeedbackCba.Core.Models;
using FeedbackCba.Core.Repositories;
using System;
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
                Console.WriteLine(ex);
                return null;
            }
        }


    }
}