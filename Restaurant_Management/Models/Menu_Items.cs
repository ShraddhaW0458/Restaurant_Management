using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Models
{
    public class Menu_Items
    {
        public int Id { get; set; }

        
        public string ItemName { get; set; }

        
        public string Category { get; set; }

       
        public string Description { get; set; }

        
        public string Availability { get; set; }

       
        public float FullPrice { get; set; }

        
        public float HalfPrice { get; set; }
    }
}