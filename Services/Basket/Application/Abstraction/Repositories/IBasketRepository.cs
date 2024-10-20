public interface IBasketRepository
{
    Task<Basket> GetBasketByCustomerIdAsync(string customerId); // Sepeti müşteri ID'sine göre al
    Task AddBasketAsync(Basket basket); // Sepet ekle
    Task UpdateBasketAsync(Basket basket); // Sepet güncelle
    Task DeleteBasketAsync(string basketId); // Sepet sil
}
