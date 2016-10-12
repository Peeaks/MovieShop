using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
using DLL.Entities;
using DLL.Managers;

namespace AuthTest.Controllers {
    public class CartController : Controller {
        private IManager<Movie, int> MovieManager => new DllFacade().GetMovieManager();
        private ICartManager CartManager => new DllFacade().GetCartManager(this.HttpContext);


        public ActionResult Index() {
            var cart = CartManager.Read();
            if (cart == null) {
                cart = new Cart {Movies = new List<Movie>(), PromoCode = new PromoCode()};
            }
            return View(cart);
        }

        [ValidateAntiForgeryToken]
        public ActionResult AddPromoCode(string promocode) {
            CartManager.AddPromoToCart(promocode);

            return RedirectToAction("Index");
        }


        public ActionResult AddToCart(int id) {
            var movie = MovieManager.Read(id);

            CartManager.AddToCart(movie);

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult RemoveFromCart(int id) {
            string movieName = MovieManager.Read(id).Title;

            CartManager.RemoveFromCart(id);

            var message = movieName + " has been removed from your shopping cart.";
            return RedirectToAction("Index");
        }

    }
}