using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.Contexts;
using DLL.Entities;

namespace DLL.Managers {
    class ApplicationUserManager : IManager<ApplicationUser, string> {
        public ApplicationUser Create(ApplicationUser element) {
            using (var db = new MovieShopContext()) {
                db.Users.Add(element);
                db.SaveChanges();
                return element;
            }
        }

        public bool Delete(string id) {
            using (var db = new MovieShopContext()) {
                db.Entry(db.Users.FirstOrDefault(x => x.Id == id)).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return db.Users.FirstOrDefault(x => x.Id == id) == null;
            }
        }

        public List<ApplicationUser> Read() {
            using (var db = new MovieShopContext()) {
                return db.Users.Include(x => x.Address).ToList();
            }
        }

        public ApplicationUser Read(string id) {
            using (var db = new MovieShopContext()) {
                return db.Users.Include(x => x.Address).FirstOrDefault(x => x.Id == id);
            }
        }

        public ApplicationUser Update(ApplicationUser element) {
            using (var db = new MovieShopContext()) {
                var originalUser = db.Users.Include(j => j.Address).Single(j => j.Id == element.Id);
                db.Entry(originalUser).CurrentValues.SetValues(element);
                originalUser.Address = element.Address;

                db.SaveChanges();

                return element;

            }
        }
    }
}