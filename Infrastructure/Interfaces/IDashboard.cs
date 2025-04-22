using Domain.DTOs.Others;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IDashboard
{
    Task<Response<DashBoardStatisticsDto>> GetStatistics();
}
