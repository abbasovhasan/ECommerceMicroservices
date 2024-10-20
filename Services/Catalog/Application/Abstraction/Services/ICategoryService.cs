public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto> GetCategoryByIdAsync(int id);
    Task AddCategoryAsync(CategoryDto categoryDto);
    Task UpdateCategoryAsync(int id, CategoryDto categoryDto);
    Task DeleteCategoryAsync(int id);
}
