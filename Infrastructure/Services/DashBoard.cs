using AutoMapper;
using Domain.DTOs.Others;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class DashBoard(DataContext context, IMapper mapper) : IDashboard
{
    // Task 10
    public async Task<Response<DashBoardStatisticsDto>> GetStatistics()
    {
        var totalProducts = await context.Products.SumAsync(n => n.QuantityStock);
        var totalRevenue = await context.Products.SumAsync(n => n.Price);
        var totalSales = await context.Sales.SumAsync(n => n.QuantitySold);

        var statistics = new DashBoardStatisticsDto{
            TotalProducts = totalProducts,
            TotalRevenue = totalRevenue,
            TotalSales = totalSales
        };

        return new Response<DashBoardStatisticsDto>(statistics);
    }
}
