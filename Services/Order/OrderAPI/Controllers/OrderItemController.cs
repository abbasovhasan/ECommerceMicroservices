using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    // Get all order items for a specific order
    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetOrderItemsByOrderId(int orderId)
    {
        var orderItems = await _orderItemService.GetOrderItemsByOrderIdAsync(orderId);
        return Ok(orderItems);
    }

    // Add an order item
    [HttpPost]
    public async Task<IActionResult> AddOrderItem([FromBody] OrderItemDto orderItemDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _orderItemService.AddOrderItemAsync(orderItemDto);
        return Ok();
    }
}
