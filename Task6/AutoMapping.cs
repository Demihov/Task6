using AutoMapper;
using BLL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task6
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Supplier, SupplierDTO>();

            CreateMap<ProductDTO, Product>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<SupplierDTO, Supplier>();
        }
    }
}
