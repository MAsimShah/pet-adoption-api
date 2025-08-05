using PetAdoption.Application.DTO;

namespace PetAdoption.API.Models
{
    public class RegisterViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public Base64ImageFile? ProfilePhoto { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public Base64ImageFile? ProfilePhoto { get; set; }
        public string? ProfileImage { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
