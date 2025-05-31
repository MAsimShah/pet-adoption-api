using PetAdoption.Application.DTO;

namespace PetAdoption.Application.Interfaces
{
    public interface IPetService
    {
        Task<IEnumerable<PetDto>> GetAllPetsAsync();

        Task<PetDto?> GetPetByIdAsync(int id);

        Task<PetDto> AddPetAsync(PetDto petDto);

        Task<PetDto> UpdatePetAsync(PetDto petDto);

        Task DeletePetAsync(int id);

        Task DeletePetPhotoAsync(int id);

        Task SavePetPhotosAsync(int petId, List<string> filePaths);
    }
}
