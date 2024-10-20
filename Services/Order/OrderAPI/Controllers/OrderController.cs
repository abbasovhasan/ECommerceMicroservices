using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")] 
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // Get all orders
    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    // Get a specific order by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    // Create a new order
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _orderService.CreateOrderAsync(orderDto);
        return CreatedAtAction(nameof(GetOrderById), new { id = orderDto.CustomerId }, orderDto);
    }

    // Update an existing order
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDto orderDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _orderService.UpdateOrderAsync(id, orderDto);
        return NoContent();
    }

    // Delete an order
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        await _orderService.DeleteOrderAsync(id);
        return NoContent();
    }
}
