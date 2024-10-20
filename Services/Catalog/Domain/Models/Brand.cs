public class Brand : BaseEntity
{
    public string Name { get; set; } // Marka adı
    public string Description { get; set; } // Marka açıklaması

    // Bir marka birden çok ürüne sahip olabilir
    public ICollection<Product> Products { get; set; }
}
