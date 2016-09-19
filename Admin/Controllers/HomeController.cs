using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
using DLL.Entities;

namespace Admin.Controllers {
    public class HomeController : Controller {
        private readonly IManager<Genre> _genreManager = new DllFacade().GetGenreManager();

        public ActionResult Index() {
            var allGenres = _genreManager.Read();
            foreach (var genre in allGenres) {
                Console.WriteLine(genre.Name);
            }
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}