using System;

namespace TestInvoice.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int CommodityId { get; set; }
        public int Qty { get; set; }
        public Int32 Price { get; set; }

        public virtual Invoice invoice { get; set; }
        public virtual Commodity commodity { get; set; }
    }
}