using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestInvoice.Models;

namespace TestInvoice.ViewModels
{
    public class InvoiceViewModel
    {
        public Invoice invoice { get; set; }
        public List<InvoiceDetail> invoiceDetails { get; set; }
    }
}