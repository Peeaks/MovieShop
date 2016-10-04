using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities {
    public class Movie : AbstractEntity {
        [Required]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public double Price { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Display(Name = "Trailer URL")]
        public string TrailerUrl { get; set; }

        public Genre Genre { get; set; }
    }
}