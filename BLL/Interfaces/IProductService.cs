using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IProductService: ICRUDService<ProductDTO>
    {
        IEnumerable<ProductDTO> GetByPrice(decimal price);
    }
}
