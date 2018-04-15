using FeedbackCba.Persistence;

namespace FeedbackCba.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Persistence\Migrations";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //context.Questions.AddRange(new[]
            //{
            //    new Question
            //    {
            //        CustomerId = new Guid("bdc057dd-6439-447f-a777-a5aef8db40f0"),
            //        FromRate = 1,
            //        ToRate = 4,
            //        IsEnabled = true,
            //        Text = "What can we do to improve?",
            //        Type = QuestionType.Text,
            //    },
            //    new Question
            //    {
            //        CustomerId = new Guid("bdc057dd-6439-447f-a777-a5aef8db40f0"),
            //        FromRate = 5,
            //        ToRate = 8,
            //        IsEnabled = true,
            //        Text = "Is there anything we can do to improve?",
            //        Type = QuestionType.Text,
            //    },
            //    new Question
            //    {
            //        CustomerId = new Guid("bdc057dd-6439-447f-a777-a5aef8db40f0"),
            //        FromRate = 9,
            //        ToRate = 10,
            //        IsEnabled = true,
            //        Text = "Is there anything you particularly like?",
            //        Type =QuestionType.Text,
            //    }
            //});

            //context.Customers.Add(new Customer
            //{
            //    Id = new Guid("bdc057dd-6439-447f-a777-a5aef8db40f0"),
            //    Name = "CommBank WebApiGlobalExceptionHandler",
            //    Statement = "Thank you for using this service. We would appreciate your feedback by answering this question.",
            //    AppLevelQuestion = "How do you like this site?",
            //    PageLevelQuestion = "How helpful is this page?",
            //    ValidDomains = "http://localhost/59955",
            //    BgColor = "",
            //    CreateDate = DateTime.Now,
            //    ExpireDate = DateTime.Now.AddYears(1),
            //    IsEnabled = true
            //});
        }
    }
}
