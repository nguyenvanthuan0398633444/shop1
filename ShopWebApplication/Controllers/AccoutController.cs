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
using System.Web.Security;


namespace ShopWebApplication.Controllers
{
   
    public class AccoutController : Controller
    {
        AccountServises accountServises = new AccountServises();
        ShopEntityDb db = new ShopEntityDb();
        [Authorize(Roles = "Account")]
        public ActionResult Index(int? page, string search)
        {
            return View(accountServises.GetListAccount(page, search));
        }
        [Authorize(Roles = "Account")]
        [HttpGet]
        public ActionResult NewAccount()
        {
            ViewBag.AccountType = db.Account_types;
            return View();
        }
        
        [ValidateInput(false)]
        [Authorize(Roles = "Account")]
        [HttpPost]
        public ActionResult NewAccount(Account accountNew, HttpPostedFileBase photoPath)
        {
            accountNew.PhotoPath= Photo(photoPath);
            if ( accountServises.NewAccount(accountNew)==true)
            {                
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Email đã đăng kí";
                 return View();
            }          
        }
        [Authorize(Roles = "Account")]
        [HttpGet]
        public ActionResult EditAccount(int id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Account pd = db.Accounts.SingleOrDefault(n => n.AccountID == id);
            if (pd == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountType = db.Account_types;
            return View(pd);
        }
        
        [HttpPost]
        public ActionResult EditAccount(Account model, HttpPostedFileBase photoPath)
        {
            model.PhotoPath = Photo(photoPath);
            if (ModelState.IsValid)
            {
                accountServises.EditAccount(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize(Roles = "Account")]
        [HttpGet]
        public ActionResult DeleteAccount(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (accountServises.DeleteAccount(id) == true)
            {
                return RedirectToAction("Index");               
            }
            return HttpNotFound();
        }
        public string Photo(HttpPostedFileBase photoPath)
        {
            if (photoPath != null)
            {
                //lay ten anh
                var fileName = Path.GetFileName(photoPath.FileName);
                // lay anh chuyen vao thu muc
                var path = Path.Combine(Server.MapPath("~/Content/Image"), fileName);
                photoPath.SaveAs(path);
                return fileName.ToString();
            }
            Response.StatusCode = 404;
            return null;
        }
        [Authorize(Roles = "Account,Product")]
        public ActionResult Profile(int id)
        {
            Account account = db.Accounts.FirstOrDefault(n => n.AccountID == id);
            Account_type type = db.Account_types.FirstOrDefault(n => n.ID == account.IDAccountType);
            ViewBag.type = type.Name.ToString();
            return View(account);
        }
        [Authorize(Roles = "Account,Product")]
        [HttpGet]
        public ActionResult EditUser(int id)
        {
            Account account = db.Accounts.FirstOrDefault(n => n.AccountID == id);
            return View(account);
        }
        [Authorize(Roles = "Account,Product")]
        [HttpPost]
        public ActionResult EditUser(Account model, HttpPostedFileBase photoPath)
        {
            model.PhotoPath = Photo(photoPath);
            if (ModelState.IsValid)
            {
                accountServises.EditAccount(model);
                return RedirectToAction("Profile/"+ model.AccountID);
            }
            return View(model);
        }
        [Authorize(Roles = "Account,Product")]
        [HttpGet]
        public ActionResult EditPassword()
        {
            var email = Request.Cookies["Email"].Value;
            Account account = db.Accounts.FirstOrDefault(n => n.Email == email);
            return View(account);
        }
        [Authorize(Roles = "Account,Product")]
        [HttpPost]
        public ActionResult EditPassword(FormCollection model)
        {
            string currentPassword = model["currentPassword"].ToString();
            string newPassword = model["newPassword"].ToString();
            string confirmPassword = model["confirmPassword"].ToString();
            int id = Int32.Parse(model["AccountID"]);
            Account account = db.Accounts.FirstOrDefault(n => n.AccountID == id);
            if(account.Password == currentPassword)
            {
                if(newPassword == confirmPassword)
                {
                    account.Password = newPassword;
                    db.SaveChanges();
                    return RedirectToAction("Profile/"+ id);
                }
                else
                {
                    ViewBag.newPass = "Mật khẩu không khớp";
                    return View(account);
                }
            }
            ViewBag.pass = "Mật khẩu không đúng";
            return View(account);
        }
    }
}
       
    