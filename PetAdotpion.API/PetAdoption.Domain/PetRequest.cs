using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoption.Domain
{
    public class PetRequest : BaseEntity
    {
        [ForeignKey("Pet")]
        public int? PetId { get; set; }         // FK to Pet
        
        [ForeignKey("User")]
        public string? UserId { get; set; }        // FK to User making the request

        public DateTime RequestDate { get; set; } = DateTime.Now;

        public string Message { get; set; }    // Optional message by user

        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        // Navigation properties (optional if using EF Core)
        public virtual Pet Pet { get; set; }
        public virtual User User { get; set; }
    }

    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected,
        Cancelled
    }
}
