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
using MailModule;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AuthTest.Controllers {
    public class HomeController : Controller {
        private readonly IManager<Movie, int> _movieManager = new DllFacade().GetMovieManager();
        private readonly IManager<Order, int> _orderManager = new DllFacade().GetOrderManager();
        private readonly IManager<Genre, int> _genreManager = new DllFacade().GetGenreManager();
        private readonly IManager<PromoCode, int> _promoCodeManager = new DllFacade().GetPromoCodeManager();

        private readonly IManager<ApplicationUser, string> _applicationUserManager =
            new DllFacade().GetApplicationUserManager();

        // GET: Movie
        public ActionResult Index(int? page) {
            var allMovies = _movieManager.Read();

            if (page == null) {
                page = 1;
            }

            return View(GetPage(allMovies, page.Value));
        }

        public ActionResult Search(int? page, string search) {

            if (search.IsNullOrWhiteSpace()) {
                return RedirectToAction("Index");
            }

            var allMovies = _movieManager.Read();

            if (page == null) {
                page = 1;
            }

            var returnMovies = new List<Movie>();

            foreach (var movie in allMovies) {
                if (movie.Title.ToLower().Contains(search.ToLower())) {
                    returnMovies.Add(movie);
                }
            }

            if (!returnMovies.Any()) {
                return View(new HomeSearchViewModel {Search = search, ErrorMessage = "No results were found"});
            }

            return View(new HomeSearchViewModel {HomeIndexViewModel = GetPage(returnMovies, page.Value), Search = search});
        }

        private HomeIndexViewModel GetPage(List<Movie> movies, int page) {
            var moviesPerPage = 12;
            var endIndex = page*moviesPerPage;

            var returnMovies = new List<Movie>();

            for (int i = endIndex - moviesPerPage; i < endIndex; i++) {
                if (i < movies.Count) {
                    returnMovies.Add(movies[i]);
                }
            }
            return new HomeIndexViewModel {Movies = returnMovies, MaxPages = (movies.Count + 9)/12, CurrentPage = page, Genres = _genreManager.Read()};
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
                    double discount = order.Movie.Price*order.PromoCode.Discount*0.01;
                    order.Movie.Price = order.Movie.Price - discount;
                }
                return View("Confirm", order);
            }
            return View(new BuyPageViewModel {Movie = movie, ApplicationUser = applicationUser, ErrorString = ""});
        }

        [Authorize]
        [HttpPost]
        public ActionResult Confirm(Order order) {
            _orderManager.Create(order);
            new Mailer().SendReceipt(order);

            return View("ThankYou", order);
        }
    }
}