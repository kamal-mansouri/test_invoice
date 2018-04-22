using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestInvoice.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Display(Name = "نام ")]
        [MaxLength(40)]
        public string Fname { get; set; }

        [Display(Name = "نام خانوادگی")]
        [MaxLength(60)]
        public string Lname { get; set; }

        [Display(Name = "نام مشتری")]
        [NotMapped]
        public string Fullname {
            get { return Fname + " " + Lname; }
        }

        [Display(Name = "کد ملی")]
        [StringLength(10 , MinimumLength =10)]
        public string NationalCode { get; set; }


        [Display(Name = "آدرس")]
        [MaxLength(500)]
        public string Address { get; set; }

        [Display(Name = "تاریخ ثبت", Description = "تاریخ ثبت در سیستم")]
        [DataType(DataType.Date)]
        public DateTime? RegDate { get; set; }

        public virtual ICollection<Invoice> invoices { get; set; }
    }
}