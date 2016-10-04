using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DLL.Contexts;
using DLL.Entities;

namespace DLL {
    internal class CustomerManager : IManager<Customer> {
        public Customer Create(Customer element) {
            using (var db = new MovieShopContext()) {
                db.Customers.Add(element);
                db.SaveChanges();
                return element;
            }
        }

        public Customer Read(int id) {
            using (var db = new MovieShopContext()) {
                return db.Customers.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Customer> Read() {
            using (var db = new MovieShopContext()) {
                return db.Customers.ToList();
            }
        }

        public Customer Update(Customer element) {
            using (var db = new MovieShopContext()) {
                db.Entry(element).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return element;
            }
        }

        public bool Delete(int id) {
            using (var db = new MovieShopContext()) {
                db.Entry(db.Customers.FirstOrDefault(x => x.Id == id)).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return db.Customers.FirstOrDefault(x => x.Id == id) == null;
            }
        }
    }
}