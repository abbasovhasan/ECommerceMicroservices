using Microsoft.EntityFrameworkCore;

public class BasketRepository : IBasketRepository
{
    private readonly BasketDbContext _context;

    public BasketRepository(BasketDbContext context)
    {
        _context = context;
    }

    // Müşteri ID'sine göre sepeti getir
    public async Task<Basket> GetBasketByCustomerIdAsync(string customerId)
    {
        return await _context.Baskets
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.CustomerId == customerId);
    }

    // Yeni bir sepet ekle
    public async Task AddBasketAsync(Basket basket)
    {
        await _context.Baskets.AddAsync(basket);
        await _context.SaveChangesAsync();
    }

    // Mevcut bir sepeti güncelle
    public async Task UpdateBasketAsync(Basket basket)
    {
        _context.Baskets.Update(basket);
        await _context.SaveChangesAsync();
    }

    // Sepeti sil
    public async Task DeleteBasketAsync(string basketId)
    {
        var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.BasketId == basketId);
        if (basket != null)
        {
            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();
        }
    }
}
