namespace FeedbackCba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customer", "Name", c => c.String(maxLength: 150));
            AlterColumn("dbo.Customer", "BgColor", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customer", "BgColor", c => c.String());
            AlterColumn("dbo.Customer", "Name", c => c.String());
        }
    }
}
