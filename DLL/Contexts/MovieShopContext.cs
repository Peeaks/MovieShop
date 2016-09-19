using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.Entities;

namespace DLL.Contexts {
    public class MovieShopContext : DbContext {

        public MovieShopContext() : base() {
            
        }

        public DbSet<Genre> Genres;
        public DbSet<Movie> Movies;
        public DbSet<Order> Orders;
        public DbSet<Customer> Customers;
        public DbSet<Address> Addresses;
    }
}
