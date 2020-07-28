using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork, IDisposable
    {
        private ShopContext db;
        private SupplierRepository supplierRepository;  
        private ProductRepository productRepository;
        private CategoryRepository categoryRepository;

        public EFUnitOfWork(ShopContext context)
        {
            db = context;
        }

        public IRepository<Category> Categories => categoryRepository ?? new CategoryRepository(db);

        public IRepository<Product> Products => productRepository ?? new ProductRepository(db);

        public IRepository<Supplier> Suppliers => supplierRepository ?? new SupplierRepository(db);

        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
