using System.ComponentModel.DataAnnotations;

namespace StationService.Models
{
    public class DispensingUnit : BaseEntity
    {
        [Required]
        public String UnitNumber { get; set; }

        [Required]
        public int GasStationId { get; set; }
        public GasStation GasStation { get; set; }

        [Required]
        public ICollection<FuelPipe> Pipes { get; set; } 
    }
}
