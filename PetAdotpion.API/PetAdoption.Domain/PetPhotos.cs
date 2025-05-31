using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Domain
{
    public class PetPhoto : BaseEntity
    {
        [ForeignKey("PetTable")]
        public int PetId { get; set; }

        public string? PhotoUrl { get; set; }


        public virtual Pet PetTable {  get; set; }
    }
}
