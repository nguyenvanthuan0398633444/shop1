using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWebApplication.Models;

namespace ShopWebApplication.Controllers
{
    //[Authorize(Roles = "Customer,Account,Category,DangNhap,Product,Supplier")]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        ShopEntityDb db =new ShopEntityDb();
        public ActionResult Index()
        {
            var email = Request.Cookies["Email"].Value;
            Account account = db.Accounts.FirstOrDefault(n => n.Email == email);
            ViewBag.name = account.Name;
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}