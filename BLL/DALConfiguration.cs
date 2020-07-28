using DAL;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class DALConfiguration
    {
        public static void injectDependencies(IServiceCollection services, string connection)
        {
            services.AddDbContext<ShopContext>(options =>
                options.UseSqlServer(connection));

            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<Supplier>, SupplierRepository>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        }
    }
}
