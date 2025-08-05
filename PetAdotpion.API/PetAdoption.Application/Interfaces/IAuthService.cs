using PetAdoption.Application.DTO;
using PetAdoption.Domain;
using System.Linq.Expressions;

namespace PetAdoption.Application.Interfaces
{
    public interface IAuthService
    {
        Task<List<UserDTO>> GetAllUsersAsync();

        Task<User> GetUser(Expression<Func<User, bool>> predicate);

        Task<UserDTO> GetUserAsync(Expression<Func<User, bool>> predicate);

        Task<TokenResponseDTO> RegisterUserAsync(RegisterDTO model);

        Task<bool> UpdateUser(UserDTO model);

        Task<TokenResponseDTO> LoginUserAsync(User user);

        Task<bool> CheckUsePassword(User user, string password);

        Task<TokenResponseDTO> RegenrateToken(User user);
    }
}
