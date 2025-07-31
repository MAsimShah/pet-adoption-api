using PetAdoption.Domain;

namespace PetAdoption.Application.DTO
{
    public class PetRequestDTO : BaseDTO
    {
        public int PetId { get; set; }
        public string? PetName { get; set; } 
        public string UserId { get; set; }
        public string? UserName { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;

        public string? Message { get; set; }

        public RequestStatus Status { get; set; }
    }
}
