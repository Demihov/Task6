using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ICRUDService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Insert(T item);
        void Update(T item);
        void Delete(int id);
    }
}
