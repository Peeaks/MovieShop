using System.Data.Entity;
using DLL.Entities;

namespace DLL.Contexts {
    public class MovieShopContext : DbContext {
        public MovieShopContext() : base() {
            
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}