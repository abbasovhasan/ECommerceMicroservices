public class OrderDto
{
    public int CustomerId { get; set; }  // Siparişi veren müşteri
    public DateTime OrderDate { get; set; } // Sipariş tarihi
    public string ShippingAddress { get; set; } // Kargo adresi
    public decimal TotalAmount { get; set; } // Toplam tutar
    public List<OrderItemDto> OrderItems { get; set; } // Sipariş edilen ürünler
}
