using Domain.DTOs.Others;
using Domain.DTOs.StockAdjustments;
using Domain.Filters;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IStockAdjustmentService
{
    Task<Response<List<GetStockAdjustmentDto>>> GetStockAdjustmentsAsync(StockAdjustmentFilter filter);
    Task<Response<GetStockAdjustmentDto>> CreateStockAdjustmentAsync(CreateStockAdjustmentDto stockAdjustmentDto);
    Task<Response<GetStockAdjustmentDto>> UpdateStockAdjustmentAsync(int id, UpdateStockAdjustmentDto stockAdjustmentDto);
    Task<Response<string>> DeleteStockAdjustmentAsync(int id);
    Task<Response<GetStockAdjustmentDto>> GetStockAdjustmentById(int id);
    Task<Response<List<StockAdjustmentHistoryDto>>> GetStockHistory(int productId);
}
