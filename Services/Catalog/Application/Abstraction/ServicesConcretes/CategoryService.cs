using AutoMapper;

public class CategoryService : ICategoryService
{
    private readonly IReadRepository<Category> _readRepository;
    private readonly IWriteRepository<Category> _writeRepository;
    private readonly IMapper _mapper;

    public CategoryService(IReadRepository<Category> readRepository,
                           IWriteRepository<Category> writeRepository,
                           IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _readRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(int id)
    {
        var category = await _readRepository.GetByIdAsync(id);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task AddCategoryAsync(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _writeRepository.AddAsync(category);
    }

    public async Task UpdateCategoryAsync(int id, CategoryDto categoryDto)
    {
        var category = await _readRepository.GetByIdAsync(id);
        if (category == null)
            throw new Exception("Category not found");

        _mapper.Map(categoryDto, category);
        _writeRepository.Update(category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _readRepository.GetByIdAsync(id);
        if (category == null)
            throw new Exception("Category not found");

        _writeRepository.Delete(category);
    }
}
