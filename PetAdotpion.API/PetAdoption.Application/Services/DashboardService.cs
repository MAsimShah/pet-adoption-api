using PetAdoption.Application.DTO;
using PetAdoption.Application.Interfaces;

namespace PetAdoption.Application.Services
{
    public class DashboardService(IPetService _petService, IPetRequestService _requestService, IAuthService _authService) : IDashboardService
    {
        public async Task<DashboardStatsDTO> GetStatsAsync(string userId = "", bool isAdmin = false)
        {

            IEnumerable<PetDto> pets = await _petService.GetAllPetsAsync(userId);
            IEnumerable<PetRequestDTO> requests = await _requestService.GetAllRequestsAsync(userId);
            int totalActiveUsers = 0;

            if (isAdmin && !string.IsNullOrEmpty(userId))
            {
                IEnumerable<UserDTO> users = await _authService.GetAllUsersAsync();
                totalActiveUsers = users.Count(x => x.IsActive);
            }

            var totalPets = pets.Count();
            var totalAdoptionsApproved = requests.Count(p => p.Status == Domain.RequestStatus.Approved);
            var totalPendingRequests = requests.Count(p => p.Status == Domain.RequestStatus.Pending);

            return new DashboardStatsDTO
            {
                TotalPets = totalPets,
                TotalAdoptionApproved = totalAdoptionsApproved,
                TotalActiveUsers = totalActiveUsers,
                TotalPendingRequests = totalPendingRequests
            };
        }
    }
}
