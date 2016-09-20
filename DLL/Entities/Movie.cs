using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities {
    public class Movie : AbstractEntity {
        public string Title { get; set; }
        public DateTime Year { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string TrailerUrl { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}