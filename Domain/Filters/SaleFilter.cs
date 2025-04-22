namespace Domain.Filters;

public class SaleFilter
{
    public DateTimeOffset? FromSaleDate { get; set; }
    public DateTimeOffset? ToSaleDate { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
