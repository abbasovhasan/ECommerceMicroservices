using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class BasketItemController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketItemController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    // Sepetteki öğeleri getir
    [HttpGet("{customerId}/items")]
    public async Task<IActionResult> GetBasketItems(string customerId)
    {
        var basket = await _basketService.GetBasketByCustomerIdAsync(customerId);
        if (basket == null)
        {
            return NotFound($"Basket for customer {customerId} not found.");
        }
        return Ok(basket.Items);
    }

    // Sepete yeni bir ürün ekle
    [HttpPost("{customerId}/items")]
    public async Task<IActionResult> AddBasketItem(string customerId, [FromBody] BasketItemDto itemDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var basket = await _basketService.GetBasketByCustomerIdAsync(customerId);
        if (basket == null)
        {
            return NotFound($"Basket for customer {customerId} not found.");
        }

        basket.Items.Add(itemDto);
        await _basketService.UpdateBasketAsync(customerId, new BasketDto { CustomerId = customerId, Items = basket.Items });

        return Ok(basket);
    }

    // Sepetteki bir öğeyi güncelle
    [HttpPut("{customerId}/items/{productId}")]
    public async Task<IActionResult> UpdateBasketItem(string customerId, int productId, [FromBody] BasketItemDto updatedItemDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var basket = await _basketService.GetBasketByCustomerIdAsync(customerId);
        if (basket == null)
        {
            return NotFound($"Basket for customer {customerId} not found.");
        }

        var item = basket.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item == null)
        {
            return NotFound($"Product with ID {productId} not found in basket.");
        }

        item.Quantity = updatedItemDto.Quantity;
        item.UnitPrice = updatedItemDto.UnitPrice;

        await _basketService.UpdateBasketAsync(customerId, new BasketDto { CustomerId = customerId, Items = basket.Items });

        return Ok(basket);
    }

    // Sepetten bir öğeyi sil
    [HttpDelete("{customerId}/items/{productId}")]
    public async Task<IActionResult> DeleteBasketItem(string customerId, int productId)
    {
        var basket = await _basketService.GetBasketByCustomerIdAsync(customerId);
        if (basket == null)
        {
            return NotFound($"Basket for customer {customerId} not found.");
        }

        var item = basket.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item == null)
        {
            return NotFound($"Product with ID {productId} not found in basket.");
        }

        basket.Items.Remove(item);
        await _basketService.UpdateBasketAsync(customerId, new BasketDto { CustomerId = customerId, Items = basket.Items });

        return Ok(basket);
    }
}
