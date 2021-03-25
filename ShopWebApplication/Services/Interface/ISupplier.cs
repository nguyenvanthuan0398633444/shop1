using PagedList;
using ShopWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebApplication.Services.Interface
{
    interface ISupplier
    {
        IPagedList<Supplier> GetListSupplier(int? page, string search);
        void NewSupplier(Supplier supplier);
        bool EditSupplier(Supplier model);
        bool DeleteSupplier(int id);
    }
}
