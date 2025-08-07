namespace PetAdoption.Application.DTO
{
    public class DashboardStatsDTO
    {
        public int TotalPets { get; set; }
        public int TotalAdoptionApproved { get; set; }
        public int TotalActiveUsers { get; set; }
        public int TotalPendingRequests { get; set; }
    }
}
