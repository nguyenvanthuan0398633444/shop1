using PagedList;
using ShopWebApplication.Models;
using ShopWebApplication.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopWebApplication.Models;

namespace ShopWebApplication.Services
{
    public class CustomerServices : ICustomer
    {
        ShopEntityDb db = new ShopEntityDb();
        public bool DeleteCustomer(int id)
        {
            Customer model = db.Customers.SingleOrDefault(n => n.CustomerID == id);
            if (model == null)
            {
                return false;
            }
            db.Customers.Remove(model);
            db.SaveChanges();
            return true;
        }

        public bool EditCustomer(Customer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public IPagedList<Customer> GetListCustomer(int? page, string search)
        {
            List<Customer> list;
            if (search == null)
            {
                list = db.Customers.ToList();
            }
            else
            {
                list = db.Customers.Where(n => n.CustomerName.Contains(search)).ToList();
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return list.OrderBy(n => n.CustomerID).ToPagedList(pageNumber, pageSize);
        }

        public bool NewCustomer(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            return true;
        }
    }
}