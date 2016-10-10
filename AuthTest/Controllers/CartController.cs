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

        // GET: /ShoppingCart/
        public ActionResult Index() {
            var cart = CartManager.GetCartManager(this.HttpContext);

            // Return the view
            return View(cart.GetCart());
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
            return View("Index", cart.GetCart());
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