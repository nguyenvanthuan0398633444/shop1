using PagedList;
using ShopWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebApplication.Services.Interface
{
    interface ICustomer
    {
        IPagedList<Customer> GetListCustomer(int? page, string search);
        bool NewCustomer(Customer product);
        bool EditCustomer(Customer model);
        bool DeleteCustomer(int id);
    }
}
