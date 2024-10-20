using AutoMapper;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<List<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<List<OrderDto>>(orders);
    }

    public async Task<OrderDto> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) throw new Exception("Order not found.");
        return _mapper.Map<OrderDto>(order);
    }

    public async Task CreateOrderAsync(OrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        await _orderRepository.AddAsync(order);
    }

    public async Task UpdateOrderAsync(int id, OrderDto orderDto)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) throw new Exception("Order not found.");

        _mapper.Map(orderDto, order); // DTO'dan entity'ye map yapılıyor
        await _orderRepository.UpdateAsync(order);
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) throw new Exception("Order not found.");

        await _orderRepository.DeleteAsync(order);
    }
}
