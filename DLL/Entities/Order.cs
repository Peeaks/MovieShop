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

        public double TotalPrice {
            get {
                if (this.PromoCode != null) {
                    double discount = this.Movie.Price * this.PromoCode.Discount * 0.01;
                    return this.Movie.Price - discount;
                } else {
                    return this.Movie.Price;
                }
            }
        }
    }
}
