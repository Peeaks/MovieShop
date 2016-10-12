using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.Entities;

namespace DLL {
    public interface ICartManager {
        Cart Read();
        void AddPromoToCart(string code);
        void AddToCart(Movie movie);
        void RemoveFromCart(int id);
        void EmptyCart();
        void MigrateCart(string username);
    }
}
