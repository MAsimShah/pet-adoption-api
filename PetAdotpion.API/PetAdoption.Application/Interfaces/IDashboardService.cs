using PetAdoption.Application.DTO;

namespace PetAdoption.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardStatsDTO> GetStatsAsync(string userId = "", bool isAdmin = false);
    }
}
