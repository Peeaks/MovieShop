using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities {
    public class Address : AbstractEntity {
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}