using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DLL.Contexts;
using DLL.Entities;

namespace DLL {
    internal class MovieManager : IManager<Movie> {

        public Movie Create(Movie element) {
            using (var db = new MovieShopContext()) {
                element.Genre = db.Genres.FirstOrDefault(x => element.Genre.Id == x.Id);
                db.Movies.Add(element);
                db.SaveChanges();
                return element;
            }
        }

        public Movie Read(int id) {
            using (var db = new MovieShopContext()) {
                //return db.Movies.Where(x => x.Id == id).Include(x => x.Genre).FirstOrDefault();
                return db.Movies.Include(x => x.Genre).FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Movie> Read() {
            using (var db = new MovieShopContext()) {
                return db.Movies.Include(b => b.Genre).ToList();
            }
        }

        public Movie Update(Movie element) {
            using (var db = new MovieShopContext()) {
                db.Entry(element).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return element;
            }
        }

        public bool Delete(int id) {
            using (var db = new MovieShopContext()) {
                db.Entry(db.Movies.FirstOrDefault(x => x.Id == id)).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return db.Movies.FirstOrDefault(x => x.Id == id) == null;
            }
        }
    }
}