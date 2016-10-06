using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthTest.Models;
using AuthTest.Views.Home;
using DLL;
using DLL.Entities;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AuthTest.Controllers {
    public class HomeController : Controller {
        private readonly IManager<Movie, int> _movieManager = new DllFacade().GetMovieManager();
        private readonly IManager<Order, int> _orderManager = new DllFacade().GetOrderManager();
        private readonly IManager<PromoCode, int> _promoCodeManager = new DllFacade().GetPromoCodeManager();
        private readonly IManager<ApplicationUser, string> _applicationUserManager = new DllFacade().GetApplicationUserManager();

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

            ApplicationUser user = _applicationUserManager.Read(User.Identity.GetUserId());

            var buyPageViewModel = new BuyPageViewModel {ApplicationUser = user, Movie = movie, ErrorString = ""};

            return View(buyPageViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult BuyPage(
            [Bind(Include = "Id, FirstName, LastName, Email, Address")] ApplicationUser applicationUser,
            [Bind(Include = "Id, Title, Genre, ImageUrl, TrailerUrl, Year, Price")] Movie movie,
            [Bind(Include = "Code")] PromoCode promoCode) {
            if (ModelState.IsValid) {
                Order order;

                var allPromoCodes = _promoCodeManager.Read();
                if (promoCode.Code.IsNullOrWhiteSpace()) {
                    order = new Order {ApplicationUser = applicationUser, Movie = movie};
                } else {
                    var promoFound = allPromoCodes.FirstOrDefault(x => x.Code == promoCode.Code);
                    if (promoFound != null && !promoFound.IsValid) {
                        return
                            View(new BuyPageViewModel {
                                Movie = movie,
                                ApplicationUser = applicationUser,
                                PromoCode = promoCode,
                                ErrorString = "The promo code is invalid"
                            });
                    } else if (promoFound == null) {
                        return
                            View(new BuyPageViewModel {
                                Movie = movie,
                                ApplicationUser = applicationUser,
                                PromoCode = promoCode,
                                ErrorString = "The promo code does not exist"
                            });
                    }
                    order = new Order {ApplicationUser = applicationUser, Movie = movie, PromoCode = promoFound};
                }

                //_orderManager.Create(order);
                if (order.PromoCode != null) {
                    double discount = order.Movie.Price * order.PromoCode.Discount * 0.01;
                    order.Movie.Price = order.Movie.Price - discount;
                }
                return View("Confirm", order);
            }
            return View(new BuyPageViewModel {Movie = movie, ApplicationUser = applicationUser, ErrorString = ""});
        }

        [Authorize]
        [HttpPost]
        public ActionResult Confirm(Order order)
        {

            _orderManager.Create(order);

            return View("ThankYou");
        }
    }
}