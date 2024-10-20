public class Product : BaseEntity
{
    public string Name { get; set; } // Ürün adı
    public string Description { get; set; } // Ürün açıklaması
    public decimal Price { get; set; } // Ürün fiyatı
    public int Stock { get; set; } // Stok miktarı
    public string ImageUrl { get; set; } // Ürün resmi URL'si

    // İlişkiler
    public int CategoryId { get; set; } // Ürünün kategorisi
    public Category Category { get; set; } // Ürünün kategorisiyle ilişkisi

    public int BrandId { get; set; } // Ürünün markası
    public Brand Brand { get; set; } // Ürünün markasıyla ilişkisi

    // Ürün için ekstra özellikler
    public ICollection<ProductFeature> Features { get; set; }
}
