using Microsoft.EntityFrameworkCore;

public class OrderRepository : ReadRepository<Order>, IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context) : base(context)
    {
        _context = context;
    }

    // Get orders by customer id
    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
    {
        return await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
    }

    // Add a new order
    public async Task AddAsync(Order entity)
    {
        await _context.Orders.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    // Update an existing order
    public async Task UpdateAsync(Order entity)
    {
        _context.Orders.Update(entity);
        await _context.SaveChangesAsync();
    }

    // Delete an order
    public async Task DeleteAsync(Order entity)
    {
        _context.Orders.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
