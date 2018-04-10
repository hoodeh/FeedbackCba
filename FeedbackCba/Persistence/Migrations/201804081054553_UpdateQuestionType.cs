namespace FeedbackCba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateQuestionType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Question", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Question", "Type", c => c.Short(nullable: false));
        }
    }
}
