using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DLL.Entities;

namespace Admin.Views.PromoCodes {
    public class PromoCodeDeleteViewModel {
        public PromoCode PromoCode { get; set; }
        public bool ShitWentWrong { get; set; }
    }
}