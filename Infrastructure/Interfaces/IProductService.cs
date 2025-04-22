using Domain.DTOs.Others;
using Domain.DTOs.Others.Details;
using Domain.DTOs.Products;
using Domain.Filters;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    Task<Response<List<GetProductDto>>> GetProductsAsync(ProductFilter filter);
    Task<Response<GetProductDto>> CreateProductAsync(CreateProductDto productDto);
    Task<Response<GetProductDto>> UpdateProductAsync(int id, UpdateProductDto productDto);
    Task<Response<string>> DeleteProductAsync(int id);
    Task<Response<GetProductDto>> GetProductById(int id);
    Task<Response<List<LowStockDto>>> GetProductsLowStock();
    Task<Response<ProductStatisticsDto>> GetStatistics(int id);
    Task<Response<DetailDto>> GetProductDetailById(int id);
}
