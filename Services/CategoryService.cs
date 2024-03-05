
using Microsoft.EntityFrameworkCore;
using SeriesApi.Models;

namespace SeriesApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.OrderBy(x => x.Name).ToListAsync();
        }
        public async Task<Category> CreateCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            _context.SaveChanges();
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return category;
        }

        public Category DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return category;
        }

        public async Task<Category> GetById(byte id)
        {
            return await _context.Categories.SingleOrDefaultAsync(c=>c.Id==id);
        }

        public Task<bool> isValidCategory(byte id)
        {
            return _context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
