using Microsoft.EntityFrameworkCore;

public class BasketDbContext : DbContext
{
    public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options) { }

    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Basket>()
            .HasMany(b => b.Items)
            .WithOne()
            .HasForeignKey(bi => bi.BasketId)
            .OnDelete(DeleteBehavior.Cascade); // Sepet silindiğinde ürünleri de silinsin
    }
}
