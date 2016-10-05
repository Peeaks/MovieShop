using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities {
    public class Order : AbstractEntity {
        public Movie Movie { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime Time { get; set; }
        public PromoCode PromoCode { get; set; }
    }
}
