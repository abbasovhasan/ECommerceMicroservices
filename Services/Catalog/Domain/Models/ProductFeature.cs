public class ProductFeature : BaseEntity
{
    public string Key { get; set; } // Özellik anahtarı (Örn: Renk, Boyut)
    public string Value { get; set; } // Özellik değeri (Örn: Kırmızı, 42 numara)

    public int ProductId { get; set; } // Hangi ürüne ait olduğu
    public Product Product { get; set; }
}
