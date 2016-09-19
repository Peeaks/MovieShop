using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities {
    class Address {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string Country { get; set; }
        public int ZipCode { get; set; }
        public Customer Customer { get; set; }
    }
}
