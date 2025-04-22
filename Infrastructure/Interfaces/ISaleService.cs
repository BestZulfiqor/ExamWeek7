using Domain.DTOs.Others;
using Domain.DTOs.Sales;
using Domain.Filters;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface ISaleService
{
    Task<Response<List<GetSaleDto>>> GetSalesAsync(SaleFilter filter);
    Task<Response<GetSaleDto>> CreateSaleAsync(CreateSaleDto saleDto);
    Task<Response<GetSaleDto>> UpdateSaleAsync(int id, UpdateSaleDto saleDto);
    Task<Response<string>> DeleteSaleAsync(int id);
    Task<Response<GetSaleDto>> GetSaleById(int id);
    Task<Response<List<SalesByDateDto>>> GetSalesByDateAsync(DateTimeOffset fromDate, DateTimeOffset toDate);
    Task<Response<List<TopProductsDto>>> GetTopProducts();
    Task<Response<List<DailyRevenueDto>>> GetDailyRevenue();
}
