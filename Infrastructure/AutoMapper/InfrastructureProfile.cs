using AutoMapper;
using Domain.DTOs.Categories;
using Domain.DTOs.Products;
using Domain.DTOs.Sales;
using Domain.DTOs.StockAdjustments;
using Domain.DTOs.Suppliers;
using Domain.Entities;

namespace Infrastructure.AutoMapper;
public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Category, GetCategoryDto>();
        CreateMap<GetCategoryDto, Category>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();

        CreateMap<Product, GetProductDto>();
        CreateMap<GetProductDto, Product>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        
        CreateMap<Sale, GetSaleDto>();
        CreateMap<GetSaleDto, Sale>();
        CreateMap<CreateSaleDto, Sale>();
        CreateMap<UpdateSaleDto, Sale>();
        
        CreateMap<StockAdjustment, GetStockAdjustmentDto>();
        CreateMap<GetStockAdjustmentDto, StockAdjustment>();
        CreateMap<CreateStockAdjustmentDto, StockAdjustment>();
        CreateMap<UpdateStockAdjustmentDto, StockAdjustment>();
        
        CreateMap<Supplier, GetSupplierDto>();
        CreateMap<GetSupplierDto, Supplier>();
        CreateMap<CreateSupplierDto, Supplier>();
        CreateMap<UpdateSupplierDto, Supplier>();
    }
}