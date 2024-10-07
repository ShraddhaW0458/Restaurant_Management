using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Models
{
    public class Tables
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string TableNumber { get; set; }

        [Required]
        public Nullable<int> Seats { get; set; }

        [Required]
        public string Discription { get; set; }

        [Required] 
        [Display(Name ="Branch Name:")]
        public Nullable<int> BranchId { get; set; }
    }
}