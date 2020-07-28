using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private ShopContext db;

        public ProductRepository(ShopContext context)
        {
            db = context;
        }

        public Product Insert(Product product)
        {
            if (product != null)
                db.Products.Add(product);
            return product;
        }
        public void Delete(int id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
                db.Products.Remove(product);
        }
        public Product Get(int id)
        {
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            //return db.Products.Find(id);

            var product = db.Products.Single(p => p.Id == id);

            db.Entry(product).Reference(p => p.Category).Load();
            db.Entry(product).Reference(p => p.Supplier).Load();

            return product;
        }
        public IEnumerable<Product> GetAll()
        {
            return db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier);
        }
        public void Update(Product product)
        {
            if (product != null)
                db.Update(product);
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
