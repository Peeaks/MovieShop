using System.Data.Entity;
using DLL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DLL.Contexts {
    public class MovieShopContext : IdentityDbContext<ApplicationUser> {
        public MovieShopContext() : base("name=MovieDB") {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }

        public static MovieShopContext Create() {
            return new MovieShopContext();
        }
    }
}