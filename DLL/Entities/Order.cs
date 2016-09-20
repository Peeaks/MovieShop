using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities {
    public class Order : AbstractEntity {
        public virtual List<Movie> Movies { get; set; }
        public virtual Customer Customer { get; set; }
        //public DateTime Date { get; set; }
    }
}
