namespace Domain.DTOs.Others;

public class SalesByDateDto
{
    public int SaleId { get; set; }
    public string Name { get; set; }
    public int QuantitySold { get; set; }
    public DateTimeOffset SaleDate { get; set; }
}
