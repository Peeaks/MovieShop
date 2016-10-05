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

namespace Admin.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IManager<Order, int> _orderManager = new DllFacade().GetOrderManager();

        // GET: Orders
        public ActionResult Index()
        {
            return View(_orderManager.Read());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _orderManager.Read(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
    }
}
