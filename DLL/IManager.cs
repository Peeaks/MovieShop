using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DLL.Entities;

namespace DLL {
    public interface IManager<T, R> {
        T Create(T element);
        T Read(R id);
        List<T> Read();
        T Update(T element);
        bool Delete(R id);
    }
}
