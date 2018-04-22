namespace TestInvoice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_invoice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "WriteDate", c => c.DateTime());
            AlterColumn("dbo.Invoices", "RegDate", c => c.DateTime());
            AlterColumn("dbo.Customers", "Fname", c => c.String(maxLength: 40));
            AlterColumn("dbo.Customers", "Lname", c => c.String(maxLength: 60));
            AlterColumn("dbo.Customers", "NationalCode", c => c.String(maxLength: 10));
            AlterColumn("dbo.Customers", "Address", c => c.String(maxLength: 500));
            DropColumn("dbo.InvoiceDetails", "Price");
            DropColumn("dbo.Invoices", "Discount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "Discount", c => c.Long(nullable: false));
            AddColumn("dbo.InvoiceDetails", "Price", c => c.Long(nullable: false));
            AlterColumn("dbo.Customers", "Address", c => c.String());
            AlterColumn("dbo.Customers", "NationalCode", c => c.String());
            AlterColumn("dbo.Customers", "Lname", c => c.String());
            AlterColumn("dbo.Customers", "Fname", c => c.String());
            AlterColumn("dbo.Invoices", "RegDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Invoices", "WriteDate", c => c.DateTime(nullable: false));
        }
    }
}
