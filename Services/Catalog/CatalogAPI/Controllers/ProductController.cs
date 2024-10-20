using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // Sadece kimliği doğrulanmış kullanıcılar (Admin ve User) ürünleri görebilir
   // [Authorize(Roles = "Admin,User")]
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    // Sadece kimliği doğrulanmış kullanıcılar (Admin ve User) ürün detaylarını görebilir
    //[Authorize(Roles = "Admin,User")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    // Yalnızca Adminler yeni ürün ekleyebilir
   // [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _productService.AddProductAsync(productDto);
        return CreatedAtAction(nameof(GetProductById), new { id = productDto.Name }, productDto);
    }

    // Yalnızca Adminler ürün güncelleyebilir
   // [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _productService.UpdateProductAsync(id, productDto);
        return NoContent();
    }

    // Yalnızca Adminler ürün silebilir
   // [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}
