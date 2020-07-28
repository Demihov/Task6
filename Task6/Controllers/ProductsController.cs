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
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            IEnumerable<ProductDTO> products = _productService.GetAll();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id:int}")]
        public ActionResult<ProductDTO> GetProductById(int id)
        {
            ProductDTO product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public ActionResult<ProductDTO> CreateProduct([FromBody] ProductDTO product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            var result = _productService.Insert(product);

            return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
        }

        // PUT: api/Products/5
        [HttpPut("{id:int}")]
        public ActionResult<ProductDTO> ChangeProduct(int id, [FromBody] ProductDTO product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            var existingProduct = _productService.Get(id);

            if (existingProduct == null)
            {
                return NoContent();
            }

            var newProduct = new ProductDTO
            {
                Id = id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                SupplierId = product.SupplierId
            };

            _productService.Update(newProduct);

            return Ok(newProduct);
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            _productService.Delete(id);

            return NoContent();
        }
        // GET: api/products/price/400
        [HttpGet("price/{price:decimal}")]
        public ActionResult<IEnumerable<ProductDTO>> GetProductsByPrice(decimal price)
        {
            IEnumerable<ProductDTO> products = _productService.GetByPrice(price);

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }
    }
}
