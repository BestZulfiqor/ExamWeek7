using Domain.DTOs.Others.Suppliers;
using Domain.DTOs.Suppliers;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SupplierController(ISupplierService service)
{
    [HttpGet]
    public async Task<Response<List<GetSupplierDto>>> GetSuppliersAsync([FromQuery]SupplierFilter filter) => await service.GetSuppliersAsync(filter);

    [HttpPost]
    public async Task<Response<GetSupplierDto>> CreateSupplierAsync(CreateSupplierDto supplierDto) => await service.CreateSupplierAsync(supplierDto);

    [HttpPut("{id:int}")]
    public async Task<Response<GetSupplierDto>> UpdateSupplierAsync(int id, UpdateSupplierDto supplierDto) => await service.UpdateSupplierAsync(id, supplierDto);

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteSupplierAsync(int id) => await service.DeleteSupplierAsync(id);

    [HttpGet("{id:int}")]
    public async Task<Response<GetSupplierDto>> GetSupplierById(int id) => await service.GetSupplierById(id);

    [HttpGet("suppliers-with-products")]
    public async Task<Response<List<SupplierDto>>> GetSuppliersWithProducts() => await service.GetSuppliersWithProducts();
}