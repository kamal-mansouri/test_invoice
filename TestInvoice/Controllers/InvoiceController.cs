using System.Web.Mvc;
using TestInvoice.ViewModels;
using TestInvoice.Repository;
using TestInvoice.Services;

namespace TestInvoice.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IMangeInvoice mangeInvoice;

        public InvoiceController(MangeInvoice mange)
        {
            mangeInvoice = mange;
        }

        #region Index
        // GET: Invoice
        public ActionResult Index()
        {
            ViewBag.WithLayout = "true";
            return View("List", mangeInvoice.get_list());
        }
        #endregion

        #region AddInvoice
        public ActionResult AddInvoice()
        {
            ViewBag.ActionType = "add";
            ViewBag.ButtonTitle = "افزودن";

            return PartialView("invoice" , mangeInvoice.get_default_invoice());
        }
        #endregion

        #region EditInvoice
        public ActionResult EditInvoice(int id)
        {
            var invoice = mangeInvoice.get_invoice(id);

            ViewBag.ActionType = "edit";
            ViewBag.ButtonTitle = "ویرایش";

            return PartialView("invoice", invoice);
        }
        #endregion

        #region SetEditInvoice
        [HttpPost]
        public ContentResult SetNewInvoice(InvoiceViewModel invoiceSet)
        {
            ContentResult result = new ContentResult();

            if (ModelState.IsValid)
            {
                if (mangeInvoice.set_new_invoice(invoiceSet))
                {
                    result.Content = "true";
                }
                else
                {
                    result.Content = "false";
                }
            }

            return result;
        }
        #endregion

        #region SetEditInvoice
        [HttpPost]
        public ContentResult SetEditInvoice(InvoiceViewModel invoiceSet)
        {
            ContentResult result = new ContentResult();

            if (ModelState.IsValid) 
            {
                if (mangeInvoice.set_edit_invoice(invoiceSet))
                {
                    result.Content = "true";
                }
                else 
                {
                    result.Content = "false";
                }
            }
            return result;
        }
        #endregion

        #region GetCustomer
        public PartialViewResult GetCustomer(string text , string inputId)
        {
            ViewBag.text = text;
            ViewBag.inputId = inputId;
            ViewBag.type = "مشتری";

            return PartialView("SetItems", mangeInvoice.search_customers(text));
        }
        #endregion

        #region GetCommodity
        public PartialViewResult GetCommodity(string text , string inputId)
        {
            ViewBag.text = text;
            ViewBag.inputId = inputId;
            ViewBag.type = "کالا";

            return PartialView("SetItems", mangeInvoice.search_commodity(text));
        }
        #endregion

        #region Invoice list
        public PartialViewResult GetList()
        {
            ViewBag.WithLayout = "false";
            return PartialView("List", mangeInvoice.get_list());
        }
        #endregion
    }
}