using System.Collections.Generic;
using System.Linq;
using DLL.Contexts;
using DLL.Entities;

namespace DLL.Managers {
    class GenreManager : IManager<Genre, int> {
        public Genre Create(Genre t) {
            using (var db = new MovieShopContext()) {
                db.Genres.Add(t);
                db.SaveChanges();
                return t;
            }
        }

        public List<Genre> Read() {
            using (var db = new MovieShopContext()) {
                return db.Genres.ToList();
            }
        }

        public Genre Read(int id) {
            using (var db = new MovieShopContext()) {
                return db.Genres.FirstOrDefault(x => x.Id == id);
            }
        }

        public Genre Update(Genre t) {
            using (var db = new MovieShopContext()) {
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return t;
            }
        }

        public bool Delete(int id) {
            using (var db = new MovieShopContext()) {
                db.Entry(db.Genres.FirstOrDefault(x => x.Id == id)).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return db.Genres.FirstOrDefault(x => x.Id == id) == null;
            }
        }
    }
}