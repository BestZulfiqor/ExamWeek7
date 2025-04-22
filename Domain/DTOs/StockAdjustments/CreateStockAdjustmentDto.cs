namespace Domain.DTOs.StockAdjustments;

public class CreateStockAdjustmentDto
{
    public int ProductId { get; set; }
    public int AdjustmentAmount { get; set; }
    public string Reason { get; set; }
    public DateTimeOffset AdjutmentDate { get; set; }
}
