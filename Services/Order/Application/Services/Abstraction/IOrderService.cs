public interface IOrderService
{
    Task<List<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto> GetOrderByIdAsync(int id);
    Task CreateOrderAsync(OrderDto orderDto);
    Task UpdateOrderAsync(int id, OrderDto orderDto);
    Task DeleteOrderAsync(int id);
}
