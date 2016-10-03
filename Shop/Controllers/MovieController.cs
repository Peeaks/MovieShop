using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DLL;
using DLL.Entities;

namespace Shop.Controllers {
    public class MovieController : Controller {
        private readonly IManager<Movie> _movieManager = new DllFacade().GetMovieManager();

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
    }
}