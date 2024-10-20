public class PaymentDto
{
    public int OrderId { get; set; } // İlgili sipariş
    public DateTime PaymentDate { get; set; } // Ödeme tarihi
    public PaymentMethod Method { get; set; } // Ödeme yöntemi
    public decimal Amount { get; set; } // Ödenen tutar
}
