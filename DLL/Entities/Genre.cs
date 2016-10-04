using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities {
    public class Genre : AbstractEntity {
        
        public virtual string Name { get; set; }
        //public virtual List<Movie> Movies { get; set; }
    }
}
