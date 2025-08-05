using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Application.DTO
{
    public class UserDTO : RegisterDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Base64ImageFile? ProfilePhoto { get; set; }
        public string PhoneNumber { get; set; }

        public string? ProfileImage { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }

    public record Base64ImageFile(string FileName, string Base64Data);

    public record PetFilesUploadRequest(int PetId, List<string> Images);

    public record Base64UploadRequest(int PetId, List<Base64ImageFile> Images);
}
