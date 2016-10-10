using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DLL.Contexts;
using DLL.Entities;

namespace DLL.Managers {
    public class CartManager {

        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public static CartManager GetCartManager(HttpContextBase context) {
            var cart = new CartManager();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        // Helper method to simplify shopping cart calls
        public static CartManager GetCartManager(Controller controller) {
            return GetCartManager(controller.HttpContext);
        }

        public Cart GetCart() {
            using (var db = new MovieShopContext()) {
                return db.Carts.Include(x => x.Movies).Include(x => x.PromoCode).FirstOrDefault(x => x.Id == ShoppingCartId);
            }
        }

        public void AddToCart(Movie movie) {
            using (var db = new MovieShopContext()) {

                var movieInDb = db.Movies.Include(x => x.Genre).FirstOrDefault(x => x.Id == movie.Id);

                // Get the matching cart and album instances
                var cartItem = db.Carts.SingleOrDefault(c => c.Id == ShoppingCartId);

                if (cartItem == null) {
                    // Create a new cart item if no cart item exists
                    cartItem = new Cart {
                        Id = ShoppingCartId,
                        Movies = new List<Movie>()
                    };
                    cartItem.Movies.Add(movieInDb);
                    db.Carts.Add(cartItem);
                } else {
                    // If the cart does exist
                    cartItem.Movies.Add(movieInDb);
                }
                // Save changes
                db.SaveChanges();
            }
        }
        public void RemoveFromCart(int id) {
            using (var db = new MovieShopContext()) {
                // Get the cart
                var cart = db.Carts.Single(x => x.Id == ShoppingCartId);

                cart.Movies.RemoveAll(x => x.Id == id);

                db.SaveChanges();
            }
        }
        public void EmptyCart() {
            using (var db = new MovieShopContext()) {
                var cart = db.Carts.Single(x => x.Id == ShoppingCartId);
                cart.Movies = new List<Movie>();
                // Save changes
                db.SaveChanges();
            }
        }
        public List<Movie> GetCartItems() {
            using (var db = new MovieShopContext()) {
                return db.Carts.Single(x => x.Id == ShoppingCartId).Movies;
            }
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context) {
            if (context.Session[CartSessionKey] == null) {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name)) {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                } else {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName) {
            using (var db = new MovieShopContext()) {
                var shoppingCart = db.Carts.Single(c => c.Id == ShoppingCartId);

                shoppingCart.Id = userName;

                db.SaveChanges();
            }
        }

    }
}
