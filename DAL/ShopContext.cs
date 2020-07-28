using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne<Category>(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Product>()
                .HasOne<Supplier>(s => s.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId);

            Seed(modelBuilder);
        }

        public static void Seed(ModelBuilder modelBuilder)
        {
            Category laptop = new Category { Id = 1, Name = "Laptop" };
            Category smartphone = new Category { Id = 2, Name = "Smartphone" };

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "IdeaPad 100", Price = 400, CategoryId = 1, SupplierId = 1 },
                new Product { Id = 2, Name = "IdeaPad L340", Price = 1000, CategoryId = 1, SupplierId = 1 },
                new Product { Id = 3, Name = "MacBook Air 13", Price = 900, CategoryId = 1, SupplierId = 2 },
                new Product { Id = 4, Name = "MacBook Pro 15", Price = 1200, CategoryId = 1, SupplierId = 2 },
                new Product { Id = 5, Name = "i242 X-treme", Price = 50, CategoryId = 2, SupplierId = 3 },
                new Product { Id = 6, Name = "i5710 Infinity X1", Price = 70, CategoryId = 2, SupplierId = 3 }
            );

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "TM Lenovo", Locality = "Kyiv" },
                new Supplier { Id = 2, Name = "TM Apple", Locality = "Lviv" },
                new Supplier { Id = 3, Name = "Unit Nomi", Locality = "Odessa" }
            );
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Laptop" },
                new Category { Id = 2, Name = "Smartphone" }
            );
        }
    }
}
