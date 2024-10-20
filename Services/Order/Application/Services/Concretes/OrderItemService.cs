using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class OrderItemService : IOrderItemService
{
    private readonly IReadRepository<OrderItem> _readRepository;
    private readonly IWriteRepository<OrderItem> _writeRepository;
    private readonly IMapper _mapper;

    public OrderItemService(IReadRepository<OrderItem> readRepository, IWriteRepository<OrderItem> writeRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<List<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId)
    {
        var orderItems = await _readRepository.GetWhere(oi => oi.OrderId == orderId).ToListAsync();
        return _mapper.Map<List<OrderItemDto>>(orderItems);
    }

    public async Task AddOrderItemAsync(OrderItemDto orderItemDto)
    {
        var orderItem = _mapper.Map<OrderItem>(orderItemDto);
        await _writeRepository.AddAsync(orderItem);
    }
}
