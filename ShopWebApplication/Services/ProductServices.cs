using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopWebApplication.Models;
using PagedList;
using ShopWebApplication.Services.Interface;

namespace ShopWebApplication.Services
{
    public class ProductServices : IProduct
    {
        ShopEntityDb db = new ShopEntityDb();
        public IPagedList<Product> GetListProduct(int? page, string search)
        {
            List<Product> list;
            if (search == null)
            {
                list = db.Products.ToList();
            }
            else
            {
                list = db.Products.Where(n => n.ProductName.Contains(search)).ToList();
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return list.OrderBy(n => n.ProductID).ToPagedList(pageNumber, pageSize);
        }
        public bool DeleteProduct(int id)
        {
            if (id == null)
            {
                return false;
            }
            Product model = db.Products.SingleOrDefault(n => n.ProductID == id);
            if (model == null)
            {
                return false;
            }
            db.Products.Remove(model);
            db.SaveChanges();
            return true;
        }

        public bool EditProduct(Product model)
        {
            if (model != null)
            {
                Product product = db.Products.FirstOrDefault(n => n.ProductID == model.ProductID);
                product.ProductName = model.ProductName;
                product.SupplierID = model.SupplierID;
                product.CategoryID = model.CategoryID;
                product.Descriptions = model.Descriptions;
                if (model.PhotoPath != null)
                {
                    product.PhotoPath = model.PhotoPath;
                }
                product.PurchasePrice = model.PurchasePrice;
                product.Price = model.Price;
                product.Amount = model.Amount;
                product.Sold = model.Sold;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool NewProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return true;
        }
    }
}