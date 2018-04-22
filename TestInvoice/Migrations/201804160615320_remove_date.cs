namespace TestInvoice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_date : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Invoices", "Comment");
            DropColumn("dbo.Invoices", "WriteDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "WriteDate", c => c.DateTime());
            AddColumn("dbo.Invoices", "Comment", c => c.String());
        }
    }
}
