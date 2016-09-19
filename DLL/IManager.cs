using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DLL.Entities;

namespace DLL {
    public interface IManager<T> where T : AbstractEntity {
        T Create(T element);
        T Read(int id);
        List<T> Read();
        T Update(T element);
        bool Delete(int id);
    }
}
