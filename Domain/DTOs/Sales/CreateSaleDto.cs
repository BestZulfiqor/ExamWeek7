namespace Domain.DTOs.Sales;

public class CreateSaleDto
{
    public int ProductId { get; set; }
    public int QuantitySold { get; set; }
    public DateTimeOffset SaleDate { get; set; }
}
