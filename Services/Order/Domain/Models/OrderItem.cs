public class OrderItem : BaseEntity
{
    public int OrderId { get; set; } // İlgili sipariş
    public int ProductId { get; set; } // Sipariş edilen ürün
    public int Quantity { get; set; } // Ürün adedi
    public decimal UnitPrice { get; set; } // Birim fiyat
    public decimal TotalPrice { get; set; } // Toplam fiyat

    // Navigation properties
    public Order Order { get; set; }
}
