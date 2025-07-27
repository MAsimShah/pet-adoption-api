using PetAdoption.Application.DTO;

namespace PetAdoption.Application.Interfaces
{
    public interface IPetService
    {
        Task<IEnumerable<PetDto>> GetAllPetsAsync();

        Task<PetDto?> GetPetByIdAsync(int id);

        Task<IEnumerable<DropdownDTO>> GetDropdownAsync();

        Task<PetDto> AddPetAsync(PetDto petDto);

        Task<PetDto> UpdatePetAsync(PetDto petDto);

        Task DeletePetAsync(int id);
    }
}
