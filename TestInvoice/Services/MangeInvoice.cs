using System;
using System.Collections.Generic;
using System.Linq;
using TestInvoice.Context;
using TestInvoice.Models;
using TestInvoice.Repository;
using TestInvoice.Utilites;
using TestInvoice.ViewModels;

namespace TestInvoice.Services
{
    public class MangeInvoice : IMangeInvoice
    {
        private readonly AppContext db;
        public MangeInvoice()
        {
            db = new AppContext();
        }

        #region get_default_invoice
        public Invoice get_default_invoice()
        {
            Invoice invoice = new Invoice
            {
                Discount = 0,
                customer = new Customer { }
            };

            return invoice;
        }
        #endregion

        #region get_invoice
        public Invoice get_invoice(int id)
        {
            Invoice invoice = db.Invoices
                .Include("customer")
                .Include("invoiceDetails")
                .SingleOrDefault(c => c.Id == id);

            return invoice;
        }
        #endregion

        #region get_list
        public List<Invoice> get_list()
        {
            return db.Invoices.Include("invoiceDetails").ToList();
        }
        #endregion

        #region set_edit_invoice
        public bool set_edit_invoice(InvoiceViewModel invoiceSet)
        {
            try
            {
                Invoice _invoice = db.Invoices.SingleOrDefault(c => c.Id == invoiceSet.invoice.Id);
                _invoice.CustomerId = invoiceSet.invoice.CustomerId;
                _invoice.Discount = invoiceSet.invoice.Discount;
                _invoice.StoreId = 1;

                db.Entry(_invoice).State = System.Data.Entity.EntityState.Modified;

                ///////////////////////
                var comparer = new InvoiceDetailComparer();
                List<InvoiceDetail> newDetail = invoiceSet.invoiceDetails;
                List<InvoiceDetail> oldDetail = db.InvoiceDetailes.Where(c => c.InvoiceId == _invoice.Id).ToList();

                List<InvoiceDetail> deleteList = oldDetail.Except(newDetail, comparer).ToList();
                List<InvoiceDetail> insertList = newDetail.Except(oldDetail, comparer).ToList();
                List<InvoiceDetail> updateList = newDetail.Except(insertList, comparer).ToList();

                foreach (var item in insertList)
                {
                    InvoiceDetail _invoiceDetail = new InvoiceDetail
                    {
                        CommodityId = item.CommodityId,
                        Price = item.Price,
                        Qty = item.Qty,
                        InvoiceId = _invoice.Id
                    };
                     
                    db.InvoiceDetailes.Add(_invoiceDetail);
                }

                foreach (var item in updateList)
                {
                    InvoiceDetail _invoiceDetail = db.InvoiceDetailes.SingleOrDefault(c => c.Id == item.Id);

                    _invoiceDetail.CommodityId = item.CommodityId;
                    _invoiceDetail.Price = item.Price;
                    _invoiceDetail.Qty = item.Qty;

                    db.Entry(_invoiceDetail).State = System.Data.Entity.EntityState.Modified;
                }

                db.InvoiceDetailes.RemoveRange(deleteList);

                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region set_new_invoice
        public bool set_new_invoice(InvoiceViewModel invoiceSet)
        {
            try
            {
                Invoice _invoice = new Invoice();
                _invoice.CustomerId = invoiceSet.invoice.CustomerId;
                _invoice.Discount = invoiceSet.invoice.Discount;
                _invoice.StoreId = 1;

                db.Invoices.Add(_invoice);

                foreach (var item in invoiceSet.invoiceDetails)
                {
                    InvoiceDetail _invoiceDetail = new InvoiceDetail
                    {
                        CommodityId = item.CommodityId,
                        Price = item.Price,
                        Qty = item.Qty,
                        InvoiceId = _invoice.Id
                    };

                    db.InvoiceDetailes.Add(_invoiceDetail);
                }

                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, string> search_customers(string text)
        {
            return (from c in db.Customers
                          where
                          (c.Fname + " " + c.Lname).Contains(text)
                          orderby c.Fname, c.Lname
                          select c
              ).ToDictionary(k => k.Id, v => v.Fullname);
        }

        public Dictionary<int, string> search_commodity(string text)
        {
            return db.Commodities
                .Where(c => c.Title.Contains(text))
                .Select(t => new { t.Id, t.Title })
                .ToDictionary(k => k.Id, v => v.Title);
        }
    }
}