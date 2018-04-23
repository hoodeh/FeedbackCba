namespace FeedbackCba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkQuestiontoCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Question", "CustomerId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Question", "CustomerId");
            AddForeignKey("dbo.Question", "CustomerId", "dbo.Customer", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Question", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Question", new[] { "CustomerId" });
            DropColumn("dbo.Question", "CustomerId");
        }
    }
}
