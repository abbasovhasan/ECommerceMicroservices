using AutoMapper;

public class BrandService : IBrandService
{
    private readonly IReadRepository<Brand> _readRepository;
    private readonly IWriteRepository<Brand> _writeRepository;
    private readonly IMapper _mapper;

    public BrandService(IReadRepository<Brand> readRepository,
                        IWriteRepository<Brand> writeRepository,
                        IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
    {
        var brands = await _readRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task<BrandDto> GetBrandByIdAsync(int id)
    {
        var brand = await _readRepository.GetByIdAsync(id);
        return _mapper.Map<BrandDto>(brand);
    }

    public async Task AddBrandAsync(BrandDto brandDto)
    {
        var brand = _mapper.Map<Brand>(brandDto);
        await _writeRepository.AddAsync(brand);
    }

    public async Task UpdateBrandAsync(int id, BrandDto brandDto)
    {
        var brand = await _readRepository.GetByIdAsync(id);
        if (brand == null)
            throw new Exception("Brand not found");

        _mapper.Map(brandDto, brand);
        _writeRepository.Update(brand);
    }

    public async Task DeleteBrandAsync(int id)
    {
        var brand = await _readRepository.GetByIdAsync(id);
        if (brand == null)
            throw new Exception("Brand not found");

        _writeRepository.Delete(brand);
    }
}
