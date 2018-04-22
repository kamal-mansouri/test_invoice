using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestInvoice.Models;

namespace TestInvoice.Context
{
    public class AppContext : DbContext
    {
        public AppContext():base("Name=InvoiceApp")
        {

        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetailes { get; set; }
    }
}