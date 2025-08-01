using PetAdoption.API.Interfaces;
using System.Security.Claims;

namespace PetAdoption.API.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var user = _httpContextAccessor.HttpContext?.User;
            var role = user?.FindFirst(ClaimTypes.Role)?.Value;
            IsAdmin = string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase);
            UserId = IsAdmin ? "" : user?.FindFirst("Id")?.Value;
        }

        public string? UserId { get; }
        public bool IsAdmin { get; }
    }
}
