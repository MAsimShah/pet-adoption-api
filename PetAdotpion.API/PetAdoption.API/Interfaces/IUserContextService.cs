namespace PetAdoption.API.Interfaces
{
    public interface IUserContextService
    {
        string? UserId { get; }
        bool IsAdmin { get; }
    }
}
