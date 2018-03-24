using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Shop.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(int.MaxValue, MinimumLength = 4)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(int.MaxValue, MinimumLength = 4)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 4)]
        public string Address { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 2)]
        public string LastName { get; set; }
    }
}