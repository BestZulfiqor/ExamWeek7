namespace Domain.DTOs.Others;

public class StockAdjustmentHistoryDto
{
    public DateTimeOffset AdjustmentDate { get; set; }
    public int Amount { get; set; }
    public string Reason { get; set; }
}
