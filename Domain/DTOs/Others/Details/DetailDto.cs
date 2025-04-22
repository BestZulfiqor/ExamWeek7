namespace Domain.DTOs.Others.Details;

public class DetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public string Category { get; set; }
    public string Supplier { get; set; }
    public List<SalesDto> Sales { get; set; }
    public List<AdjustmentDto> Adjustments { get; set; }
}
