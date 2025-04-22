using Domain.DTOs.Others;
using Domain.DTOs.Others.Details;
using Domain.DTOs.Products;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService service)
{
    [HttpGet]
    public async Task<Response<List<GetProductDto>>> GetProductsAsync([FromQuery] ProductFilter filter) => await service.GetProductsAsync(filter);

    [HttpPost]
    public async Task<Response<GetProductDto>> CreateProductAsync(CreateProductDto productDto) => await service.CreateProductAsync(productDto);

    [HttpPut("{id:int}")]
    public async Task<Response<GetProductDto>> UpdateProductAsync(int id, UpdateProductDto productDto) => await service.UpdateProductAsync(id, productDto);

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteProductAsync(int id) => await service.DeleteProductAsync(id);

    [HttpGet("{id:int}")]
    public async Task<Response<GetProductDto>> GetProductById(int id) => await service.GetProductById(id);

    [HttpGet("low-stock")]
    public async Task<Response<List<LowStockDto>>> GetProductsLowStock() => await service.GetProductsLowStock();

    [HttpGet("statistics{id:int}")]
    public async Task<Response<ProductStatisticsDto>> GetStatistics(int id) => await service.GetStatistics(id);

    [HttpGet("product-detail/{productId:int}")]
    public async Task<Response<DetailDto>> GetProductDetailById(int id) => await service.GetProductDetailById(id);
    
}