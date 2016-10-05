using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DLL.Contexts;
using DLL.Entities;

namespace DLL.Managers {
    internal class OrderManager : IManager<Order> {
        public Order Create(Order element) {
            using (var db = new MovieShopContext()) {

                element.Movie = db.Movies.Include(x => x.Genre).FirstOrDefault(x => element.Movie.Id == x.Id);
                if (element.PromoCode != null) {
                    element.PromoCode = db.PromoCodes.FirstOrDefault(x => element.PromoCode.Code == x.Code);
                }
                element.Time = DateTime.Now;
                db.Orders.Add(element);
                db.SaveChanges();

                return element;
            }
        }

        public Order Read(int id) {
            using (var db = new MovieShopContext()) {
                return db.Orders.Include(x => x.Customer.Address).Include(x => x.Movie.Genre).Include(x => x.PromoCode).FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Order> Read() {
            using (var db = new MovieShopContext()) {
                return db.Orders.Include(x => x.Customer.Address).Include(x => x.Movie.Genre).Include(x => x.PromoCode).ToList();
            }
        }

        public Order Update(Order element) {
            using (var db = new MovieShopContext()) {
                db.Entry(element).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return element;
            }
        }

        public bool Delete(int id) {
            using (var db = new MovieShopContext()) {
                db.Entry(db.Orders.FirstOrDefault(x => x.Id == id)).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return db.Orders.FirstOrDefault(x => x.Id == id) == null;
            }
        }
    }
}