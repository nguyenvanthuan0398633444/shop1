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
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {

        EmployeeServises employee = new EmployeeServises();
        ShopEntityDb db =new ShopEntityDb();

        // GET: Employee
        public ActionResult Index(int? page, string seach)
        {
            return View(employee.GetListEmployee(page,seach));
        }
        [HttpGet]
        public ActionResult NewEmployee()
        {
            return View();
        }
        [ValidateInput(false)]// tắt kiểm tra dữ liệu đầu vào
        [HttpPost]
        public ActionResult NewEmployee(Employee employeeNew)
        {
            employee.NewEmployee(employeeNew);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditEmployee(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Employee pd = db.Employees.SingleOrDefault(n => n.EmployeeID == id);
            if (pd == null)
            {
                return HttpNotFound();
            }

            return View(pd);
        }
        [HttpPost]
        public ActionResult EditEmployee(Employee model)
        {
            if (ModelState.IsValid)
            {
                employee.EditEmployee(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteEmployee(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (employee.DeleteEmployee(id)==false)
            {
                return HttpNotFound();
            }
            employee.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}