using PetAdoption.Application.DTO;
using PetAdoption.Domain;

namespace PetAdoption.Application.Interfaces
{
    public interface IPetRequestService
    {
        Task<IEnumerable<PetRequestDTO>> GetAllRequestsAsync(string userId = "");

        Task<IEnumerable<PetRequestDTO>> GetAllUserRequestsAsync(string userId);

        Task<PetRequestDTO?> GetRequestByIdAsync(int id);

        Task<PetRequestDTO> AddRequestAsync(PetRequestDTO requestDto);

        Task<PetRequestDTO> UpdateRequestAsync(PetRequestDTO requestDto);

        Task<PetRequestDTO> UpdateRequestStatusAsync(int requestId, string ownerId, RequestStatus Status);

        Task DeleteRequestAsync(int id);
    }
}