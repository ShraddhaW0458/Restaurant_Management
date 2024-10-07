using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Models
{
    public class User
    {
        public int Id { set; get; }

        [Required]
        public string UserName { set; get; }

        [Required]
        [Display(Name = "Enter Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { set; get; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Re-Enter Password")]
        public string RePassword { set; get; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }

        [Required]
        public string FullName { set; get; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please Enter 10 Digit No.")]
        public string MobileNo { set; get; }

        [Required]
        public Nullable<int> UserRoleId { set; get; }

        [Required]
        public Nullable<int> BranchId { set; get; }
    }
}