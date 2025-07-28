using PetAdoption.Application.Interfaces.InfrastructureInterfaces;
using PetAdoption.Domain;
using PetAdoption.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace PetAdoption.Infrastructure.Repositories
{
    public class PetRequestRepository(IGenericRepository<PetRequest> _requestRepo) : IPetRequestRepository
    {
        public async Task<IEnumerable<PetRequest>> GetAllRequestsAsync(Expression<Func<PetRequest, bool>> predicate = null)
        {
            return await _requestRepo.ListAsync(predicate, a => a.Pet, b => b.User);
        }

        public async Task<PetRequest?> GetRequestByIdAsync(int id)
        {
            return await _requestRepo.GetAsync(x => x.Id == id);
        }

        public async Task AddRequestAsync(PetRequest request)
        {
            await _requestRepo.AddWithSaveAsync(request);
        }

        public async Task UpdateRequestAsync(PetRequest request)
        {
            await _requestRepo.UpdateWithSaveAsync(request);
        }

        public async Task DeleteRequestAsync(int id)
        {
            var pet = await _requestRepo.GetAsync(x => x.Id == id);
            if (pet != null)
            {
                await _requestRepo.DeleteWithSaveAsync(pet);
            }
        }

        public async Task<PetRequest> GetAsync(Expression<Func<PetRequest, bool>> predicate)
        {
            return await _requestRepo.GetAsync(predicate, a => a.Pet, b => b.User);
        }
    }
}
