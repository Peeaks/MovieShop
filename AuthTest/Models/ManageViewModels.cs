using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuthTest.Controllers;
using DLL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace AuthTest.Models {
    public class IndexViewModel {
        public ApplicationUser ApplicationUser { get; set; }
        public string Message { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class ChangePasswordViewModel {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeNameViewModel {
        [Required]
        [Display(Name = "New first name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "New last name")]
        public string LastName { get; set; }
    }

    public class ChangeAddressViewModel {
        [Required]
        [Display(Name = "Street")]
        public string StreetName { get; set; }

        [Required]
        [Display(Name = "Number")]
        public string StreetNumber { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
    }
}