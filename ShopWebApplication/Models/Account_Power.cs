//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account_Power
    {
        public int IDAccountType { get; set; }
        public string IDPower { get; set; }
        public string Note { get; set; }
    
        public virtual Account_type Account_type { get; set; }
        public virtual Power Power { get; set; }
    }
}
