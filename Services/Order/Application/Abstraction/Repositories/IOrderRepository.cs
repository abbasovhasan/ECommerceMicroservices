public interface IOrderRepository : IReadRepository<Order>, IWriteRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
}
