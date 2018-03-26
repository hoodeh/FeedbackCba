namespace FeedbackCba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuestionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Text = c.String(),
                        Rate = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Question");
        }
    }
}
