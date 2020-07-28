using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private ShopContext db;

        public CategoryRepository(ShopContext context)
        {
            db = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories.Include(c => c.Products);
        }
        public Category Get(int id)
        {
            //db.Categories.Find(id);

            var category = db.Categories.Single(c => c.Id == id);

            db.Entry(category).Collection(c => c.Products).Load();

            return category;
        }
        public Category Insert(Category category)
        {
            if (category != null)
                db.Categories.Add(category);
            return category;
        }
        public void Update(Category category)
        {
            db.Entry(category).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Category category = db.Categories.Find(id);
            if (category != null)
                db.Remove(category);
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
