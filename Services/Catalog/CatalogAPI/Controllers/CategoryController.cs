using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // Sadece kimliği doğrulanmış Admin kullanıcılar tüm kategorileri görebilir
   // [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    // Sadece kimliği doğrulanmış Admin ve User kullanıcılar kategori detaylarını görebilir
   // [Authorize(Roles = "Admin,User")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
            return NotFound();

        return Ok(category);
    }

    // Yalnızca Adminler kategori ekleyebilir
  //  [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _categoryService.AddCategoryAsync(categoryDto);
        return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDto.Name }, categoryDto);
    }

    // Yalnızca Adminler kategori güncelleyebilir
    //[Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _categoryService.UpdateCategoryAsync(id, categoryDto);
        return NoContent();
    }

    // Yalnızca Adminler kategori silebilir
   // [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}