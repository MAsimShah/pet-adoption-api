using PetAdoption.Domain;

namespace PetAdoption.Domain
{
    public class Pet : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsAdopted { get; set; } = false;

        public string? ImageBase64 { get; set; } // Store image as Base64
        public string UserId { get; set; }

        //public virtual ApplicationUser User { get; set; }
    }
}
