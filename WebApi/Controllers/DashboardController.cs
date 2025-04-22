using Domain.DTOs.Others;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DashboardController(IDashboard service)
{
    [HttpGet]
    public async Task<Response<DashBoardStatisticsDto>> GetStatistics() => await service.GetStatistics();
}
