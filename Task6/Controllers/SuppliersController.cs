using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        // GET: api/Suppliers
        [HttpGet]
        public ActionResult<IEnumerable<SupplierDTO>> GetSuppliers()
        {
            var suppliers = _supplierService.GetAll();

            if (suppliers == null)
            {
                return NotFound();
            }

            return Ok(suppliers);
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public ActionResult<SupplierDTO> GetSupplierById(int id)
        {
            SupplierDTO supplier = _supplierService.Get(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        // POST: api/Suppliers
        [HttpPost]
        public ActionResult<SupplierDTO> CreateSupplier([FromBody] SupplierDTO supplier)
        {
            if (supplier == null)
            {
                return BadRequest();
            }

            var result = _supplierService.Insert(supplier);

            return CreatedAtAction(nameof(GetSupplierById), new { id = result.Id }, result);
        }

        // PUT: api/Suppliers/5
        [HttpPut("{id}")]
        public ActionResult<SupplierDTO> ChangeSupplier(int id, [FromBody] SupplierDTO supplier)
        {
            if (supplier == null)
            {
                return BadRequest();
            }

            var existingSupplier = _supplierService.Get(id);

            if (existingSupplier == null)
            {
                return NoContent();
            }

            var newSupplier = new SupplierDTO
            {
                Id = id,
                Name = supplier.Name,
                Locality = supplier.Locality
            };

            _supplierService.Update(newSupplier);

            return Ok(newSupplier);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var supplier = _supplierService.Get(id);
            if (supplier == null)
            {
                return NotFound();
            }

            _supplierService.Delete(id);

            return NoContent();
        }

        [HttpGet("{id:int}/products")]
        public ActionResult<IEnumerable<ProductDTO>> GetProductsBySupplierId(int id)
        {
            IEnumerable<ProductDTO> products = _supplierService.GetProductsBySupplier(id);

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }
    }
}
