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
    [Authorize(Roles = "Category")]
    public class CategoryController : Controller
    {
        // GET: Category
        ShopEntityDb db = new ShopEntityDb();
        CategoryServices category = new CategoryServices();
        public ActionResult Index(int? page, string seach)
        {
            return View(category.GetListCategory(page, seach));
        }
        [HttpGet]
        public ActionResult NewCategory()
        {       
            return View();
        }
        [ValidateInput(false)]// tắt kiểm tra dữ liệu đầu vào
        [HttpPost]
        public ActionResult NewCategory(Category model)
        {
            category.NewCategory(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Category model = db.Categories.SingleOrDefault(n => n.CategoryID == id);
            if (model == null)
            {
                return HttpNotFound();
            }
 
            return View(model);
        }
        [HttpPost]
        public ActionResult EditCategory(Category model)
        {
            category.EditCategory(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            category.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}