public class Payment : BaseEntity
{
    public int OrderId { get; set; } // İlgili sipariş
    public DateTime PaymentDate { get; set; } // Ödeme tarihi
    public PaymentMethod Method { get; set; } // Ödeme yöntemi
    public decimal Amount { get; set; } // Ödenen tutar
    public PaymentStatus Status { get; set; } // Ödeme durumu

    // Navigation property
    public Order Order { get; set; }
}

public enum PaymentMethod
{
    CreditCard,
    PayPal,
    BankTransfer
}

public enum PaymentStatus
{
    Pending,
    Completed,
    Failed,
    Refunded
}
