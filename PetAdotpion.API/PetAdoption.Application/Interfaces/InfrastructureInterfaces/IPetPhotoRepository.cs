using PetAdoption.Domain;
using System.Linq.Expressions;

namespace PetAdoption.Application.Interfaces.InfrastructureInterfaces
{
    public interface IPetPhotoRepository
    {
        Task<IEnumerable<PetPhoto>> GetListAsync(Expression<Func<PetPhoto, bool>> predicate);

        Task<PetPhoto> GetAsync(Expression<Func<PetPhoto, bool>> predicate);

        Task DeletePetPhotoAsync(int id);

        Task SavePetPhotosAsync(List<PetPhoto> entities);
    }
}