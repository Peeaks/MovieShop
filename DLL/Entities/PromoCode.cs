using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities {
    public class PromoCode : AbstractEntity{
        public string Code { get; set; }
        public int Discount { get; set; }
        public bool IsValid { get; set; }
    }
}
