using PagedList;
using ShopWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShopWebApplication.Services.Interface
{
    interface IAccount
    {
        IPagedList<Account> GetListAccount(int? page, string search);
        bool NewAccount(Account account);
        bool EditAccount(Account model);
        bool DeleteAccount(int id);
        Account EditPassword(FormCollection model);
    }
}
