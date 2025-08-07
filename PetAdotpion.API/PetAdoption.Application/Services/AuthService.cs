using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PetAdoption.Application.DTO;
using PetAdoption.Application.Interfaces;
using PetAdoption.Application.Interfaces.InfrastructureInterfaces;
using PetAdoption.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PetAdoption.Application.Services
{
    public class AuthService(IAuthRepository _authRepository, AppSettingConfiguration _appSetting, IMapper _mapper) : IAuthService
    {
        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
           List<User> users = await _authRepository.GetAllUsersAsync();

            return users.Select(x => new UserDTO()
            {
                Id = x.Id,
                Name = x.UserName,
                Email = x.Email,
                IsActive = x.IsActive
            }).ToList();
        }

        public async Task<User> GetUser(Expression<Func<User, bool>> predicate)
        {
            return await _authRepository.GetAsync(predicate);
        }

        public async Task<UserDTO> GetUserAsync(Expression<Func<User, bool>> predicate)
        {
            var user = await _authRepository.GetAsync(predicate);
            user.PasswordHash = string.Empty; // Clear password hash for security reasons
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<TokenResponseDTO> RegisterUserAsync(RegisterDTO model)
        {
            User user = new User { UserName = model.Email, Email = model.Email, PasswordHash = model.Password, ProfileImage = model.ProfilePhoto, PhoneNumber = model.PhoneNumber };

            user = await _authRepository.RegisterUserAsync(user);

            return await LoginUserAsync(user);
        }

        public async Task<bool> UpdateUser(UserDTO model)
        {
            var exisitingUser = await GetUser(x => x.Email == model.Email && x.Id == model.Id);

            var refreshToken = GenerateRefreshToken();
            exisitingUser.RefreshToken = refreshToken;
            exisitingUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            exisitingUser.UserName = model.Name;
            exisitingUser.Email = model.Email;
            exisitingUser.PhoneNumber = model.PhoneNumber;

            if (!string.IsNullOrEmpty(model.ProfileImage) || !string.IsNullOrWhiteSpace(model.ProfileImage))
                exisitingUser.ProfileImage = model.ProfileImage;

            return await _authRepository.UpdateUserAsync(exisitingUser, model.Password);
        }

        public async Task<bool> CheckUsePassword(User user, string password)
        {
            if (user != null && !string.IsNullOrEmpty(password))
            {
                return await _authRepository.CheckExistUsersPassword(user, password);
            }

            return false;
        }

        public async Task<TokenResponseDTO> LoginUserAsync(User user)
        {
            if (user == null)
                return null;

            string accessToken = GenerateJwtToken(user);
            string refreshToken = await GenerateAndSaveTokens(user);

            return new TokenResponseDTO() { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public async Task<TokenResponseDTO> RegenrateToken(User user)
        {
            if (user == null)
                return null;

            string accessToken = GenerateJwtToken(user);
            string refreshToken = await GenerateAndSaveTokens(user);

            return new TokenResponseDTO() { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),
                new Claim("ProfileImage", string.IsNullOrEmpty(user.ProfileImage) ? "" : (user.ProfileImage.Contains("uploads/users") ? user.ProfileImage : $"uploads/users/{user.ProfileImage}"))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetting.Token!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _appSetting.Issuer,
                audience: _appSetting.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveTokens(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            await _authRepository.UpdateUserAsync(user);
            return refreshToken;
        }
    }
}
