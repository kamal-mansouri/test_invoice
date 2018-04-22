using System;
using System.Collections.Generic;
using TestInvoice.Models;

namespace TestInvoice.Repository
{
    public interface IMangeInvoice : IDisposable
    {
        /// <summary>
        /// برگرداندن لیست فاکنور ها
        /// </summary>
        /// <returns></returns>
        List<Invoice> get_list();

        /// <summary>
        /// برگرداندن یک فاکتور با ای دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Invoice get_invoice(int id);

        /// <summary>
        /// برگرداندن یک فاکتور پیش فرض خالی
        /// </summary>
        /// <returns></returns>
        Invoice get_default_invoice();

        /// <summary>
        /// افزودن فاکتور جدید
        /// </summary>
        /// <param name="invoiceSet"></param>
        /// <returns></returns>
        bool set_new_invoice(ViewModels.InvoiceViewModel invoiceSet);

        /// <summary>
        /// ویرایش فاکتور
        /// </summary>
        /// <param name="invoiceSet"></param>
        /// <returns></returns>
        bool set_edit_invoice(ViewModels.InvoiceViewModel invoiceSet);

        /// <summary>
        /// جستجو در مشتریان و بگرداندن مشتریان با نام مشابه
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Dictionary<int, string> search_customers(string text);

        /// <summary>
        /// جستجو در کالا ها و بگرداندن کالا های با نام مشابه
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Dictionary<int, string> search_commodity(string text);
    }
}
