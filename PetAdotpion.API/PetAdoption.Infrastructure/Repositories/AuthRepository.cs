using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Application.Interfaces.InfrastructureInterfaces;
using PetAdoption.Domain;
using PetAdoption.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace PetAdoption.Infrastructure.Repositories
{
    public class AuthRepository(IGenericRepository<User> _userRepo, UserManager<User> _userManager) : IAuthRepository
    {
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userManager.Users.Where(_ => !_.IsAdmin)
                .Select(u => new User()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                }).ToListAsync();

        }

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

        public async Task<bool> CheckExistUsersPassword(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}