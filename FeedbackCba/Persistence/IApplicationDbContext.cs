using System.Data.Entity;
using FeedbackCba.Core.Models;

namespace FeedbackCba.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Feedback> Feedbacks { get; set; }
        DbSet<Question> Questions { get; set; }
    }
}