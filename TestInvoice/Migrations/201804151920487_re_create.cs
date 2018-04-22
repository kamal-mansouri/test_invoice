namespace TestInvoice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class re_create : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceDetails", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "Discount", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Discount");
            DropColumn("dbo.InvoiceDetails", "Price");
        }
    }
}
