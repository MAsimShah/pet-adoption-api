using Microsoft.AspNetCore.Identity;
using PetAdoption.Application.Interfaces.InfrastructureInterfaces;
using PetAdoption.Domain;
using PetAdoption.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace PetAdoption.Infrastructure.Repositories
{
    public class AuthRepository(IGenericRepository<User> _userRepo, UserManager<User> _userManager) : IAuthRepository
    {
        public async Task<User> RegisterUserAsync(User user)
        {
            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            if (!result.Succeeded)
                return null;

            return user;
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> predicate)
        {
            return await _userRepo.GetAsync(predicate);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            IdentityResult result = await _userManager.UpdateAsync(user);

            return result != null && result.Succeeded;
        }
    }
}