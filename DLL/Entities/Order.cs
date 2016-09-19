using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities {
    class Order {
        public int Id { get; set; }
        public List<Movie> Movies { get; set; }
        public Customer Customer { get; set; }
        public DateTime Date { get; set; }
    }
}
