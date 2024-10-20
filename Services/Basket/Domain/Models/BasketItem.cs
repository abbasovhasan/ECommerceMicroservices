public class BasketItem
{
    public int Id { get; set; } // BasketItem ID'si (Primary Key)
    public int ProductId { get; set; } // Ürün ID'si
    public string ProductName { get; set; } // Ürün Adı
    public int Quantity { get; set; } // Ürün Adedi
    public decimal UnitPrice { get; set; } // Ürün Birim Fiyatı
    public string? ImageUrl { get; set; } // Ürün Görsel URL'si
    public string BasketId { get; set; } // Basket ID (Foreign Key)
}
