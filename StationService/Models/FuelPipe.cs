using System.ComponentModel.DataAnnotations;

namespace StationService.Models
{
    public class FuelPipe: BaseEntity
    {
        [Required]
        public FuelType Type { get; set; }

        [Required]
        public int DispensingUnitId { get; set; }

        public DispensingUnit DispensingUnit { get; set; }

        public GasMeter Meter { get; set; }

    }
}
