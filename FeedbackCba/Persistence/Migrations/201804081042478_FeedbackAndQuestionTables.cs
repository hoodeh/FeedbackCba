namespace FeedbackCba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeedbackAndQuestionTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedback",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Guid(nullable: false),
                        UserId = c.String(maxLength: 50),
                        SubmitDate = c.DateTime(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PageUrl = c.String(),
                        IsMainPage = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        UserReply = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Question", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(maxLength: 255),
                        Type = c.Short(nullable: false),
                        FromRate = c.Short(nullable: false),
                        ToRate = c.Short(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedback", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Feedback", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Feedback", new[] { "QuestionId" });
            DropIndex("dbo.Feedback", new[] { "CustomerId" });
            DropTable("dbo.Question");
            DropTable("dbo.Feedback");
        }
    }
}
