using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BrandController : ControllerBase
{
    private readonly IBrandService _brandService;

    public BrandController(IBrandService brandService)  
    {
        _brandService = brandService;
    }

    // Sadece kimliği doğrulanmış kullanıcılar (Admin ve User) markaları görebilir
    //[Authorize(Roles = "Admin, User")]
    [HttpGet]
    public async Task<IActionResult> GetAllBrands()
    {
        var brands = await _brandService.GetAllBrandsAsync();
        return Ok(brands);
    }

    // Sadece kimliği doğrulanmış kullanıcılar (Admin ve User) marka detaylarını görebilir
    //[Authorize(Roles = "Admin,User")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBrandById(int id)
    {
        var brand = await _brandService.GetBrandByIdAsync(id);
        if (brand == null)
            return NotFound();

        return Ok(brand);
    }

    // Yalnızca Adminler marka ekleyebilir
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddBrand([FromBody] BrandDto brandDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _brandService.AddBrandAsync(brandDto);
        return CreatedAtAction(nameof(GetBrandById), new { id = brandDto.Name }, brandDto);
    }

    // Yalnızca Adminler marka güncelleyebilir
    //[Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBrand(int id, [FromBody] BrandDto brandDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _brandService.UpdateBrandAsync(id, brandDto);
        return NoContent();
    }

    // Yalnızca Adminler marka silebilir
    //[Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        await _brandService.DeleteBrandAsync(id);
        return NoContent();
    }
}
