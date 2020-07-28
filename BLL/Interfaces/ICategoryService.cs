using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface ICategoryService : ICRUDService<CategoryDTO>
    {
        IEnumerable<ProductDTO> GetProductsByCategory(int id);
        IEnumerable<SupplierDTO> GetSuppliersByCategory(int id);
    }
}
