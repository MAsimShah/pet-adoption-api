using PetAdoption.Domain;
using System.Linq.Expressions;

namespace PetAdoption.Application.Interfaces.InfrastructureInterfaces
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAllPetsAsync(Expression<Func<Pet, bool>> predicate);

        Task AddPetAsync(Pet pet);

        Task UpdatePetAsync(Pet pet);

        Task DeletePetAsync(int id);

        Task<Pet> GetAsync(Expression<Func<Pet, bool>> predicate);
    }
}
