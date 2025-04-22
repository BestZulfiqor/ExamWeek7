using Domain.DTOs.Categories;
using Domain.DTOs.Others.CategoryProducts;
using Domain.Filters;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface ICategoryService
{
    Task<Response<List<GetCategoryDto>>> GetCategoriesAsync(CategoryFilter filter);
    Task<Response<GetCategoryDto>> CreateCategoryAsync(CreateCategoryDto categoryDto);
    Task<Response<GetCategoryDto>> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto);
    Task<Response<string>> DeleteCategoryAsync(int id);
    Task<Response<GetCategoryDto>> GetCategoryById(int id);
    Task<Response<List<CategoryProductsDto>>> GetCategoriesWithProductsAsync();
}
