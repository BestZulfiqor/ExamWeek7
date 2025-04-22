using System.Net;
using AutoMapper;
using Domain.DTOs.Others.Suppliers;
using Domain.DTOs.Suppliers;
using Domain.Entities;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class SupplierService(DataContext context, IMapper mapper) : ISupplierService
{
    public async Task<Response<GetSupplierDto>> CreateSupplierAsync(CreateSupplierDto supplierDto)
    {
        var supplier = mapper.Map<Supplier>(supplierDto);
        await context.Suppliers.AddAsync(supplier);
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetSupplierDto>(supplier);
        return result == 0
            ? new Response<GetSupplierDto>(HttpStatusCode.BadRequest, "Supplier not added!")
            : new Response<GetSupplierDto>(dto);

    }

    public async Task<Response<string>> DeleteSupplierAsync(int id)
    {
        var exist = await context.Suppliers.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Supplier not found");
        }
        context.Suppliers.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Supplier not deleted!")
            : new Response<string>("Supplier deleted succesfully!");
    }

    public async Task<Response<List<GetSupplierDto>>> GetSuppliersAsync(SupplierFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);

        var Suppliers = context.Suppliers.AsQueryable();

        if (filter.Name != null)
        {
            Suppliers = Suppliers.Where(n => n.Name.Contains(filter.Name));
        }

        var mapped = mapper.Map<List<GetSupplierDto>>(Suppliers);

        var data = mapped
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToList();

        var totalRecords = mapped.Count;
        return new PagedResponse<List<GetSupplierDto>>(data, validFilter.PageNumber, validFilter.PageSize, totalRecords);
    }

    public async Task<Response<GetSupplierDto>> GetSupplierById(int id)
    {
        var exist = await context.Suppliers.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetSupplierDto>(HttpStatusCode.NotFound, "Supplier not found!");
        }
        var dto = mapper.Map<GetSupplierDto>(exist);
        return new Response<GetSupplierDto>(dto);
    }

    public async Task<Response<GetSupplierDto>> UpdateSupplierAsync(int id, UpdateSupplierDto SupplierDto)
    {
        var exist = await context.Suppliers.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetSupplierDto>(HttpStatusCode.NotFound, "Supplier not found!");
        }

        exist.Name = SupplierDto.Name;
        exist.Phone = SupplierDto.Phone;
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetSupplierDto>(exist);
        return result == 0
            ? new Response<GetSupplierDto>(HttpStatusCode.BadRequest, "Supplier not updated!")
            : new Response<GetSupplierDto>(dto);
    }

    // Task 9
    public async Task<Response<List<SupplierDto>>> GetSuppliersWithProducts()
    {
        var suppliers = await context.Suppliers
            .Select(n => new SupplierDto
            {
                SupplierId = n.Id,
                SupplierName = n.Name,
                Products = n.Products.Select(n => new Products{
                    Name = n.Name
                }).ToList()
            }).ToListAsync();

        return new Response<List<SupplierDto>>(suppliers);
    }
}
