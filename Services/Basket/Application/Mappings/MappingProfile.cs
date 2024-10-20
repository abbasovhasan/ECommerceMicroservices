using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Basket ve BasketItem için mapleme işlemi
        CreateMap<BasketDto, Basket>();
        CreateMap<BasketItemDto, BasketItem>();
        CreateMap<Basket, BasketDto>();
        CreateMap<BasketItem, BasketItemDto>();
    }
}
