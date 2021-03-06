﻿using FeedbackCba.Core.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FeedbackCba.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Question> Questions { get; set; }

        public ApplicationDbContext() : base("FeedbackCbaConn")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Question>()
                .HasRequired(q => q.Customer)
                .WithMany(c => c.Questions)
                .WillCascadeOnDelete(false);
        }
    }
}