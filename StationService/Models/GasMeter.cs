using System.ComponentModel.DataAnnotations;

namespace StationService.Models
{
    public class GasMeter : BaseEntity
    {
        [Required]
        public double MeterReading { get; set; }
        public int? FuelPipeId { get; set; }
        public FuelPipe? FuelPipe { get; set; }

    }
}
