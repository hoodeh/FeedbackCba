namespace FeedbackCba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedback : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedback",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        Score = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PageUrl = c.String(),
                        IsMainPage = c.Boolean(nullable: false),
                        Answer = c.String(),
                        User_Guid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Guid)
                .Index(t => t.User_Guid);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Guid = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 255),
                        Email = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Guid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedback", "User_Guid", "dbo.User");
            DropIndex("dbo.Feedback", new[] { "User_Guid" });
            DropTable("dbo.User");
            DropTable("dbo.Feedback");
        }
    }
}
