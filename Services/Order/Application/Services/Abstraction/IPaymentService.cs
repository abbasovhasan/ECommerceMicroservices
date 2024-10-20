public interface IPaymentService
{
    Task<List<PaymentDto>> GetPaymentsByOrderIdAsync(int orderId);
    Task ProcessPaymentAsync(PaymentDto paymentDto);
}
