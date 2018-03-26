using FeedbackCba.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FeedbackCba.DAL
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("FeedbackConnection")
        {
        }

        public DbSet<Question> Questions { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}