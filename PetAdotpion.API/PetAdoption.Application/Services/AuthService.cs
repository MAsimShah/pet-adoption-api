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
    public class AuthService(IAuthRepository _authRepository, AppSettingConfiguration _appSetting) : IAuthService
    {
        public async Task<User> GetUser(Expression<Func<User, bool>> predicate)
        {
            return await _authRepository.GetAsync(predicate);
        }

        public async Task<User> RegisterUserAsync(RegisterDTO model)
        {
            User user = new User { UserName = model.Email, Email = model.Email, PasswordHash = model.Password, ProfileImage = model.ProfilePhoto };

            return await _authRepository.RegisterUserAsync(user);
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
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),
                new Claim("ProfileImage", string.IsNullOrEmpty(user.ProfileImage) ? "" : $"uploads/users/{user.ProfileImage}")
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
