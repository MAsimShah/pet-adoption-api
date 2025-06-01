using PetAdoption.Application.DTO;
using PetAdoption.Domain;
using System.Linq.Expressions;

namespace PetAdoption.Application.Interfaces
{
    public interface IPetPhotoService
    {
        Task<PetPhotoDTO> GetAsync(Expression<Func<PetPhoto, bool>> predicate = null);

        Task DeletePetPhotoAsync(int id);

        Task SavePetPhotosAsync(int petId, List<string> filePaths);
    }
}
