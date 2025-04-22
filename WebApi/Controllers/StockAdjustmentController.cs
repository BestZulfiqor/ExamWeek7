using Domain.DTOs.Others;
using Domain.DTOs.StockAdjustments;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StockAdjustmentController(IStockAdjustmentService service)
{
    [HttpGet]
    public async Task<Response<List<GetStockAdjustmentDto>>> GetStockAdjustmentsAsync([FromQuery] StockAdjustmentFilter filter) => await service.GetStockAdjustmentsAsync(filter);

    [HttpPost]
    public async Task<Response<GetStockAdjustmentDto>> CreateStockAdjustmentAsync(CreateStockAdjustmentDto stockAdjustmentDto) => await service.CreateStockAdjustmentAsync(stockAdjustmentDto);

    [HttpPut("{id:int}")]
    public async Task<Response<GetStockAdjustmentDto>> UpdateStockAdjustmentAsync(int id, UpdateStockAdjustmentDto stockAdjustmentDto) => await service.UpdateStockAdjustmentAsync(id, stockAdjustmentDto);

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteStockAdjustmentAsync(int id) => await service.DeleteStockAdjustmentAsync(id);

    [HttpGet("{id:int}")]
    public async Task<Response<GetStockAdjustmentDto>> GetStockAdjustmentById(int id) => await service.GetStockAdjustmentById(id);

    [HttpGet("get-stock-history{productId:int}")]
    public async Task<Response<List<StockAdjustmentHistoryDto>>> GetStockHistory(int productId) => await service.GetStockHistory(productId);

}