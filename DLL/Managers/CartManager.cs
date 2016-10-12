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
    class CartManager : ICartManager {

        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public CartManager(HttpContextBase httpContext) {
            ShoppingCartId = GetCartId(httpContext);
        }

        public Cart Read() {
            using (var db = new MovieShopContext()) {
                return db.Carts.Include(x => x.Movies).Include(x => x.PromoCode).FirstOrDefault(x => x.Id == ShoppingCartId);
            }
        }

        public void AddPromoToCart(string promo) {
            using (var db = new MovieShopContext()) {
                var promoFound = db.PromoCodes.FirstOrDefault(x => x.Code == promo);
                if (promoFound != null) {
                    var cart = db.Carts.Include(x => x.Movies).Include(x => x.PromoCode).FirstOrDefault(x => x.Id == ShoppingCartId);
                    cart.PromoCode = promoFound;
                    db.SaveChanges();
                }
            }
        }

        public void AddToCart(Movie movie) {
            using (var db = new MovieShopContext()) {

                var movieInDb = db.Movies.Include(x => x.Genre).FirstOrDefault(x => x.Id == movie.Id);

                var cartItem = db.Carts.SingleOrDefault(c => c.Id == ShoppingCartId);

                if (cartItem == null) {
                    cartItem = new Cart {
                        Id = ShoppingCartId,
                        Movies = new List<Movie>()
                    };
                    cartItem.Movies.Add(movieInDb);
                    db.Carts.Add(cartItem);
                } else {
                    cartItem.Movies.Add(movieInDb);
                }
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
                cart.Movies.Clear();
                cart.PromoCode = null;
                // Save changes
                db.SaveChanges();
            }
        }

        private string GetCartId(HttpContextBase context) {
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
        
        public void MigrateCart(string userName) {
            using (var db = new MovieShopContext()) {
                var shoppingCart = db.Carts.Single(c => c.Id == ShoppingCartId);

                shoppingCart.Id = userName;

                db.SaveChanges();
            }
        }

    }
}
