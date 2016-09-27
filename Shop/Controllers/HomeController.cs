using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
using DLL.Entities;

namespace Shop.Controllers {
    public class HomeController : Controller {

        private readonly IManager<Movie> _movieManager = new DllFacade().GetMovieManager();

        public ActionResult Index() {
            return View(_movieManager.Read());
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