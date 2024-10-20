using AutoMapper;

public class ProductService : IProductService
{
    private readonly IReadRepository<Product> _readRepository;
    private readonly IWriteRepository<Product> _writeRepository;
    private readonly IMapper _mapper;

    public ProductService(IReadRepository<Product> readRepository,
                          IWriteRepository<Product> writeRepository,
                          IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _readRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _readRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task AddProductAsync(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        await _writeRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(int id, ProductDto productDto)
    {
        var product = await _readRepository.GetByIdAsync(id);
        if (product == null)
            throw new Exception("Product not found");

        _mapper.Map(productDto, product);
        _writeRepository.Update(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _readRepository.GetByIdAsync(id);
        if (product == null)
            throw new Exception("Product not found");

        _writeRepository.Delete(product);
    }
}
