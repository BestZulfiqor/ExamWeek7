using System.Net;
using AutoMapper;
using Domain.DTOs.Others;
using Domain.DTOs.Others.Details;
using Domain.DTOs.Products;
using Domain.Entities;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductService(DataContext context, IMapper mapper) : IProductService
{
    public async Task<Response<GetProductDto>> CreateProductAsync(CreateProductDto productDto)
    {
        var product = mapper.Map<Product>(productDto);
        await context.Products.AddAsync(product);
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetProductDto>(product);
        return result == 0
            ? new Response<GetProductDto>(HttpStatusCode.BadRequest, "Product not added!")
            : new Response<GetProductDto>(dto);

    }

    public async Task<Response<string>> DeleteProductAsync(int id)
    {
        var exist = await context.Products.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Product not found");
        }
        context.Products.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Product not deleted!")
            : new Response<string>("Product deleted succesfully!");
    }

    public async Task<Response<List<GetProductDto>>> GetProductsAsync(ProductFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);

        var products = context.Products.AsQueryable();

        if (filter.Name != null)
        {
            products = products.Where(n => n.Name.Contains(filter.Name));
        }

        if (filter.FromPrice != null)
        {
            products = products.Where(n => n.Price >= filter.FromPrice);
        }

        if (filter.ToPrice != null)
        {
            products = products.Where(n => n.Price <= filter.ToPrice);
        }

        if (filter.FromQuantityStock != null)
        {
            products = products.Where(n => n.QuantityStock >= filter.FromQuantityStock);
        }

        if (filter.ToQuantityStock != null)
        {
            products = products.Where(n => n.QuantityStock <= filter.ToQuantityStock);
        }
        var mapped = mapper.Map<List<GetProductDto>>(products);

        var data = mapped
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToList();

        var totalRecords = mapped.Count;
        return new PagedResponse<List<GetProductDto>>(data, validFilter.PageNumber, validFilter.PageSize, totalRecords);
    }

    public async Task<Response<GetProductDto>> GetProductById(int id)
    {
        var exist = await context.Products.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetProductDto>(HttpStatusCode.NotFound, "Product not found!");
        }
        var dto = mapper.Map<GetProductDto>(exist);
        return new Response<GetProductDto>(dto);
    }

    public async Task<Response<GetProductDto>> UpdateProductAsync(int id, UpdateProductDto ProductDto)
    {
        var exist = await context.Products.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetProductDto>(HttpStatusCode.NotFound, "Product not found!");
        }

        exist.Name = ProductDto.Name;
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetProductDto>(exist);
        return result == 0
            ? new Response<GetProductDto>(HttpStatusCode.BadRequest, "Product not updated!")
            : new Response<GetProductDto>(dto);
    }

    // Task 2
    public async Task<Response<List<LowStockDto>>> GetProductsLowStock()
    {
        var products = await context.Products
            .Where(n => n.QuantityStock < 5)
            .Select(n => new LowStockDto
            {
                Id = n.Id,
                Name = n.Name,
                QuantityInStock = n.QuantityStock
            })
            .ToListAsync();

        return new Response<List<LowStockDto>>(products);
    }

    // Task 3
    public async Task<Response<ProductStatisticsDto>> GetStatistics(int id)
    {
        var averagePrice = await context.Products.AverageAsync(n => n.Price);
        var totalProducts = await context.Products.SumAsync(n => n.QuantityStock);
        var totalSold = await context.Products.SumAsync(n => n.Sales.Count);

        var statistics = new ProductStatisticsDto
        {
            TotalProducts = totalProducts,
            AveragePrice = averagePrice,
            TotalSold = totalSold
        };

        return new Response<ProductStatisticsDto>(statistics);
    }

    // Task 8
    public async Task<Response<DetailDto>> GetProductDetailById(int id)
    {
        var product = await context.Products
            .Where(n => n.Id == id)
            .Select(n => new DetailDto
            {
                Id = n.Id,
                Name = n.Name,
                Price = n.Price,
                QuantityInStock = n.QuantityStock,
                Category = n.Category.Name,
                Supplier = n.Supplier.Name,
                Sales = n.Sales.Select(n => new SalesDto
                {
                    Quantity = n.QuantitySold,
                    Date = n.SaleDate.Date
                }).ToList(),
                Adjustments = n.StockAdjustments.Select(n => new AdjustmentDto{
                    Amount = n.AdjustmentAmount,
                    Date = n.AdjutmentDate.Date,
                    Reason = n.Reason
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return new Response<DetailDto>(product);
    }

}
