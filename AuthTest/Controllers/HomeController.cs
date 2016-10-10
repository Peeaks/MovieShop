using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AuthTest.Models;
using DLL;
using DLL.Entities;
using DLL.Managers;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AuthTest.Controllers {
    public class HomeController : Controller {
        private readonly IManager<Movie, int> _movieManager = new DllFacade().GetMovieManager();
        private readonly IManager<Order, int> _orderManager = new DllFacade().GetOrderManager();
        private readonly IManager<Genre, int> _genreManager = new DllFacade().GetGenreManager();
        private readonly IManager<PromoCode, string> _promoCodeManager = new DllFacade().GetPromoCodeManager();

        private readonly IManager<ApplicationUser, string> _applicationUserManager =
            new DllFacade().GetApplicationUserManager();

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

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

            return
                View(new HomeSearchViewModel {HomeIndexViewModel = GetPage(returnMovies, page.Value), Search = search});
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
            return new HomeIndexViewModel {
                Movies = returnMovies,
                MaxPages = (movies.Count + 9)/12,
                CurrentPage = page,
                Genres = _genreManager.Read()
            };
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
        public ActionResult BuyPage() {
            Cart cart = CartManager.GetCartManager(this.HttpContext).GetCart();

            if (ModelState.IsValid) {
                Order order;

                if (cart.PromoCode == null) {
                    order = new Order {
                        ApplicationUser = _applicationUserManager.Read(User.Identity.GetUserId()),
                        Movies = cart.Movies
                    };
                } else {
                    order = new Order {
                        ApplicationUser = _applicationUserManager.Read(User.Identity.GetUserId()),
                        Movies = cart.Movies, PromoCode = cart.PromoCode
                    };
                }
                return View("Confirm", order);
            }
            return RedirectToAction("Index", "Cart");
        }

        [Authorize]
        public async Task<ActionResult> Confirm(Order order) {
            var cartManager = CartManager.GetCartManager(this.HttpContext);
            cartManager.EmptyCart();


            _orderManager.Create(order);

            const string subject = "Order receipt from Movie Shop";
            var body = new EmailTemplate.EmailTemplate().Receipt(order);

            await UserManager.SendEmailAsync(User.Identity.GetUserId(), subject, body);

            return View("ThankYou", order);
        }
    }
}