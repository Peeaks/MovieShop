using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DLL.Entities {
    public class ApplicationUser : IdentityUser {
        [Display (Name = "First name")]
        public string FirstName { get; set; }
        [Display (Name = "Last name")]
        public string LastName { get; set; }
        public Address Address { get; set; }
    }
}
