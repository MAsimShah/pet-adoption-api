using PetAdoption.Application.Interfaces.InfrastructureInterfaces;
using PetAdoption.Domain;
using PetAdoption.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace PetAdoption.Infrastructure.Repositories
{
    public class PetPhotoRepository(IGenericRepository<PetPhoto> petPhotoRepo) : IPetPhotoRepository
    {
        private readonly IGenericRepository<PetPhoto> _petPhotoRepo = petPhotoRepo;

        public async Task<IEnumerable<PetPhoto>> GetListAsync(Expression<Func<PetPhoto, bool>> predicate)
        {
            return await _petPhotoRepo.ListAsync(predicate);
        }


        public async Task<PetPhoto> GetAsync(Expression<Func<PetPhoto, bool>> predicate)
        {
            return await _petPhotoRepo.GetAsync(predicate);
        }

        public async Task DeletePetPhotoAsync(int id)
        {
            var photo = await _petPhotoRepo.GetAsync(x => x.Id == id);
            if (photo != null)
            {
                await _petPhotoRepo.DeleteWithSaveAsync(photo);
            }
        }

        public async Task SavePetPhotosAsync(List<PetPhoto> entities)
        {
            if (entities == null || !entities.Any())
                throw new Exception("No files uploaded.");

            await _petPhotoRepo.AddRangeWithSaveAsync(entities);
        }
    }
}
