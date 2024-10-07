using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Customer Name:")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "The mobile number must be exactly 10 digits.")]
        public string MobileNo { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public Nullable<int> CountryId { get; set; }

        [Required]
        public Nullable<int> StateId { get; set; }

        [Required]
        public Nullable<int> CityId { get; set; }

        public string TableNumber { set; get; }
    }
}