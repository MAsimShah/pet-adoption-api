using PetAdoption.Application.DTO;

namespace PetAdoption.Application.Interfaces
{
    public interface IPetRequestService
    {
        Task<IEnumerable<PetRequestDTO>> GetAllRequestsAsync();

        Task<PetRequestDTO?> GetRequestByIdAsync(int id);

        Task<PetRequestDTO> AddRequestAsync(PetRequestDTO requestDto);

        Task<PetRequestDTO> UpdateRequestAsync(PetRequestDTO requestDto);

        Task DeleteRequestAsync(int id);
    }
}