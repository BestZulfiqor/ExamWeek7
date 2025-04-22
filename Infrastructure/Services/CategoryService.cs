using System.Net;
using AutoMapper;
using Domain.DTOs.Categories;
using Domain.DTOs.Others.CategoryProducts;
using Domain.Entities;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CategoryService(DataContext context, IMapper mapper) : ICategoryService
{
    public async Task<Response<GetCategoryDto>> CreateCategoryAsync(CreateCategoryDto categoryDto)
    {
        var category = mapper.Map<Category>(categoryDto);
        await context.Categories.AddAsync(category);
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetCategoryDto>(category);
        return result == 0
            ? new Response<GetCategoryDto>(HttpStatusCode.BadRequest, "Category not added!")
            : new Response<GetCategoryDto>(dto);

    }

    public async Task<Response<string>> DeleteCategoryAsync(int id)
    {
        var exist = await context.Categories.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Category not found");
        }
        context.Categories.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Category not deleted!")
            : new Response<string>("Category deleted succesfully!");
    }

    public async Task<Response<List<GetCategoryDto>>> GetCategoriesAsync(CategoryFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);

        var categories = context.Categories.AsQueryable();

        if (filter.Name != null)
        {
            categories = categories.Where(n => n.Name.Contains(filter.Name));
        }
        var mapped = mapper.Map<List<GetCategoryDto>>(categories);

        var data = mapped
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToList();

        var totalRecords = mapped.Count;
        return new PagedResponse<List<GetCategoryDto>>(data, validFilter.PageNumber, validFilter.PageSize, totalRecords);
    }

    public async Task<Response<GetCategoryDto>> GetCategoryById(int id)
    {
        var exist = await context.Categories.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetCategoryDto>(HttpStatusCode.NotFound, "Category not found!");
        }
        var dto = mapper.Map<GetCategoryDto>(exist);
        return new Response<GetCategoryDto>(dto);
    }

    public async Task<Response<GetCategoryDto>> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto)
    {
        var exist = await context.Categories.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetCategoryDto>(HttpStatusCode.NotFound, "Category not found!");
        }

        exist.Name = categoryDto.Name;
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetCategoryDto>(exist);
        return result == 0
            ? new Response<GetCategoryDto>(HttpStatusCode.BadRequest, "Category not updated!")
            : new Response<GetCategoryDto>(dto);
    }

    // Task 1
    public async Task<Response<List<CategoryProductsDto>>> GetCategoriesWithProductsAsync()
    {
        var categories = await context.Categories
            .Select(n => new CategoryProductsDto
            {
                CategoryId = n.Id,
                CategoryName = n.Name,
                Products = n.Products.Select(n => new ProductDto
                {
                    ProductId = n.Id,
                    Price = n.Price,
                    Name = n.Name
                }).ToList()
            })
            .ToListAsync();

        return new Response<List<CategoryProductsDto>>(categories);
    }
}
