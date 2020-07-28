using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface ISupplierService: ICRUDService<SupplierDTO>
    {
        IEnumerable<ProductDTO> GetProductsBySupplier(int id);
    }
}
