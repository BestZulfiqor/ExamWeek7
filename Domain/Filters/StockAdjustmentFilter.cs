namespace Domain.Filters;

public class StockAdjustmentFilter
{
    public int? FromAdjustmentAmount { get; set; }
    public int? ToAdjustmentAmount { get; set; }
    public DateTimeOffset? FromAdjutmentDate { get; set; }
    public DateTimeOffset? ToAdjutmentDate { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
