﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.Entities;

namespace DLL {
    public class DllFacade {
        public IManager<Movie> GetMovieManager() {
            return new MovieManager();
        }
        public IManager<Genre> GetGenreManager() {
            return new GenreManager();
        }
        public IManager<Order> GetOrderManager() {
            return new OrderManager();
        }
        public IManager<Customer> GetCustomerManager() {
            return new CustomerManager();
        }
        public IManager<Address> GetAddressManager() {
            return new AddressManager();
        }
    }
}