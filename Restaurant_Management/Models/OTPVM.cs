using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Models
{
    public class OTPVM
    {
        [Required]
        [Display(Name ="Enter OTP:")]
        public int OTP { set; get; }
    }
}