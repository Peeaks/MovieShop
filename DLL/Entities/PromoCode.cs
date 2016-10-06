using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities {
    public class PromoCode : AbstractEntity{
        [Display (Name = "Promo code")]
        public string Code { get; set; }
        public int Discount { get; set; }
        public bool IsValid { get; set; }
    }
}
