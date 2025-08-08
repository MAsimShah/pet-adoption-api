using PetAdoption.Application.DTO;
using PetAdoption.Domain;

namespace PetAdoption.Application.Interfaces
{
    public interface IPetService
    {
        Task<IEnumerable<PetDto>> GetAllPetsAsync(string userId = "");

        Task<PetDto?> GetPetByIdAsync(int id);

        Task<IEnumerable<DropdownDTO>> GetDropdownAsync(string userId, Species? specie = null);

        Task<PetDto> AddPetAsync(PetDto petDto);

        Task<PetDto> UpdatePetAsync(PetDto petDto);

        Task DeletePetAsync(int id);
    }
}
