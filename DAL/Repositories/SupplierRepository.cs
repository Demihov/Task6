using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class SupplierRepository : IRepository<Supplier>
    {
        private ShopContext db;

        public SupplierRepository(ShopContext context)
        {
            db = context;
        }

        public IEnumerable<Supplier> GetAll()
        {
            return db.Suppliers.Include(s => s.Products);
        }
        public Supplier Get(int id)
        {
            //return db.Suppliers.Find(id);

            var supplier = db.Suppliers.Single(s => s.Id == id);

            db.Entry(supplier).Collection(s => s.Products).Load();

            return supplier;
        }
        public Supplier Insert(Supplier supplier)
        {
            if (supplier != null)
                db.Suppliers.Add(supplier);
            return supplier;
        }
        public void Update(Supplier supplier)
        {
            db.Entry(supplier).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier != null)
                db.Remove(supplier);
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
