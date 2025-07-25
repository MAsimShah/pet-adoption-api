namespace PetAdoption.Domain
{
    public class PetRequest : BaseEntity
    {
        public int PetId { get; set; }         // FK to Pet
        public int UserId { get; set; }        // FK to User making the request

        public DateTime RequestDate { get; set; } = DateTime.Now;

        public string Message { get; set; }    // Optional message by user

        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        // Navigation properties (optional if using EF Core)
        public Pet Pet { get; set; }
        public User User { get; set; }
    }

    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected,
        Cancelled
    }
}
