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
    [Authorize(Roles = "Supplier")]
    public class SupplierController : Controller
    {
        // GET: Supplier
        ShopEntityDb db =new ShopEntityDb();
        SupplierServices supplier = new SupplierServices();
        public ActionResult Index(int? page, string seach)
        {
            return View(supplier.GetListSupplier(page, seach));
        }
        [HttpGet]
        public ActionResult NewSupplier()
        {
            return View();
        }
        [ValidateInput(false)]// tắt kiểm tra dữ liệu đầu vào
        [HttpPost]
        public ActionResult NewSupplier(Supplier supplierNew)
        {
            if (supplierNew.Phone.Count() > 10)
            {
                ViewBag.error = "Số điện thoại không hợp lệ";
                return View(supplierNew);
            }
            supplier.NewSupplier(supplierNew);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditSupplier(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Supplier pd = db.Suppliers.SingleOrDefault(n => n.SupplierID == id);
            if (pd == null)
            {
                return HttpNotFound();
            }

            return View(pd);
        }
        [HttpPost]
        public ActionResult EditSupplier(Supplier model)
        {
            if (ModelState.IsValid)
            {
                supplier.EditSupplier(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteSupplier(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (supplier.DeleteSupplier(id) ==false)
            {
                return HttpNotFound();
            }
            supplier.DeleteSupplier(id);
            return RedirectToAction("Index");
        }
    }
}