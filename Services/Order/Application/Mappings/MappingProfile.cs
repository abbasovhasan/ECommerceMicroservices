using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Order Mapping
        CreateMap<Order, OrderDto>().ReverseMap();

        // OrderItem Mapping
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();

        // Payment Mapping
        CreateMap<Payment, PaymentDto>().ReverseMap();
    }
}
