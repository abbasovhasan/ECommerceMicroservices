public class Basket
{
    public string BasketId { get; set; } // Sepet ID'si (Primary Key)
    public string CustomerId { get; set; } // Kullanıcı ID'si (Müşteri ID'si)
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();

    // Toplam fiyat hesaplama (miktar * birim fiyat)
    public decimal TotalPrice
    {
        get
        {
            return Items.Sum(item => item.Quantity * item.UnitPrice);
        }
    }
}
