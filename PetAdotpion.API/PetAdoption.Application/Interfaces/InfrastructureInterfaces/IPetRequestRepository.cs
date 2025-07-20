using PetAdoption.Domain;
using System.Linq.Expressions;

namespace PetAdoption.Application.Interfaces.InfrastructureInterfaces
{
    public interface IPetRequestRepository
    {
        Task<IEnumerable<PetRequest>> GetAllRequestsAsync(Expression<Func<PetRequest, bool>> predicate = null);

        Task<PetRequest?> GetRequestByIdAsync(int id);

        Task AddRequestAsync(PetRequest request);

        Task UpdateRequestAsync(PetRequest request);

        Task DeleteRequestAsync(int id);

        Task<PetRequest> GetAsync(Expression<Func<PetRequest, bool>> predicate);
    }
}
