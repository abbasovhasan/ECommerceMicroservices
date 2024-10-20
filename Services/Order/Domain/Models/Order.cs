public class Order : BaseEntity
{
    public int CustomerId { get; set; } // Siparişi veren müşteri
    public DateTime OrderDate { get; set; } // Sipariş tarihi
    public DateTime? ShippedDate { get; set; } // Kargo tarihi
    public string ShippingAddress { get; set; } // Kargo adresi
    public decimal TotalAmount { get; set; } // Toplam tutar
    public OrderStatus Status { get; set; } // Sipariş durumu

    // Navigation property
    public ICollection<OrderItem> OrderItems { get; set; } // Siparişteki ürünler
}

public enum OrderStatus
{
    Pending,
    Shipped,
    Delivered,
    Canceled
}
