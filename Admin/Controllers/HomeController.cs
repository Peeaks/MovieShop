using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Admin.Views.Movies;
using DLL;
using DLL.Entities;

namespace Admin.Controllers {
    public class HomeController : Controller {
        private IManager<Movie, int> MovieManager => new DllFacade().GetMovieManager();
        private IManager<Genre, int> GenreManager => new DllFacade().GetGenreManager();


        // GET: Movies
        public ActionResult Index() {
            return View(MovieManager.Read());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = MovieManager.Read(id.Value);
            if (movie == null) {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create() {
            var viewModel = new CreateMovieViewModel { Genres = GenreManager.Read() };
            return View(viewModel);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Year,Price,ImageUrl,TrailerUrl, Genre")] Movie movie) {
            if (ModelState.IsValid) {
                MovieManager.Create(movie);
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = MovieManager.Read(id.Value);
            if (movie == null) {
                return HttpNotFound();
            }
            var viewModel = new EditMovieViewModel { Genres = GenreManager.Read(), Movie = movie };
            return View(viewModel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Year,Price,ImageUrl,TrailerUrl, Genre")] Movie movie) {
            if (ModelState.IsValid) {
                MovieManager.Update(movie);
                return RedirectToAction("Index");
            }
            var viewModel = new EditMovieViewModel { Genres = GenreManager.Read(), Movie = movie };
            return View(viewModel);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = MovieManager.Read(id.Value);
            if (movie == null) {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            MovieManager.Delete(id);
            return RedirectToAction("Index");
        }
    }
}