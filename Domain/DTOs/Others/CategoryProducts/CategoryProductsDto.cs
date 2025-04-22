using Domain.Entities;

namespace Domain.DTOs.Others.CategoryProducts;

public class CategoryProductsDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    
    public List<ProductDto> Products { get; set; }
}
