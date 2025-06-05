using PetAdoption.Domain;
using System.Linq.Expressions;

namespace PetAdoption.Application.Interfaces.InfrastructureInterfaces
{
    public interface IAuthRepository
    {
        Task<User> RegisterUserAsync(User user);

        Task<User> GetAsync(Expression<Func<User, bool>> predicate);

        Task<bool> UpdateUserAsync(User user);
    }
}