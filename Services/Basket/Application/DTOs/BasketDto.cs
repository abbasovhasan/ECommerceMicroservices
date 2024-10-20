public class BasketDto
{
    public string BasketId { get; set; }
    public string CustomerId { get; set; }
    public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    public decimal TotalPrice { get; set; } // Burada sabit bir alan olarak tutuluyor
    
}
