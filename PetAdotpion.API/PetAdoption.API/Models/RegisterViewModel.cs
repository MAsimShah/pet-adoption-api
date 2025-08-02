using PetAdoption.Api.Controllers;
using PetAdoption.Domain;

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

    public class UserViewModel : RegisterViewModel
    {
        public string Id { get; set; }
    }
}
