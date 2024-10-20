public class BaseEntity
{
    public int Id { get; set; } // Tüm entity'lerde ortak olacak Id alanı

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Entity oluşturulma zamanı
    public DateTime? UpdatedDate { get; set; } // Güncellenme zamanı, nullable olabilir
}