public class Category : BaseEntity
{
    public string Name { get; set; } // Kategori adı
    public string Description { get; set; } // Kategorinin kısa açıklaması

    // Parent ve Child ilişkisi için (Örneğin, Elektronik -> Telefon)
    public int? ParentCategoryId { get; set; }
    public Category ParentCategory { get; set; } // Parent kategori
    public ICollection<Category> SubCategories { get; set; } // Alt kategoriler

    // Bir kategori birçok ürüne sahip olabilir
    public ICollection<Product> Products { get; set; }
}
