using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopWebApplication.Models;
using PagedList;
using ShopWebApplication.Services;

namespace ShopWebApplication.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        // GET: Customer
        ShopEntityDb db = new ShopEntityDb();
        CustomerServices customer = new CustomerServices();
        // Index Customer
        public ActionResult Index(int? page, string seach)
        {
            return View(customer.GetListCustomer(page, seach));
        }
        [HttpGet]
        public ActionResult NewCustomer()
        {

            return View();
        }
        [ValidateInput(false)]// tắt kiểm tra dữ liệu đầu vào
        [HttpPost]
        public ActionResult NewCustomer(Customer customerNew)
        {
            customer.NewCustomer(customerNew);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditCustomer(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Customer pd = db.Customers.SingleOrDefault(n => n.CustomerID == id);
            if (pd == null)
            {
                return HttpNotFound();
            }

            return View(pd);
        }
        [HttpPost]
        public ActionResult EditCustomer(Customer model)
        {
            if (ModelState.IsValid)
            {
               customer.EditCustomer(model);
               return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteCustomer(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (customer.DeleteCustomer(id)==false)
            {
                return HttpNotFound();
            }
            customer.DeleteCustomer(id);
            return RedirectToAction("Index");
        }
       
    }
}