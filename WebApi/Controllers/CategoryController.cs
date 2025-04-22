using Domain.DTOs.Categories;
using Domain.DTOs.Others.CategoryProducts;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService service)
{
    [HttpGet]
    public async Task<Response<List<GetCategoryDto>>> GetCategoriesAsync([FromQuery] CategoryFilter filter) => await service.GetCategoriesAsync(filter);
    [HttpPost]
    public async Task<Response<GetCategoryDto>> CreateCategoryAsync(CreateCategoryDto categoryDto) => await service.CreateCategoryAsync(categoryDto);
    [HttpPut("{id:int}")]
    public async Task<Response<GetCategoryDto>> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto) => await service.UpdateCategoryAsync(id, categoryDto);
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteCategoryAsync(int id) => await service.DeleteCategoryAsync(id);
    [HttpGet("{id:int}")]
    public async Task<Response<GetCategoryDto>> GetCategoryById(int id) => await service.GetCategoryById(id);

    [HttpGet("category-with-products")]
    public async Task<Response<List<CategoryProductsDto>>> GetCategoriesWithProductsAsync() => await service.GetCategoriesWithProductsAsync();
}