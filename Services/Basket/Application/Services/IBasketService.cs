public interface IBasketService
{
    Task<BasketDto> GetBasketByCustomerIdAsync(string customerId);  // Müşteriye göre sepeti getir
    Task<BasketDto> CreateBasketAsync(BasketDto basketDto);   // Yeni bir sepet oluştur
    Task<BasketDto> UpdateBasketAsync(string customerId, BasketDto basketDto); // Sepeti güncelle
    Task DeleteBasketAsync(string basketId);  // Sepeti sil
}
