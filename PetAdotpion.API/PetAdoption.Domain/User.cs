using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace PetAdoption.Domain
{
    public class User : IdentityUser
    {
        public string? ProfileImage { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}
