using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DLL.Entities;
using DLL.Managers;

namespace DLL {
    public class DllFacade {
        public IManager<Movie, int> GetMovieManager() {
            return new MovieManager();
        }
        public IManager<Genre, int> GetGenreManager() {
            return new GenreManager();
        }
        public IManager<Order, int> GetOrderManager() {
            return new OrderManager();
        }
        public IManager<PromoCode, int> GetPromoCodeManager() {
            return new PromoCodeManager();
        }
        public IManager<ApplicationUser, string> GetApplicationUserManager() {
            return new ApplicationUserManager();
        }

        public ICartManager GetCartManager(HttpContextBase httpContext) {
            return new CartManager(httpContext);
        }
    }
}