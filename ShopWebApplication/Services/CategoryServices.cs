using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopWebApplication.Models;
using PagedList;
using ShopWebApplication.Services.Interfaces;

namespace ShopWebApplication.Services
{
    public class CategoryServices : ICategory
    {
        ShopEntityDb db = new ShopEntityDb();
        public IPagedList<Category> GetListCategory(int? page, string search)
        {
            List<Category> list;
            if (search == null)
            {
                list = db.Categories.ToList();
            }
            else
            {
                list = db.Categories.Where(n => n.CategoryName.Contains(search)).ToList();
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return list.OrderBy(n => n.CategoryID).ToPagedList(pageNumber, pageSize);
        }

        public Boolean NewCategory(Category category)
        {  
            if(category != null)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public Boolean DeleteCategory(int id)
        {
            if (id == null)
            {
                return false;
            }
            Category model = db.Categories.SingleOrDefault(n => n.CategoryID == id);
            if (model == null)
            {
                return false;
            }
            db.Categories.Remove(model);
            db.SaveChanges();
            return true;
        }

        public Boolean EditCategory(Category model)
        {
            if (model != null)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }

    }
}