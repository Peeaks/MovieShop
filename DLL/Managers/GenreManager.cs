using System;
using System.Collections.Generic;
using System.Linq;
using DLL.Contexts;
using DLL.Entities;

namespace DLL {
    internal class GenreManager : IManager<Genre> {
        public Genre Create(Genre element) {
            using (var db = new MovieShopContext()) {
                db.Genres.Add(element);
                db.SaveChanges();
                return element;
            }
        }

        public Genre Read(int id) {
            using (var db = new MovieShopContext()) {
                return db.Genres.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Genre> Read() {
            using (var db = new MovieShopContext()) {
                if (db.Genres != null) {
                    return db.Genres.ToList();
                }
                return new List<Genre>();
            }
        }

        public Genre Update(Genre element) {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id) {
            throw new System.NotImplementedException();
        }
    }
}