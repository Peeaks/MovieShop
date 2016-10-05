using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Admin.Views.PromoCodes;
using DLL;
using DLL.Contexts;
using DLL.Entities;

namespace Admin.Controllers {
    public class PromoCodesController : Controller {
        private readonly IManager<PromoCode> _promoCodeManager = new DllFacade().GetPromoCodeManager();

        // GET: PromoCodes
        public ActionResult Index() {
            return View(_promoCodeManager.Read());
        }

        // GET: PromoCodes/Create
        public ActionResult Create() {
            return View();
        }

        // POST: PromoCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Discount, IsValid")] PromoCode promoCode) {
            if (ModelState.IsValid) {
                _promoCodeManager.Create(promoCode);
                return RedirectToAction("Index");
            }

            return View(promoCode);
        }

        // GET: PromoCodes/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromoCode promoCode = _promoCodeManager.Read(id.Value);
            if (promoCode == null) {
                return HttpNotFound();
            }
            return View(promoCode);
        }

        // POST: PromoCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Discount, IsValid")] PromoCode promoCode) {
            if (ModelState.IsValid) {
                _promoCodeManager.Update(promoCode);
                return RedirectToAction("Index");
            }
            return View(promoCode);
        }

        // GET: PromoCodes/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromoCode promoCode = _promoCodeManager.Read(id.Value);
            if (promoCode == null) {
                return HttpNotFound();
            }
            return View(new PromoCodeDeleteViewModel {PromoCode = promoCode, ShitWentWrong = false});
        }

        // POST: PromoCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            try {
                _promoCodeManager.Delete(id);
            } catch (SqlException) {
                return View(new PromoCodeDeleteViewModel {PromoCode = _promoCodeManager.Read(id), ShitWentWrong = true});
            }
            return RedirectToAction("Index");
        }
    }
}