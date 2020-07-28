using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Category> Categories { get; }
        IRepository<Product> Products { get; }
        IRepository<Supplier> Suppliers { get; }
        void Save();
    }
}
