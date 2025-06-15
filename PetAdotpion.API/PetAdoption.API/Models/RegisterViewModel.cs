using PetAdoption.Api.Controllers;

namespace PetAdoption.API.Models
{
    public class RegisterViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        public Base64ImageFile? ProfilePhoto { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}
