using AutoMapper;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public BasketService(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }

    // Müşteri ID'sine göre sepeti getir
    public async Task<BasketDto> GetBasketByCustomerIdAsync(string customerId)
    {
        var basket = await _basketRepository.GetBasketByCustomerIdAsync(customerId);
        if (basket == null)
        {
            return null;
        }
        return _mapper.Map<BasketDto>(basket);
    }

    // Yeni sepet oluştur
    public async Task<BasketDto> CreateBasketAsync(BasketDto basketDto)
    {
        var basket = _mapper.Map<Basket>(basketDto);
        await _basketRepository.AddBasketAsync(basket);
        return _mapper.Map<BasketDto>(basket);
    }

    // Sepeti güncelle
    public async Task<BasketDto> UpdateBasketAsync(string customerId, BasketDto basketDto)
    {
        var basket = await _basketRepository.GetBasketByCustomerIdAsync(customerId);
        if (basket == null)
        {
            return null; // Sepet bulunamadı
        }

        // Sepet güncelleniyor
        basket.Items = _mapper.Map<List<BasketItem>>(basketDto.Items);
        await _basketRepository.UpdateBasketAsync(basket);

        return _mapper.Map<BasketDto>(basket);
    }

    // Sepeti sil
    public async Task DeleteBasketAsync(string basketId)
    {
        await _basketRepository.DeleteBasketAsync(basketId);
    }
}
