using Domain.DTOs.Others;
using Domain.DTOs.Sales;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SaleController(ISaleService service)
{
    [HttpGet]
    public async Task<Response<List<GetSaleDto>>> GetSalesAsync([FromQuery] SaleFilter filter) => await service.GetSalesAsync(filter);

    [HttpPost]
    public async Task<Response<GetSaleDto>> CreateSaleAsync(CreateSaleDto saleDto) => await service.CreateSaleAsync(saleDto);

    [HttpPut("{id:int}")]
    public async Task<Response<GetSaleDto>> UpdateSaleAsync(int id, UpdateSaleDto saleDto) => await service.UpdateSaleAsync(id, saleDto);

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteSaleAsync(int id) => await service.DeleteSaleAsync(id);

    [HttpGet("{id:int}")]
    public async Task<Response<GetSaleDto>> GetSaleById(int id) => await service.GetSaleById(id);

    [HttpGet("{fromDate:datetime}/{toDate:datetime}")]
    public async Task<Response<List<SalesByDateDto>>> GetSalesByDateAsync(DateTimeOffset fromDate, DateTimeOffset toDate) => await service.GetSalesByDateAsync(fromDate, toDate);

    [HttpGet("top-products")]
    public async Task<Response<List<TopProductsDto>>> GetTopProducts() => await service.GetTopProducts();

    [HttpGet("daily-revenue")]
    public async Task<Response<List<DailyRevenueDto>>> GetDailyRevenue() => await service.GetDailyRevenue();

}