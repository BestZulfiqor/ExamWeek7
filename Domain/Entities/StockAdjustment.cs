namespace Domain.Entities;

public class StockAdjustment
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int AdjustmentAmount { get; set; }
    public string Reason { get; set; }
    public DateTimeOffset AdjutmentDate { get; set; }

    public Product Product { get; set; }
}
