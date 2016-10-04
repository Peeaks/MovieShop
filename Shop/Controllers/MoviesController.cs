using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DLL;
using DLL.Entities;
using Shop.Views.Movies;

namespace Shop.Controllers {
    public class MoviesController : Controller {
        private readonly IManager<Movie> _movieManager = new DllFacade().GetMovieManager();
        private readonly IManager<Order> _orderManager = new DllFacade().GetOrderManager();
        private readonly IManager<Customer> _customerManager = new DllFacade().GetCustomerManager();

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

        public ActionResult BuyPage(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = _movieManager.Read(id.Value);
            if (movie == null) {
                return HttpNotFound();
            }

            var buyPageViewModel = new BuyPageViewModel {Movie = movie};

            return View(buyPageViewModel);
        }

        [HttpPost]
        public ActionResult BuyPage([Bind(Include = "Id, FirstName, LastName, Email, Address")] Customer customer,
            [Bind(Include = "Id, Title, Genre, ImageUrl, TrailerUrl, Year, Price")] Movie movie) {
            if (ModelState.IsValid) {
                var order = new Order {
                    Customer = customer,
                    Movie = movie
                };

                _orderManager.Create(order);

                return View("ThankYou");
            }
            return View(new BuyPageViewModel {Movie = movie, Customer = customer});
        }
    }
}