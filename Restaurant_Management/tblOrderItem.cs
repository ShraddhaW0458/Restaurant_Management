//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Restaurant_Management
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblOrderItem
    {
        public int Id { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<int> MenuId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> ItemPrice { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public string Status { get; set; }
        public string Portion { get; set; }

        // Navigation properties
        public virtual tblMenu tblMenu { get; set; }
        public virtual tblOrder tblOrder { get; set; }

    }
}
