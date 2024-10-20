public interface IOrderItemService
{
    Task<List<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId);
    Task AddOrderItemAsync(OrderItemDto orderItemDto);
}
