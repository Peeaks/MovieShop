using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DLL;
using DLL.Contexts;
using DLL.Entities;

namespace Admin.Controllers {
    public class OrdersController : Controller {
        private IManager<Order, int> OrderManager => new DllFacade().GetOrderManager();


        // GET: Orders
        public ActionResult Index() {
            return View(OrderManager.Read());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = OrderManager.Read(id.Value);
            if (order == null) {
                return HttpNotFound();
            }
            return View(order);
        }
    }
}