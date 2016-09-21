using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DLL.Contexts;
using DLL.Entities;

namespace DLL {
    internal class AddressManager : IManager<Address> {
        public Address Create(Address element) {
            using (var db = new MovieShopContext()) {
                db.Addresses.Add(element);
                db.SaveChanges();
                return element;
            }
        }

        public bool Delete(int id) {
            using (var db = new MovieShopContext()) {
                db.Entry(db.Addresses.FirstOrDefault(x => x.Id == id)).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return db.Addresses.FirstOrDefault(x => x.Id == id) == null;
            }
        }

        public List<Address> Read() {
            using (var db = new MovieShopContext()) {
                return db.Addresses.ToList();
            }
        }

        public Address Read(int id) {
            using (var db = new MovieShopContext()) {
                return db.Addresses.FirstOrDefault(x => x.Id == id);
            }
        }

        public Address Update(Address element) {
            using (var db = new MovieShopContext()) {
                db.Entry(element).State = EntityState.Modified;
                db.SaveChanges();
                return element;
            }
        }
    }
}