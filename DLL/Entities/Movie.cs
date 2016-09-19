using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities {
    class Movie {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Year { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string TrailerUrl { get; set; }
        public Genre Genre { get; set; }
    }
}
