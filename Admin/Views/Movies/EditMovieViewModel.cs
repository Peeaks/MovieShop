using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DLL.Entities;

namespace Admin.Views.Movies {
    public class EditMovieViewModel {
        public Movie Movie { get; set; }
        public List<Genre> Genres { get; set; }
    }
}