using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestInvoice.Models
{
    public class Commodity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? RegDate = DateTime.Now;

        public virtual ICollection<InvoiceDetail> invoiceDetails { get; set; }
    }
}