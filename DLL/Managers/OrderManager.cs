using System.Collections.Generic;
using System.Linq;
using DLL.Contexts;
using DLL.Entities;

namespace DLL {
    internal class OrderManager : IManager<Order> {
        public Order Create(Order element) {
            using (var db = new MovieShopContext()) {
                db.Orders.Add(element);
                db.SaveChanges();
                return element;
            }
        }

        public Order Read(int id) {
            using (var db = new MovieShopContext()) {
                return db.Orders.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Order> Read() {
            using (var db = new MovieShopContext()) {
                return db.Orders.ToList();
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