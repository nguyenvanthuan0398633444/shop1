using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PagedList;
using ShopWebApplication.Models;

namespace ShopWebApplication.Services.Interface
{
    interface IProduct
    {
        IPagedList<Product> GetListProduct(int? page, string search);
        Boolean NewProduct(Product product);
        Boolean EditProduct(Product model);
        Boolean DeleteProduct(int id);
    }
}
