using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TestInvoice.Models
{
    public class Invoice
    {
        public Invoice()
        {
            RegDate = DateTime.Now;
        }

        public int Id { get; set; }
        public int StoreId { get; set; }
        public int CustomerId { get; set; }

        [Display(Name ="تخفیف")]
        [DefaultValue(0)]
        public Int64 Discount { get; set; }

        [Display(Name = "جمع کل")]
        public Int64 TotalNet
        {
            get
            {
                if (this.invoiceDetails == null)
                {
                    return 0;
                }

                Int64 _TotalNet = 0;

                foreach (var item in this.invoiceDetails)
                {
                    _TotalNet += (item.Price * item.Qty);
                }

                return _TotalNet - this.Discount;
                return 0;
            }
        }

        [Display(Name = "تاریخ ثبت", Description = "تاریخ ثبت در سیستم")]
        //[DataType(DataType.Date)]
        public DateTime? RegDate { get; set; }

        public virtual Store store { get; set; }
        public virtual Customer customer { get; set; }
        public virtual ICollection<InvoiceDetail> invoiceDetails { get; set; }
    }
}