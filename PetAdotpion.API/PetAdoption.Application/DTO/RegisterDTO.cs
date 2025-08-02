using PetAdoption.Domain;

namespace PetAdoption.Application.DTO
{
    public class RegisterDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfilePhoto { get; set; }
        public string PhoneNumber { get; set; }
    }
}
