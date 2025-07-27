namespace PetAdoption.Application.DTO
{
    public class DropdownDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public bool Selected { get; set; } = false;
    }
}
