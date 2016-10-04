using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities {
    class PromoCode : AbstractEntity{
        public string Code { get; set; }
        public int Discount { get; set; }
    }
}
