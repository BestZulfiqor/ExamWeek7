namespace Domain.Entities;

public class Sale
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int QuantitySold { get; set; }
    public DateTimeOffset SaleDate { get; set; }

    public Product Product { get; set; }
}
