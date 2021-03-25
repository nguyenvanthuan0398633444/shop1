using PagedList;
using ShopWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebApplication.Services.Interface
{
    interface IEmployee
    {
        IPagedList<Employee> GetListEmployee(int? page, string search);
        void NewEmployee(Employee employee);
        bool EditEmployee(Employee model);
        bool DeleteEmployee(int id);
    }
}
