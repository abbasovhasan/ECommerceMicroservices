using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    // Get all payments for a specific order
    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetPaymentsByOrderId(int orderId)
    {
        var payments = await _paymentService.GetPaymentsByOrderIdAsync(orderId);
        return Ok(payments);
    }

    // Process a payment
    [HttpPost]
    public async Task<IActionResult> ProcessPayment([FromBody] PaymentDto paymentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _paymentService.ProcessPaymentAsync(paymentDto);
        return Ok();
    }
}
