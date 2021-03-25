using PagedList;
using ShopWebApplication.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopWebApplication.Models;
namespace ShopWebApplication.Services
{
    public class EmployeeServises : IEmployee

    {
        ShopEntityDb db = new ShopEntityDb();
        public bool DeleteEmployee(int id)
        {
            Employee model = db.Employees.SingleOrDefault(n => n.EmployeeID == id);
            if (model == null)
            {
                return false;
            }
            db.Employees.Remove(model);
            db.SaveChanges();
            return true;
        }

        public bool EditEmployee(Employee model)
        {
            Employee employee = db.Employees.FirstOrDefault(n => n.EmployeeID == model.EmployeeID);
            employee.Email = model.Email;
            employee.EmployeeName = model.EmployeeName;
            employee.Address = model.Address;
            employee.Password = model.Password;
            employee.phone = model.phone;
            if(model.BirthDate != null)
            {
                employee.BirthDate = model.BirthDate;
            }
            db.SaveChanges();
            return true;
        }

        public IPagedList<Employee> GetListEmployee(int? page, string search)
        {
            List<Employee> list;
            if (search == null)
            {
                list = db.Employees.ToList();
            }
            else
            {
                list = db.Employees.Where(n => n.EmployeeName.Contains(search)).ToList();
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return list.OrderBy(n => n.EmployeeID).ToPagedList(pageNumber, pageSize);
        }

        public void NewEmployee(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }
    }
}