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
    [Authorize(Roles = "Product")]
    public class ProductController : Controller
    {
       
        ShopEntityDb db = new ShopEntityDb();
        ProductServices productServices = new ProductServices();
        public ActionResult Index(int? page , string searchKey)
        {
            return View(productServices.GetListProduct(page, searchKey));         
        }
        
        [HttpGet]
        public ActionResult NewProduct()
        {
            ViewBag.SupplierID = new SelectList(db.Suppliers.OrderBy(n => n.SupplierName), "SupplierID", "SupplierName");
            ViewBag.CategoryID = new SelectList(db.Categories.OrderBy(n => n.CategoryName), "CategoryID", "CategoryName");
            return View();
        }
        [ValidateInput(false)]// tắt kiểm tra dữ liệu đầu vào

        [HttpPost]
        public ActionResult NewProduct(Product product, HttpPostedFileBase photoPath)
        {
            ViewBag.SupplierID = new SelectList(db.Suppliers.OrderBy(n => n.SupplierName), "SupplierID", "SupplierName");
            ViewBag.CategoryID = new SelectList(db.Categories.OrderBy(n => n.CategoryName), "CategoryID", "CategoryName");
            product.PhotoPath = GetPhoto(photoPath);
            productServices.NewProduct(product);
            return RedirectToAction("Index");
        }
        // Edit Product
        [HttpGet]
        public ActionResult EditProduct(int? productId)
        {
            if (productId == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Product product = db.Products.SingleOrDefault(n =>n.ProductID == productId);
            if(product == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers.OrderBy(n => n.SupplierName), "SupplierID", "SupplierName");
            ViewBag.CategoryID = new SelectList(db.Categories.OrderBy(n => n.CategoryName), "CategoryID", "CategoryName");
            return View(product);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditProduct(Product product, HttpPostedFileBase photoPath)
        {
            ViewBag.SupplierID = new SelectList(db.Suppliers.OrderBy(n => n.SupplierName), "SupplierID", "SupplierName",product.SupplierID);
            ViewBag.CategoryID = new SelectList(db.Categories.OrderBy(n => n.CategoryName), "CategoryID", "CategoryName",product.CategoryID);
            product.PhotoPath = GetPhoto(photoPath);
            if (ModelState.IsValid)
            {
                productServices.EditProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            productServices.DeleteProduct(id);
            return RedirectToAction("Index");
        }
        public string GetPhoto(HttpPostedFileBase photoPath)
        {
            if (photoPath != null)
            {
                var fileName = Path.GetFileName(photoPath.FileName);
                // lay anh chuyen vao thu muc
                var path = Path.Combine(Server.MapPath("~/Content/Image"), fileName);
                //lay anh dua vao thu muc hinh anh
                photoPath.SaveAs(path);
                //Session["tenHinh"] = PhotoPath.FileName;
                //ViewBag.tenHinh = "";
                return fileName;
            }
            return null;
        }
    }
}