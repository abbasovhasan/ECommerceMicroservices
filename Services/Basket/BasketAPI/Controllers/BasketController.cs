using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    // Müşteri ID'sine göre sepeti getir
    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetBasket(string customerId)
    {
        var basket = await _basketService.GetBasketByCustomerIdAsync(customerId);
        if (basket == null)
        {
            return NotFound($"Basket for customer {customerId} not found.");
        }
        return Ok(basket);
    }

    // Yeni bir sepet oluştur
    [HttpPost]
    public async Task<IActionResult> CreateBasket([FromBody] BasketDto basketCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var basket = await _basketService.CreateBasketAsync(basketCreateDto);
        return CreatedAtAction(nameof(GetBasket), new { customerId = basket.CustomerId }, basket);
    }

    // Müşteri ID'sine göre sepeti güncelle
    [HttpPut("{customerId}")]
    public async Task<IActionResult> UpdateBasket(string customerId, [FromBody] BasketDto basketUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var basket = await _basketService.UpdateBasketAsync(customerId, basketUpdateDto);
        if (basket == null)
        {
            return NotFound($"Basket for customer {customerId} not found.");
        }

        return Ok(basket);
    }

    // Sepeti sil
    [HttpDelete("{basketId}")]
    public async Task<IActionResult> DeleteBasket(string basketId)
    {
        await _basketService.DeleteBasketAsync(basketId);
        return NoContent();
    }
}
