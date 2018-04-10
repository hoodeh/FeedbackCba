namespace FeedbackCba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingStatementAndQuestionToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "Statement", c => c.String());
            AddColumn("dbo.Customer", "AppLevelQuestion", c => c.String());
            AddColumn("dbo.Customer", "PageLevelQuestion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "PageLevelQuestion");
            DropColumn("dbo.Customer", "AppLevelQuestion");
            DropColumn("dbo.Customer", "Statement");
        }
    }
}
