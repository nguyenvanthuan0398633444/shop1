using PagedList;
using ShopWebApplication.Models;
using ShopWebApplication.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ShopWebApplication.Models;

namespace ShopWebApplication.Services
{
    public class AccountServises : IAccount
    {
        ShopEntityDb db = new ShopEntityDb();
        public bool DeleteAccount(int id)
        {
            Account model = db.Accounts.SingleOrDefault(n => n.AccountID == id);
            if (model == null)
            {
                return false;
            }
            db.Accounts.Remove(model);
            db.SaveChanges();
            return true;
        }

        public bool EditAccount(Account model)
        {
            if(model != null)
            {
                Account accounts = db.Accounts.FirstOrDefault(n => n.AccountID == model.AccountID);
                if(model.Name != null)
                {
                    accounts.Name = model.Name;
                }
                if (model.Password != null)
                {
                    accounts.Password = model.Password;
                }
                if (model.Email != null)
                {
                    accounts.Email = model.Email;
                }
                if (model.IDAccountType != null)
                {
                    accounts.IDAccountType = model.IDAccountType;
                }             
                if (model.PhotoPath != null)
                {
                    accounts.PhotoPath = model.PhotoPath;
                }              
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public Account EditPassword(FormCollection model)
        {
            throw new NotImplementedException();
        }

        public IPagedList<Account> GetListAccount(int? page, string search)
        {
            List<Account> list;
            if (search == null)
            {
                list = db.Accounts.ToList();
            }
            else
            {
                list = db.Accounts.Where(n => n.Email.Contains(search)).ToList();
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return list.OrderBy(n => n.Email).ToPagedList(pageNumber, pageSize);
        }

        public bool NewAccount(Account account)
        {
            Account acc = db.Accounts.FirstOrDefault(n => n.Email == account.Email);
            if (acc == null)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}