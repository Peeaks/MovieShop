using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DLL.Contexts;
using DLL.Entities;

namespace DLL.Managers {
    internal class OrderManager : IManager<Order, int> {
        public Order Create(Order element) {
            using (var db = new MovieShopContext()) {
                element.ApplicationUser =
                    db.Users.Include(x => x.Address).FirstOrDefault(x => element.ApplicationUser.Id == x.Id);

                var tempList = new List<Movie>();
                foreach (var movie in element.Movies) {
                    tempList.Add(db.Movies.FirstOrDefault(x => x.Id == movie.Id));
                }

                element.Movies = tempList;


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
                return db.Orders.Include(x => x.ApplicationUser.Address).Include(x => x.Movies).Include(x => x.PromoCode).FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Order> Read() {
            using (var db = new MovieShopContext()) {
                return db.Orders.Include(x => x.ApplicationUser.Address).Include(x => x.Movies).Include(x => x.PromoCode).ToList();
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