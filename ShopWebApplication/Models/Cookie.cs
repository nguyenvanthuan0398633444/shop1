using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWebApplication.Models
{

    public class Cookie
    {
        public int id { get; set; }
        public string name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhotoPath { get; set; }
        public Cookie()
        {

        }
        public Cookie(string email)
        {
            using(ShopEntityDb db = new ShopEntityDb())
            {
                this.Email = email;
                Account account = db.Accounts.FirstOrDefault(n => n.Email == email);
                this.id = account.AccountID;
                this.name = account.Name;
                this.Password = account.Password;
                this.PhotoPath = account.PhotoPath;
            }
         
        }
    }
    //<add name="ShopEntityDb" connectionString="metadata=res://*/Models.ShopEntityDb.csdl|res://*/Models.ShopEntityDb.ssdl|res://*/Models.ShopEntityDb.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=AITD201506008\SQLEXPRESS;initial catalog=Shop;persist security info=True;user id=sa;password=12345;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" /></connectionStrings>
}