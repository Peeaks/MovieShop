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
        private readonly IManager<PromoCode, string> _promoManager = new DllFacade().GetPromoCodeManager();

        // GET: /ShoppingCart/
        public ActionResult Index() {
            var cartManager = CartManager.GetCartManager(this.HttpContext);
            var cart = cartManager.GetCart();
            if (cart == null) {
                cart = new Cart {Movies = new List<Movie>(), PromoCode = new PromoCode()};
            }
            // Return the view
            return View(cart);
        }

        public ActionResult AddPromoCode(string promocode) {

            var cartManager = CartManager.GetCartManager(this.HttpContext);
            cartManager.AddPromoToCart(promocode);

            return RedirectToAction("Index");
        }


        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id) {
            // Retrieve the movie from the database
            var movie = _movieManager.Read(id);

            // Add it to the shopping cart
            var cart = CartManager.GetCartManager(this.HttpContext);

            cart.AddToCart(movie);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
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

        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary() {
            var cart = CartManager.GetCartManager(this.HttpContext);

            ViewData["CartCount"] = cart.GetCartItems().Count;
            return PartialView("CartSummary");
        }
    }
}