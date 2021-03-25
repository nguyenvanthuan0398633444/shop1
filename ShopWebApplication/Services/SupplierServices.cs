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
    public class SupplierServices : ISupplier
    {
        ShopEntityDb db = new ShopEntityDb();
        public bool DeleteSupplier(int id)
        {
            Supplier model = db.Suppliers.SingleOrDefault(n => n.SupplierID == id);
            if (model == null)
            {
                return false;
            }
            db.Suppliers.Remove(model);
            db.SaveChanges();
            return true;
        }

        public bool EditSupplier(Supplier model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public IPagedList<Supplier> GetListSupplier(int? page, string search)
        {
            List<Supplier> list;
            if (search == null)
            {
                list = db.Suppliers.ToList();
            }
            else
            {
                list = db.Suppliers.Where(n => n.SupplierName.Contains(search)).ToList();
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return list.OrderBy(n => n.SupplierID).ToPagedList(pageNumber, pageSize);

        }

        public void NewSupplier(Supplier supplier)
        {
            db.Suppliers.Add(supplier);
            db.SaveChanges();
        }
    }
}