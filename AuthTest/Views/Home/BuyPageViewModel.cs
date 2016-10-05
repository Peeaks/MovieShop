using DLL.Entities;

namespace AuthTest.Views.Home {
    public class BuyPageViewModel {
        public Customer Customer { get; set; }
        public Movie Movie { get; set; }
        public string PromoCode { get; set; }
        public string ErrorString { get; set; }
    }
}