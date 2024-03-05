namespace SeriesApi.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();

        Task<Category> GetById(byte id);

        Task<Category> CreateCategory(Category category);

        Category UpdateCategory(Category category);
        
        Category DeleteCategory(Category category);
        Task<bool> isValidCategory(byte id);
    }
}
