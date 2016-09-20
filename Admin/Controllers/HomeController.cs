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
            _genreManager.Create(new Genre() {Name = "Banan"});
            return View(_genreManager.Read());
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