using Microsoft.AspNetCore.Http;

namespace PetAdoption.Application.DTO
{
    public class PetPhotoDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int PetId { get; set; }
        public string PhotoUrl { get; set; }
    }
}
