using System.Net;
using AutoMapper;
using Domain.DTOs.Others;
using Domain.DTOs.StockAdjustments;
using Domain.Entities;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class StockAdjustmentService(DataContext context, IMapper mapper) : IStockAdjustmentService
{
    public async Task<Response<GetStockAdjustmentDto>> CreateStockAdjustmentAsync(CreateStockAdjustmentDto stockAdjustmentDto)
    {
        var stockAdjustment = mapper.Map<StockAdjustment>(stockAdjustmentDto);
        await context.StockAdjustments.AddAsync(stockAdjustment);
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetStockAdjustmentDto>(stockAdjustment);
        return result == 0
            ? new Response<GetStockAdjustmentDto>(HttpStatusCode.BadRequest, "StockAdjustment not added!")
            : new Response<GetStockAdjustmentDto>(dto);

    }

    public async Task<Response<string>> DeleteStockAdjustmentAsync(int id)
    {
        var exist = await context.StockAdjustments.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "StockAdjustment not found");
        }
        context.StockAdjustments.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "StockAdjustment not deleted!")
            : new Response<string>("StockAdjustment deleted succesfully!");
    }

    public async Task<Response<List<GetStockAdjustmentDto>>> GetStockAdjustmentsAsync(StockAdjustmentFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);

        var stockAdjustments = context.StockAdjustments.AsQueryable();

        if (filter.FromAdjustmentAmount != null)
        {
            stockAdjustments = stockAdjustments.Where(n => n.AdjustmentAmount >= filter.FromAdjustmentAmount);
        }

        if (filter.ToAdjustmentAmount != null)
        {
            stockAdjustments = stockAdjustments.Where(n => n.AdjustmentAmount <= filter.ToAdjustmentAmount);
        }

        if (filter.FromAdjutmentDate != null)
        {
            stockAdjustments = stockAdjustments.Where(n => n.AdjutmentDate >= filter.ToAdjutmentDate);
        }

        if (filter.ToAdjutmentDate != null)
        {
            stockAdjustments = stockAdjustments.Where(n => n.AdjutmentDate <= filter.ToAdjutmentDate);
        }

        var mapped = mapper.Map<List<GetStockAdjustmentDto>>(stockAdjustments);

        var data = mapped
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToList();

        var totalRecords = mapped.Count;
        return new PagedResponse<List<GetStockAdjustmentDto>>(data, validFilter.PageNumber, validFilter.PageSize, totalRecords);
    }

    public async Task<Response<GetStockAdjustmentDto>> GetStockAdjustmentById(int id)
    {
        var exist = await context.StockAdjustments.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetStockAdjustmentDto>(HttpStatusCode.NotFound, "StockAdjustment not found!");
        }
        var dto = mapper.Map<GetStockAdjustmentDto>(exist);
        return new Response<GetStockAdjustmentDto>(dto);
    }

    public async Task<Response<GetStockAdjustmentDto>> UpdateStockAdjustmentAsync(int id, UpdateStockAdjustmentDto StockAdjustmentDto)
    {
        var exist = await context.StockAdjustments.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetStockAdjustmentDto>(HttpStatusCode.NotFound, "StockAdjustment not found!");
        }

        exist.ProductId = StockAdjustmentDto.ProductId;
        exist.AdjustmentAmount = StockAdjustmentDto.AdjustmentAmount;
        exist.AdjutmentDate = StockAdjustmentDto.AdjutmentDate;
        exist.Reason = StockAdjustmentDto.Reason;
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetStockAdjustmentDto>(exist);
        return result == 0
            ? new Response<GetStockAdjustmentDto>(HttpStatusCode.BadRequest, "StockAdjustment not updated!")
            : new Response<GetStockAdjustmentDto>(dto);
    }

    // Task 7
    public async Task<Response<List<StockAdjustmentHistoryDto>>> GetStockHistory(int productId)
    {
        var history = await context.StockAdjustments
            .Where(n => n.ProductId == productId)
            .Select(n => new StockAdjustmentHistoryDto{
                AdjustmentDate = n.AdjutmentDate,
                Amount = n.AdjustmentAmount,
                Reason = n.Reason
            })
            .ToListAsync();

        return new Response<List<StockAdjustmentHistoryDto>>(history);
    }
}
