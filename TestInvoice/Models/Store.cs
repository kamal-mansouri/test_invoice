using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestInvoice.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool  Status { get; set; }

        public virtual ICollection<Invoice> invoices { get; set; }
    }
}