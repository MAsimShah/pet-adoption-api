using PetAdoption.Application.DTO;
using PetAdoption.Domain;
using System.Linq.Expressions;

namespace PetAdoption.Application.Interfaces
{
    public interface IAuthService
    {
        Task<User> GetUser(Expression<Func<User, bool>> predicate);

        Task<User> RegisterUserAsync(RegisterDTO model);

        Task<TokenResponseDTO> LoginUserAsync(User user);

        Task<bool> CheckUsePassword(User user, string password);

        Task<TokenResponseDTO> RegenrateToken(User user);
    }
}
