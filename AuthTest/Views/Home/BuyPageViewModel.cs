using System.ComponentModel.DataAnnotations.Schema;
using DLL.Entities;

namespace AuthTest.Views.Home {
    [NotMapped]
    public class BuyPageViewModel {
        public Cart Cart { get; set; }
        public string ErrorString { get; set; }
    }
}