namespace Domain.DTOs.Products;

public class CreateProductDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int QuantityStock { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }
}
