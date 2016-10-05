using System.ComponentModel.DataAnnotations.Schema;
using DLL.Entities;

namespace AuthTest.Views.Home {
    [NotMapped]
    public class BuyPageViewModel {
        public ApplicationUser ApplicationUser { get; set; }
        public Movie Movie { get; set; }
        public PromoCode PromoCode { get; set; }
        public string ErrorString { get; set; }
    }
}