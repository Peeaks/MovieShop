using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities {
    public class Address : AbstractEntity {
        [Required]
        [Display (Name = "Street")]
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