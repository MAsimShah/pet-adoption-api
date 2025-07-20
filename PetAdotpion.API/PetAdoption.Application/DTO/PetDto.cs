using PetAdoption.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Application.DTO
{
    public class PetDto : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public int Age { get; set; }
        public int ContactInformation { get; set; }
        public Species Species { get; set; } = Species.Other;
        public AnimalGender Gender { get; set; }
        public AnimalColor Color { get; set; }
        public HealthStatus HealthStatus { get; set; }

        public string Description { get; set; } = string.Empty;
        public bool Microchipped { get; set; } = false;
        public bool GoodWithKids { get; set; } = false;
        public bool GoodWithOtherPets { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public decimal? AdoptionFee { get; set; } = 0;
        public DateTime? AdoptableSince { get; set; }
        public string? Location { get; set; }
    }
}
