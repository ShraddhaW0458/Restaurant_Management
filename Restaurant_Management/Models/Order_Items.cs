using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Models
{
    public class Order_Items
    {
        public int Id { get; set; }
        public Nullable<int> MenuId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> ItemPrice { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public string Status { get; set; }
        public string Portion { get; set; }
        public Nullable<int> OrderId { get; set; }

        // Navigation properties
        public virtual tblMenu tblMenu { get; set; }
        public virtual tblOrder tblOrder { get; set; }
    }
}