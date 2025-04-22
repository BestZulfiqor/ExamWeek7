using System.Net;
using AutoMapper;
using Domain.DTOs.Others;
using Domain.DTOs.Sales;
using Domain.Entities;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class SaleService(DataContext context, IMapper mapper) : ISaleService
{
    public async Task<Response<GetSaleDto>> CreateSaleAsync(CreateSaleDto saleDto)
    {
        var sale = mapper.Map<Sale>(saleDto);
        await context.Sales.AddAsync(sale);
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetSaleDto>(sale);
        return result == 0
            ? new Response<GetSaleDto>(HttpStatusCode.BadRequest, "Sale not added!")
            : new Response<GetSaleDto>(dto);

    }

    public async Task<Response<string>> DeleteSaleAsync(int id)
    {
        var exist = await context.Sales.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Sale not found");
        }
        context.Sales.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Sale not deleted!")
            : new Response<string>("Sale deleted succesfully!");
    }

    public async Task<Response<List<GetSaleDto>>> GetSalesAsync(SaleFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);

        var Sales = context.Sales.AsQueryable();

        if (filter.FromSaleDate != null)
        {
            Sales = Sales.Where(n => n.SaleDate >= filter.FromSaleDate);
        }

        if (filter.ToSaleDate != null)
        {
            Sales = Sales.Where(n => n.SaleDate <= filter.ToSaleDate);
        }

        var mapped = mapper.Map<List<GetSaleDto>>(Sales);

        var data = mapped
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToList();

        var totalRecords = mapped.Count;
        return new PagedResponse<List<GetSaleDto>>(data, validFilter.PageNumber, validFilter.PageSize, totalRecords);
    }

    public async Task<Response<GetSaleDto>> GetSaleById(int id)
    {
        var exist = await context.Sales.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetSaleDto>(HttpStatusCode.NotFound, "Sale not found!");
        }
        var dto = mapper.Map<GetSaleDto>(exist);
        return new Response<GetSaleDto>(dto);
    }

    public async Task<Response<GetSaleDto>> UpdateSaleAsync(int id, UpdateSaleDto SaleDto)
    {
        var exist = await context.Sales.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetSaleDto>(HttpStatusCode.NotFound, "Sale not found!");
        }

        exist.ProductId = SaleDto.ProductId;
        exist.QuantitySold = SaleDto.QuantitySold;
        exist.SaleDate = SaleDto.SaleDate;
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetSaleDto>(exist);
        return result == 0
            ? new Response<GetSaleDto>(HttpStatusCode.BadRequest, "Sale not updated!")
            : new Response<GetSaleDto>(dto);
    }

    // Task 4
    public async Task<Response<List<SalesByDateDto>>> GetSalesByDateAsync(DateTimeOffset fromDate, DateTimeOffset toDate)
    {
        var sales = await context.Sales
            .Where(n => n.SaleDate.Day >= fromDate.Day && n.SaleDate.Day <= toDate.Day)
            .Select(n => new SalesByDateDto
            {
                SaleId = n.Id,
                Name = n.Product.Name,
                QuantitySold = n.QuantitySold,
                SaleDate = n.SaleDate
            })
            .ToListAsync();

        return new Response<List<SalesByDateDto>>(sales);
    }

    // Task 5
    public async Task<Response<List<TopProductsDto>>> GetTopProducts()
    {
        var topSales = await context.Products
            .OrderByDescending(n => n.Sales.Count)
            .Take(5)
            .Select(n => new TopProductsDto
            {
                Name = n.Name,
                TotalSold = n.Sales.Count
            })
            .ToListAsync();

        return new Response<List<TopProductsDto>>(topSales);
    }

    // Task 6
    public async Task<Response<List<DailyRevenueDto>>> GetDailyRevenue()
    {
        var daily = await context.Sales
            .Where(n => n.SaleDate >= DateTime.Now.AddDays(-7))
            .Select(n => new DailyRevenueDto
            {
                Date = n.SaleDate,
                Revenue = n.QuantitySold * n.Product.Price
            })
            .ToListAsync();

        return new Response<List<DailyRevenueDto>>(daily);
    }

}
