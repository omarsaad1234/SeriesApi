using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriesApi.Services;


namespace SeriesApi.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }
        [HttpPost]
        public async Task<IActionResult> PostCategoryAsync(CategoryDto category)
        {
            var newCategory = new Category { Name = category.Name };
            await _categoryService.CreateCategory(newCategory);

            return Ok(newCategory);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(byte id,CategoryDto category)
        {
            var SelectedCategory = await _categoryService.GetById(id);
            if (SelectedCategory is null)
            {
                return NotFound($"There Is No Categories With {id} As ID !");
            }
            SelectedCategory.Name = category.Name;
            _categoryService.UpdateCategory(SelectedCategory);
            return Ok(SelectedCategory);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(byte id)
        {
            var DeletedCategory = await _categoryService.GetById(id);
            if (DeletedCategory is null)
            {
                return NotFound($"There Is No Categories With {id} As ID");
            }
            _categoryService.DeleteCategory(DeletedCategory);
            return Ok(DeletedCategory);
        }
    }
}
