using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthTest.Views.Home;
using DLL;
using DLL.Entities;

namespace AuthTest.Controllers {
    public class HomeController : Controller {
        private readonly IManager<Movie> _movieManager = new DllFacade().GetMovieManager();
        private readonly IManager<Order> _orderManager = new DllFacade().GetOrderManager();
        private readonly IManager<PromoCode> _promoCodeManager = new DllFacade().GetPromoCodeManager();

        // GET: Movie
        public ActionResult Index() {
            return View(_movieManager.Read());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = _movieManager.Read(id.Value);
            if (movie == null) {
                return HttpNotFound();
            }
            return View(movie);
        }

        [Authorize]
        public ActionResult BuyPage(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = _movieManager.Read(id.Value);
            if (movie == null) {
                return HttpNotFound();
            }

            var buyPageViewModel = new BuyPageViewModel {Movie = movie, ErrorString = ""};

            return View(buyPageViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult BuyPage([Bind(Include = "Id, FirstName, LastName, Email, Address")] Customer customer,
            [Bind(Include = "Id, Title, Genre, ImageUrl, TrailerUrl, Year, Price")] Movie movie, string promoCode) {
            if (ModelState.IsValid) {
                Order order;

                var allPromoCodes = _promoCodeManager.Read();
                if (promoCode.Equals("")) {
                    order = new Order {Customer = customer, Movie = movie};
                } else {
                    var promoFound = allPromoCodes.FirstOrDefault(x => x.Code == promoCode);
                    order = new Order {Customer = customer, Movie = movie, PromoCode = promoFound};

                    if (promoFound != null && !promoFound.IsValid) {
                        return View(new BuyPageViewModel {Movie = movie, Customer = customer, PromoCode = promoCode, ErrorString = "The promo code is invalid"});
                    } else if (promoFound == null) {
                        return View(new BuyPageViewModel { Movie = movie, Customer = customer, PromoCode = promoCode, ErrorString = "The promo code does not exist"});
                    }
                }

                _orderManager.Create(order);

                return View("ThankYou");
            }
            return View(new BuyPageViewModel {Movie = movie, Customer = customer, ErrorString = ""});
        }
    }
}