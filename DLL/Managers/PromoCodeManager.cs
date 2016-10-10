using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.Contexts;
using DLL.Entities;

namespace DLL.Managers {
    class PromoCodeManager : IManager<PromoCode, string> {
        public PromoCode Create(PromoCode element) {
            using (var db = new MovieShopContext()) {
                db.PromoCodes.Add(element);
                db.SaveChanges();
                return element;
            }
        }

        public bool Delete(string code) {
            using (var db = new MovieShopContext()) {
                db.Entry(db.PromoCodes.FirstOrDefault(x => x.Code == code)).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return db.PromoCodes.FirstOrDefault(x => x.Code == code) == null;
            }
        }

        public List<PromoCode> Read() {
            using (var db = new MovieShopContext()) {
                return db.PromoCodes.ToList();
            }
        }

        public PromoCode Read(string code) {
            using (var db = new MovieShopContext()) {
                return db.PromoCodes.FirstOrDefault(x => x.Code == code);
            }
        }

        public PromoCode Update(PromoCode element) {
            using (var db = new MovieShopContext()) {
                db.Entry(element).State = EntityState.Modified;
                db.SaveChanges();
                return element;
            }
        }
    }
}