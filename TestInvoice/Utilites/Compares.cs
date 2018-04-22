using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestInvoice.Models;

namespace TestInvoice.Utilites
{
    public class InvoiceDetailComparer : IEqualityComparer<InvoiceDetail>
    {
        public bool Equals(InvoiceDetail x, InvoiceDetail y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(InvoiceDetail obj)
        {
            return obj.Id;
        }
    }
}