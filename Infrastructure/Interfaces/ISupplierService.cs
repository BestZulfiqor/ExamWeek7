using Domain.DTOs.Others.Suppliers;
using Domain.DTOs.Suppliers;
using Domain.Filters;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface ISupplierService
{
    Task<Response<List<GetSupplierDto>>> GetSuppliersAsync(SupplierFilter filter);
    Task<Response<GetSupplierDto>> CreateSupplierAsync(CreateSupplierDto supplierDto);
    Task<Response<GetSupplierDto>> UpdateSupplierAsync(int id, UpdateSupplierDto supplierDto);
    Task<Response<string>> DeleteSupplierAsync(int id);
    Task<Response<GetSupplierDto>> GetSupplierById(int id);
    Task<Response<List<SupplierDto>>> GetSuppliersWithProducts();
}
