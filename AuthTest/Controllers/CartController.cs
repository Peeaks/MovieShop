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
        private readonly IManager<Movie, int> _movieManager = new DllFacade().GetMovieManager();
        private readonly IManager<PromoCode, int> _promoManager = new DllFacade().GetPromoCodeManager();

        public ActionResult Index() {
            var cartManager = CartManager.GetCartManager(this.HttpContext);
            var cart = cartManager.GetCart();
            if (cart == null) {
                cart = new Cart {Movies = new List<Movie>(), PromoCode = new PromoCode()};
            }
            return View(cart);
        }

        [ValidateAntiForgeryToken]
        public ActionResult AddPromoCode(string promocode) {

            var cartManager = CartManager.GetCartManager(this.HttpContext);
            cartManager.AddPromoToCart(promocode);

            return RedirectToAction("Index");
        }


        public ActionResult AddToCart(int id) {
            // Retrieve the movie from the database
            var movie = _movieManager.Read(id);

            // Add it to the shopping cart
            var cart = CartManager.GetCartManager(this.HttpContext);

            cart.AddToCart(movie);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult RemoveFromCart(int id) {
            // Remove the item from the cart
            var cart = CartManager.GetCartManager(this.HttpContext);

            // Get the name of the movie to display confirmation
            string movieName = _movieManager.Read(id).Title;

            // Remove from cart
            cart.RemoveFromCart(id);

            // Display the confirmation message
            var message = movieName + " has been removed from your shopping cart.";
            return RedirectToAction("Index");
        }

    }
}