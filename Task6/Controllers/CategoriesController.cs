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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategories()
        {
            IEnumerable<CategoryDTO> categories = _categoryService.GetAll();

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id:int}")]
        public ActionResult<CategoryDTO> GetCategoryById(int id)
        {
            CategoryDTO category = _categoryService.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // POST: api/Categories
        [HttpPost]
        public ActionResult<CategoryDTO> CreateCategory([FromBody] CategoryDTO category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            var result = _categoryService.Insert(category);

            return CreatedAtAction(nameof(GetCategoryById), new { id = result.Id }, result);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public ActionResult<CategoryDTO> ChangeCategory(int id, [FromBody] CategoryDTO category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            var existingCategory = _categoryService.Get(id);

            if (existingCategory == null)
            {
                return NoContent();
            }

            var newCategory = new CategoryDTO
            {
                Id = id,
                Name = category.Name
            };

            _categoryService.Update(newCategory);

            return Ok(newCategory);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var category = _categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            _categoryService.Delete(id);

            return NoContent();
        }

        [HttpGet("{id:int}/suppliers")]
        public ActionResult<IEnumerable<SupplierDTO>> GetSuppliersByCategoryId(int id)
        {
            IEnumerable<SupplierDTO> suppliers = _categoryService.GetSuppliersByCategory(id);

            if (suppliers == null)
            {
                return NotFound();
            }

            return Ok(suppliers);
        }

        [HttpGet("{id:int}/products")]
        public ActionResult<IEnumerable<ProductDTO>> GetProductsByCategory(int id)
        {
            IEnumerable<ProductDTO> products = _categoryService.GetProductsByCategory(id);

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }
    }
}
